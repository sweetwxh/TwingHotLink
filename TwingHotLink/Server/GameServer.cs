using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using TwingHotLink.Client;
using TwingHotLink.Common;
using TwingHotLink.Server.Extensions;
using TwingHotLink.Tools;

/***
 * 作者：吴兴华
 * 日期：2010-12-16
 * 最后修改日期：2011-01-04
 * Copryright:StepOn Technology
 * 
 * 2010-12-21：
 * 1.增加了UDP协议相关函数，并实现NAT穿透
 * 
 * 2011-01-04：
 * 1.优化代码
 * */

namespace TwingHotLink.Server
{
    internal enum ServerCode
    {
        AddPlayer = 0,
        PSPData = 1,
        KeepAlive = 2, //仅Udp命令
        UdpEnd = 3,
        Chat = 4
    }

    internal class GameServer : ICMDProcessor
    {
        public void ProcessCMD(byte code, byte[] data, ClientInfo client, EndPoint clientEndPoint)
        {
            var scode = (ServerCode) code;
            switch (scode)
            {
                case ServerCode.AddPlayer:
                    ProcessAddPlayer(client, data);
                    break;
                case ServerCode.UdpEnd:
                    ProcessUdpEnd(client);
                    break;
                case ServerCode.KeepAlive:
                    ProcessKeepAlive(data, clientEndPoint);
                    break;
                case ServerCode.PSPData:
                    if (client.IsClientAdd)
                    {
                        ProcessPSPData(client, data);
                    }
                    break;
                case ServerCode.Chat:
                    SendChatData(data);
                    break;
            }
        }

        #region 成员变量

        /// <summary>
        ///     服务端Socket
        /// </summary>
        private Socket serverSocket;

        /// <summary>
        ///     客户端信息列表(客户端ID，客户端信息类)，包含所有的客户端
        /// </summary>
        private readonly Dictionary<string, ClientInfo> clientList = new Dictionary<string, ClientInfo>();

        /// <summary>
        ///     客户端列表读写锁
        /// </summary>
        private readonly ReaderWriterLockSlim clientLocker = new ReaderWriterLockSlim();

        /// <summary>
        ///     端口号
        /// </summary>
        private readonly int port = GlobleSetting.TcpPort;

        /// <summary>
        ///     最大客户端连接数
        /// </summary>
        private readonly int maxConnection = 4;

        /// <summary>
        ///     等待队列数
        /// </summary>
        private readonly int backLog = 4;

        /// <summary>
        ///     当前连接数
        /// </summary>
        private int currentConnectedNumber;

        private readonly IMainFrame frame;

        /// <summary>
        ///     数据缓存池
        /// </summary>
        private BufferPool bufferManager;

        /// <summary>
        ///     用于同步服务器的运行
        /// </summary>
        private static readonly Mutex mutex = new Mutex();

        /// <summary>
        ///     接收数据和发送数据准备的参数池
        /// </summary>
        private SocketArgsPool argsPool;

        /// <summary>
        ///     限制连接到服务器的最大连接数
        /// </summary>
        private Semaphore semaphoreAcceptedClients;

        /// <summary>
        ///     为读写准备数据缓存
        /// </summary>
        private const int opsToPreAlloc = 2;

        /// <summary>
        ///     数据包大小（接受和发送）
        /// </summary>
        private readonly int bufferSize = GlobleSetting.BufferSize;

        /// <summary>
        ///     接受连接参数
        /// </summary>
        private SocketAsyncEventArgs AcceptArg;

        private readonly string hostNickName = string.Empty;

        private readonly int requestTimeout = GlobleSetting.RequestTimeout;

        private readonly bool isAsync = GlobleSetting.IsAsync;

        #endregion

        #region 构造函数

        public GameServer(IMainFrame frame, string hostNickName)
        {
            this.frame = frame;
            this.hostNickName = hostNickName;
        }

        public GameServer(IMainFrame frame, string hostNickName, int maxConnection)
        {
            this.frame = frame;
            this.hostNickName = hostNickName;
            this.maxConnection = maxConnection;
            backLog = maxConnection;
        }

        public GameServer(IMainFrame frame, string hostNickName, int maxConnection, int port)
        {
            this.frame = frame;
            this.hostNickName = hostNickName;
            this.maxConnection = maxConnection;
            backLog = maxConnection;
            this.port = port;
        }

        #endregion

        #region 服务器初始化及连接管理函数

        private void initServer()
        {
            var accept = maxConnection + 1;
            //建立缓存池，包括读写，缓存池的大小为最大连接数×数据包大小×2（2表示读写两个）
            bufferManager = new BufferPool(accept*bufferSize*opsToPreAlloc, bufferSize);
            //同样建立读写参数池
            argsPool = new SocketArgsPool(accept*opsToPreAlloc);
            //根据最大连接数建立Semaphore
            semaphoreAcceptedClients = new Semaphore(accept, accept);
            //预分配数据发送接收参数
            SocketAsyncEventArgs rsArgs;
            for (var i = 0; i < accept*opsToPreAlloc; i++)
            {
                rsArgs = new SocketAsyncEventArgs();
                rsArgs.Completed += IOCompleted;
                bufferManager.SetBuffer(rsArgs);
                argsPool.Push(rsArgs);
            }
            //接受连接参数
            AcceptArg = new SocketAsyncEventArgs();
            AcceptArg.Completed += SocketAccepted;
        }

        /// <summary>
        ///     启动服务器，并开始监听客户端连接，未添加IPV6的处理
        /// </summary>
        public void StartServer()
        {
            initServer();
            //建立本地服务器通信端口
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            var serverEndPoint = new IPEndPoint(IPAddress.Any, port);
            serverSocket.Bind(serverEndPoint);
            serverSocket.Listen(backLog);

            InitUdpServer();
            IsServerStarted = true;
            frame.SetMessage("服务器已经启动");
            StartListening(AcceptArg);
            mutex.WaitOne();
            frame.ToggleBuildButton();
        }

        /// <summary>
        ///     开始监听连接
        /// </summary>
        /// <param name="args">事件参数</param>
        private void StartListening(SocketAsyncEventArgs args)
        {
            //清空Socket以便重用
            args.AcceptSocket = null;
            //这里控制连接数，如果满了最大连接数，则该操作挂起直到连接数小于最大数
            semaphoreAcceptedClients.WaitOne();
            var willRaiseEvent = serverSocket.AcceptAsync(args);
            if (!willRaiseEvent)
            {
                ProcessAccept(args);
            }
        }

        /// <summary>
        ///     连接完成事件
        /// </summary>
        /// <param name="sender">产生事件的对象</param>
        /// <param name="e">事件参数</param>
        private void SocketAccepted(object sender, SocketAsyncEventArgs e)
        {
            //AcceptArg.Compelte的事件
            if (e.SocketError == SocketError.OperationAborted)
            {
                return; //服务器停止
            }
            if (e.SocketError == SocketError.Success)
            {
                ProcessAccept(e);
            }
        }

        /// <summary>
        ///     开始处理接受连接
        /// </summary>
        /// <param name="args">事件参数</param>
        private void ProcessAccept(SocketAsyncEventArgs args)
        {
            try
            {
                if (!IsServerStarted)
                {
                    return;
                }
                //从参数池中取接收数据参数
                var revcEventArg = argsPool.Pop();
                //从参数池中取发送数据参数
                var sendEventArg = argsPool.Pop();

                var client = new ClientInfo(args.AcceptSocket, revcEventArg, sendEventArg);
                client.recvBuffer = client.revcEventArg.Buffer;
                client.sendBuffer = client.sendEventArg.Buffer;
                client.revcEventArg.UserToken = client;
                client.PlayerAddress = (args.AcceptSocket.RemoteEndPoint as IPEndPoint).Address.ToString();
                AddClientToList(client);

                WaitForData(client.revcEventArg);
                StartListening(AcceptArg);
            }
            catch (ObjectDisposedException ex)
            {
            }
            catch (Exception ex)
            {
                ErrorLogger.LogException(ex);
            }
        }

        /// <summary>
        ///     关闭服务器
        /// </summary>
        /// <exception cref="关闭服务器的Socket异常"></exception>
        public void CloseServer()
        {
            IsServerStarted = false;
            //获取连接的客户端列表
            var toClose = new ClientInfo[clientList.Count];
            var counter = 0;
            clientLocker.EnterReadLock();
            try
            {
                foreach (var kvp in clientList)
                {
                    toClose[counter] = kvp.Value;
                    counter++;
                }
            }
            finally
            {
                clientLocker.ExitReadLock();
            }
            //断开连接
            for (var i = 0; i < toClose.Length; i++)
            {
                DropClient(toClose[i]);
            }
            try
            {
                serverSocket.Close();
                udpSocket.Close();
            }
            catch (Exception ex)
            {
                ErrorLogger.LogException(ex);
                throw new Exception("关闭服务器时出现异常");
            }
            //释放数据池资源
            //Very important,must set null!!!
            bufferManager = null;
            argsPool = null;
            mutex.ReleaseMutex();
            frame.SetMessage("服务器已经关闭");
            frame.ToggleBuildButton();
        }

        #endregion

        #region 数据接收及处理函数

        /// <summary>
        ///     数据接收或发送事件完成
        /// </summary>
        /// <param name="sender">产生事件的对象</param>
        /// <param name="e">事件参数</param>
        private void IOCompleted(object sender, SocketAsyncEventArgs e)
        {
            //initServer中rsArgs.Complete的事件
            if (e.LastOperation == SocketAsyncOperation.Receive)
            {
                ProcessReceives(e);
            }
        }

        /// <summary>
        ///     开启异步数据接收
        /// </summary>
        /// <param name="args">事件参数</param>
        private void WaitForData(SocketAsyncEventArgs args)
        {
            try
            {
                var clientSocket = (args.UserToken as ClientInfo).ClientSocket;
                if (clientSocket.Connected)
                {
                    //异步接收数据，接受到数据以后调用IOCompleted函数
                    var willRaiseEvent = clientSocket.ReceiveAsync(args);
                    if (!willRaiseEvent)
                    {
                        ProcessReceives(args);
                    }
                }
            }
            catch (NullReferenceException nrex)
            {
                //do noting
            }
            catch (Exception ex)
            {
                ErrorLogger.LogException(ex);
            }
        }

        /// <summary>
        ///     开始处理数据接收
        /// </summary>
        /// <param name="args">事件参数</param>
        private void ProcessReceives(SocketAsyncEventArgs args)
        {
            var client = args.UserToken as ClientInfo;
            if (client == null)
                return;
            if (args.BytesTransferred > 0)
            {
                if (args.SocketError != SocketError.Success)
                {
                    //这里可能会产生一系列的问题，比如操作取消之类
                    DropClient(client);
                    return;
                }
                try
                {
                    var dbRecieved = args.BytesTransferred;
                    var unpack = new DataUnpack(this, client, null);
                    unpack.ProcessBinaryData(args.Buffer, args.Offset, dbRecieved, ref client.lastDataBuf);
                    WaitForData(args);
                }
                catch (ObjectDisposedException ex)
                {
                    //在被释放的资源上进行操作
                    //WSO.Tools.ErrorLogger.LogException(ex);
                    //_serverInfoSetter.AppendText = ex.Message;
                }
                catch (SocketException socketEx)
                {
                    if (socketEx.ErrorCode == 10054)
                    {
                        DropClient(client); //移除客户端
                    }
                    else
                    {
                        ErrorLogger.LogException(socketEx);
                    }
                }
            }
            else
            {
                //客户端断开连接
                DropClient(client);
            }
        }

        #endregion

        #region 命令处理

        private void ProcessUdpEnd(ClientInfo clientToProcess)
        {
            clientToProcess.IsUdp = false;
        }

        private void ProcessAddPlayer(ClientInfo client, byte[] data)
        {
            //判断人数
            if (currentConnectedNumber > maxConnection)
            {
                SendFull(client);
                return;
            }
            var namelength = data[0];
            var playerName = Encoding.UTF8.GetString(data, 1, namelength);
            playerName = CheckNameDuplicate(playerName, 0);
            client.PlayerName = playerName;
            client.IsClientAdd = true;
            data = null;
            if (!playerName.Equals(hostNickName))
                SendPlayerList(client);
            SendClientId(client);
            SendNotifyData(true, playerName);
        }

        private void ProcessPSPData(ClientInfo client, byte[] data)
        {
            SendDataExceptSender(DataPack.GetPackedData((byte) ClientCode.PSPData, data), client.ClientID);
        }

        private void SendFull(ClientInfo client)
        {
            var id = client.ClientID;
            var iddb = Encoding.UTF8.GetBytes(id);
            SendDataToClient(DataPack.GetPackedData((byte) ClientCode.Full, iddb), client);
            DropClient(client);
        }

        /// <summary>
        ///     发送客户端ID号
        /// </summary>
        /// <param name="clientToSend"></param>
        private void SendClientId(ClientInfo clientToSend)
        {
            //发送客户端ID
            var id = clientToSend.ClientID;
            var iddb = new byte[37];
            Array.Copy(Encoding.UTF8.GetBytes(id), iddb, 36);
            iddb[36] = (byte) requestTimeout;
            SendDataToClient(DataPack.GetPackedData((byte) ClientCode.ClientID, iddb), clientToSend);
        }

        /// <summary>
        ///     发送玩家列表
        /// </summary>
        /// <param name="clientToSend"></param>
        private void SendPlayerList(ClientInfo clientToSend)
        {
            var players = new string[currentConnectedNumber - 1];
            clientLocker.EnterReadLock();
            var count = 0;
            foreach (var kvp in clientList)
            {
                if (!clientToSend.PlayerName.Equals(kvp.Value.PlayerName))
                {
                    players[count] = kvp.Value.PlayerName;
                    count++;
                }
            }
            clientLocker.ExitReadLock();
            var dbfixed = new byte[currentConnectedNumber];
            dbfixed[0] = (byte) (currentConnectedNumber - 1);
            var startpos = 1;
            var allName = string.Empty;
            foreach (var player in players)
            {
                dbfixed[startpos] = (byte) Encoding.UTF8.GetBytes(player).Length;
                startpos++;
                allName += player;
            }
            var dbplayername = Encoding.UTF8.GetBytes(allName);
            var toSend = new byte[dbfixed.Length + dbplayername.Length];
            Array.Copy(dbfixed, toSend, dbfixed.Length);
            Array.Copy(dbplayername, 0, toSend, dbfixed.Length, dbplayername.Length);
            SendDataToClient(DataPack.GetPackedData((byte) ClientCode.PlayerList, toSend), clientToSend);
        }

        private void SendNotifyData(bool isAdd, string playerName)
        {
            var dbname = Encoding.UTF8.GetBytes(playerName);
            var toSend = new byte[dbname.Length + 1];
            var code = isAdd ? ClientCode.AddPlayer : ClientCode.RemovePlayer;
            toSend[0] = (byte) dbname.Length;
            Array.Copy(dbname, 0, toSend, 1, dbname.Length);
            SendDataToAll(DataPack.GetPackedData((byte) code, toSend));
        }

        private void SendChatData(byte[] chatData)
        {
            SendDataToAll(DataPack.GetPackedData((byte) ClientCode.Chat, chatData));
        }

        #endregion

        #region 客户端列表管理函数

        /// <summary>
        ///     检查姓名是否重复，若重复，则在名称后面+1
        /// </summary>
        /// <param name="playerName">玩家姓名</param>
        /// <param name="count">检查次数</param>
        /// <returns></returns>
        private string CheckNameDuplicate(string playerName, int count)
        {
            clientLocker.EnterReadLock();
            var kvp = clientList.FirstOrDefault(cl => cl.Value.PlayerName == playerName);
            clientLocker.ExitReadLock();
            if (kvp.Key == null)
            {
                return playerName;
            }
            if (count != 0)
            {
                playerName = playerName.Substring(0, playerName.Length - 1) + (count + 1);
            }
            else
            {
                playerName = playerName + "1";
            }
            count++;
            return CheckNameDuplicate(playerName, count);
        }

        /// <summary>
        ///     添加客户端到列表
        /// </summary>
        /// <param name="clientToAdd">要添加的客户端</param>
        private void AddClientToList(ClientInfo clientToAdd)
        {
            /*
             *不再使用lock来进行对象锁，而采用读写锁，这样可以提高效率如果在读取时没
             *有发现写锁，则可以多线程读取，采用lock进行锁的话读取也会被锁住
             */
            clientLocker.EnterWriteLock();
            try
            {
                clientList.Add(clientToAdd.ClientID, clientToAdd);
            }
            finally
            {
                clientLocker.ExitWriteLock();
            }
            Interlocked.Increment(ref currentConnectedNumber);
        }

        /// <summary>
        ///     移除客户端并关闭连接
        /// </summary>
        /// <param name="clientID">客户端ID</param>
        /// <exception cref="断开连接的Socket异常"></exception>
        private void DropClient(string clientID)
        {
            if (clientList.ContainsKey(clientID))
            {
                var client = clientList[clientID];
                //从客户端列表中移除
                clientLocker.EnterWriteLock();
                try
                {
                    clientList.Remove(clientID);
                }
                finally
                {
                    clientLocker.ExitWriteLock();
                }
                //减少计数器
                Interlocked.Decrement(ref currentConnectedNumber);

                if (client.IsClientAdd)
                {
                    //通知客户端移除
                    SendNotifyData(false, client.PlayerName);
                }

                //断开连接
                try
                {
                    client.ClientSocket.Shutdown(SocketShutdown.Both);
                }
                catch (Exception ex)
                {
                    ErrorLogger.LogException(ex);
                    //throw new Exception("断开客户端：" + clientID + " 时出现问题，详细请查看错误日志");
                }
                client.ClientSocket.Close();
                //释放资源
                //设置为空以避免在回调函数ProcessReceives当中执行
                client.revcEventArg.UserToken = null;
                argsPool.Push(client.revcEventArg);
                argsPool.Push(client.sendEventArg);
                //这个很关键，要不服务器满员了之后，就不会接受连接了
                semaphoreAcceptedClients.Release();
                client = null;
            }
        }

        /// <summary>
        ///     移除客户端并关闭连接
        /// </summary>
        /// <param name="clientToDrop">客户端信息类</param>
        private void DropClient(ClientInfo clientToDrop)
        {
            if (clientToDrop == null)
            {
                return;
            }
            DropClient(clientToDrop.ClientID);
        }

        /// <summary>
        ///     移除客户端
        /// </summary>
        /// <param name="playerName">游戏名称</param>
        public void CloseClient(string playerName)
        {
            try
            {
                clientLocker.EnterReadLock();
                var kvp = clientList.FirstOrDefault(e => e.Value.PlayerName == playerName);
                clientLocker.ExitReadLock();
                if (kvp.Key != null)
                {
                    DropClient(kvp.Value.ClientID);
                }
            }
            catch
            {
            }
        }

        #endregion

        #region 数据发送函数

        /// <summary>
        ///     向客户端发送数据
        /// </summary>
        /// <param name="data">要发送的二进制数据</param>
        /// <param name="clientToSend">客户端信息类</param>
        public void SendDataToClient(byte[] data, ClientInfo clientToSend)
        {
            if (clientToSend != null)
            {
                clientToSend.SendData(data);
            }
        }

        /// <summary>
        ///     向所有玩家发送数据
        /// </summary>
        /// <param name="data">待发送的二进制数据</param>
        public void SendDataToAll(byte[] data)
        {
            clientLocker.EnterReadLock();
            try
            {
                foreach (var kvp in clientList)
                {
                    kvp.Value.SendData(data);
                }
            }
            finally
            {
                clientLocker.ExitReadLock();
            }
        }

        /// <summary>
        ///     向除了发送者外的所有玩家发送数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="senderId"></param>
        public void SendDataExceptSender(byte[] data, string senderId)
        {
            if (isAsync)
            {
                clientLocker.EnterReadLock();
            }
            else
            {
                clientLocker.EnterWriteLock();
            }
            try
            {
                foreach (var kvp in clientList)
                {
                    if (kvp.Value.ClientID != senderId)
                    {
                        if (kvp.Value.IsUdp)
                        {
                            kvp.Value.SendUdpData(data);
                        }
                        else
                        {
                            kvp.Value.SendData(data);
                        }
                    }
                }
            }
            finally
            {
                if (isAsync)
                {
                    clientLocker.ExitReadLock();
                }
                else
                {
                    clientLocker.ExitWriteLock();
                }
            }
        }

        #endregion

        #region UDP相关函数

        private Socket udpSocket;
        private EndPoint localEndPoint;

        private void InitUdpServer()
        {
            udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            localEndPoint = new IPEndPoint(IPAddress.Any, GlobleSetting.UdpPort);
            udpSocket.Bind(localEndPoint);
            var state = new ServerUdpState();
            Udp_WaitForData(state);
        }

        private void Udp_WaitForData(ServerUdpState state)
        {
            try
            {
                udpSocket.BeginReceiveFrom(state.buffer, 0, ServerUdpState.BufferSize, 0,
                    ref localEndPoint, Udp_ProcessDataRecieved, state);
            }
            catch (ObjectDisposedException odex)
            {
                //do nothing
            }
            catch (Exception ex)
            {
                ErrorLogger.LogException(ex);
            }
        }

        private void Udp_ProcessDataRecieved(IAsyncResult result)
        {
            var state = (ServerUdpState) result.AsyncState;
            if (udpSocket == null)
                return;
            try
            {
                EndPoint temp = new IPEndPoint(IPAddress.Any, 0);
                var recievedCount = udpSocket.EndReceiveFrom(result, ref temp);

                if (recievedCount > 0)
                {
                    var unpack = new DataUnpack(this, null, temp);
                    unpack.ProcessBinaryData(state.buffer, 0, recievedCount, ref state.lastDataBuf);
                }
            }
            catch (ObjectDisposedException ex)
            {
                //在被释放的资源上进行操作
                //WSO.Tools.ErrorLogger.LogException(ex);
                //_serverInfoSetter.AppendText = ex.Message;
            }
            catch (SocketException socketEx)
            {
                if (socketEx.ErrorCode != 10054)
                {
                    ErrorLogger.LogException(socketEx);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogException(ex);
            }
            Udp_WaitForData(state);
        }

        private void ProcessKeepAlive(byte[] data, EndPoint clientEndPoint)
        {
            var clientId = Encoding.UTF8.GetString(data);
            clientLocker.EnterReadLock();
            try
            {
                var client = clientList[clientId];
                client.IsUdp = true;
                client.ClientEndPoint = clientEndPoint;
            }
            finally
            {
                clientLocker.ExitReadLock();
            }
        }

        #endregion

        #region 服务器数据

        /// <summary>
        ///     获取当前连接用户数
        /// </summary>
        public int ConnectedNumber
        {
            get { return currentConnectedNumber; }
        }

        /// <summary>
        ///     获取服务器是否启动
        /// </summary>
        public bool IsServerStarted { get; private set; }

        #endregion
    }

    internal class ServerUdpState
    {
        public const int BufferSize = 8192;
        public byte[] buffer = new byte[BufferSize];
        public byte[] lastDataBuf;
    }
}