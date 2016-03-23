using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using TwingHotLink.Common;
using TwingHotLink.Server;
using TwingHotLink.Tools;

/***
 * 作者：吴兴华
 * 日期：2010-12-16
 * 最后修改日期：2011-01-04
 * Copryright:StepOn Technology
 * 
 * 2010-12-21：
 * 1.增加了UDP相关协议，并实现NAT穿透
 * 
 * 2011-01-04：
 * 1.优化代码
 * */

namespace TwingHotLink.Client
{
    internal enum ClientCode
    {
        AddPlayer = 0,
        RemovePlayer = 1,
        PlayerList = 2,
        PSPData = 3,
        ClientID = 4,
        Full = 5,
        Chat = 6
    }

    /// <summary>
    ///     数据接收事件
    /// </summary>
    internal class DataRecivedArgs : EventArgs
    {
        public DataRecivedArgs(byte[] recievedData)
        {
            RecievedData = recievedData;
        }

        public byte[] RecievedData { get; }
    }

    internal class StateObject
    {
        public byte[] buffer = new byte[GlobleSetting.BufferSize];
        public int BufferSize = GlobleSetting.BufferSize;
        public byte[] lastDataBuf;
    }

    internal class ConnectState
    {
        public string ipAddress;
        public string playerName;
        public Socket socket;
    }

    internal class GameClient : ICMDProcessor
    {
        public void ProcessCMD(byte code, byte[] data, ClientInfo client, EndPoint clientEndPoint)
        {
            var ccode = (ClientCode) code;
            switch (ccode)
            {
                case ClientCode.AddPlayer:
                    ProcessPlayer(data, true);
                    break;
                case ClientCode.RemovePlayer:
                    ProcessPlayer(data, false);
                    break;
                case ClientCode.PlayerList:
                    ProcessPlayerList(data);
                    break;
                case ClientCode.ClientID:
                    ProcessClientId(data);
                    break;
                case ClientCode.Full:
                    ProcessFull();
                    break;
                case ClientCode.PSPData:
                    if (DataRecived != null)
                    {
                        var pspdata = data;
                        var dra = new DataRecivedArgs(pspdata);
                        DataRecived(dra);
                    }
                    break;
                case ClientCode.Chat:
                    var msg = Encoding.UTF8.GetString(data);
                    frame.RecivedChat(msg);
                    break;
            }
        }

        #region 成员变量

        private Socket client;

        private byte[] rvhead = new byte[4];

        private readonly IMainFrame frame;

        public delegate void DataRecievedHandler(DataRecivedArgs e);

        public event DataRecievedHandler DataRecived;

        //private string playerName;
        private string clientId;

        private bool isUdp;
        private Socket udpSocket;
        private EndPoint localEndPoint;
        private EndPoint serverEndPoint;
        private readonly int keepAliveTimeout = 30000; //默认30秒
        private Thread keepAliveThread;

        #endregion

        #region 构造函数

        public GameClient(IMainFrame frame)
        {
            this.frame = frame;
        }

        public GameClient(IMainFrame frame, bool isUdp)
        {
            this.frame = frame;
            this.isUdp = isUdp;
        }

        public GameClient(IMainFrame frame, bool isUdp, int keepAliveTimeout)
        {
            this.frame = frame;
            this.isUdp = isUdp;
            this.keepAliveTimeout = keepAliveTimeout;
        }

        #endregion

        #region UDP相关函数

        private void StartUdp()
        {
            //设置服务器的EndPoint
            var tempPoint = client.RemoteEndPoint as IPEndPoint;
            serverEndPoint = new IPEndPoint(tempPoint.Address, GlobleSetting.UdpPort);

            //启动UDP监听
            udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            localEndPoint = new IPEndPoint(IPAddress.Any, GlobleSetting.ClientPort);
            udpSocket.Bind(localEndPoint);
            var state = new StateObject();
            Udp_WaitForData(state);

            //启动NAT穿透线程
            ThreadStart keepAlivets = KeepAlive;
            keepAliveThread = new Thread(keepAlivets);
            keepAliveThread.Start();
        }

        private void Udp_WaitForData(StateObject state)
        {
            try
            {
                udpSocket.BeginReceiveFrom(state.buffer, 0, state.BufferSize, 0,
                    ref localEndPoint, Udp_ProcessDataRecieved, state);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogException(ex);
            }
        }

        /// <summary>
        ///     处理UDP数据，UDP仅接收PSP数据
        /// </summary>
        /// <param name="result"></param>
        private void Udp_ProcessDataRecieved(IAsyncResult result)
        {
            var state = (StateObject) result.AsyncState;
            if (!isUdp || udpSocket == null)
            {
                return;
            }
            try
            {
                EndPoint temp = new IPEndPoint(IPAddress.Any, 0);
                var recievedCount = udpSocket.EndReceiveFrom(result, ref temp);
                if (recievedCount > 0)
                {
                    var unpack = new DataUnpack(this, null, null);
                    unpack.ProcessBinaryData(state.buffer, 0, recievedCount, ref state.lastDataBuf);
                    Udp_WaitForData(state);
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
                SendUdpEnd();
            }
            catch (Exception ex)
            {
                ErrorLogger.LogException(ex);
                SendUdpEnd();
            }
        }

        private void KeepAlive()
        {
            if (isUdp)
            {
                var iddb = Encoding.UTF8.GetBytes(clientId);
                var toSend = DataPack.GetPackedData((byte) ServerCode.KeepAlive, iddb);
                udpSocket.BeginSendTo(toSend, 0, toSend.Length, 0, serverEndPoint, null, null);
                Thread.Sleep(keepAliveTimeout);
            }
        }

        #endregion

        #region 连接管理相关函数

        /// <summary>
        ///     连接服务器
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="playerName"></param>
        public void Connect(string ipAddress, string playerName)
        {
            Connect(ipAddress, GlobleSetting.TcpPort, playerName);
        }

        /// <summary>
        ///     连接服务器
        /// </summary>
        /// <param name="ipAddress">IP地址</param>
        /// <param name="port">端口号</param>
        /// <param name="playerName">玩家姓名</param>
        public void Connect(string ipAddress, int port, string playerName)
        {
            if (client != null && client.Connected)
            {
                return;
            }
            client = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
            frame.SetMessage("正在连接服务器...");
            try
            {
                var state = new ConnectState();
                state.socket = client;
                state.ipAddress = ipAddress;
                state.playerName = playerName;
                client.BeginConnect(ipAddress, port, ConnectComplete, state);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogException(ex);
                frame.ShowMessageBox("连接错误", "尝试连接服务器失败，请重新尝试");
                if (client != null)
                {
                    client.Close();
                    client = null;
                }
                frame.SetMessage("未连接或建立服务器");
            }
        }

        private void ConnectComplete(IAsyncResult result)
        {
            try
            {
                var connState = (ConnectState) result.AsyncState;
                var resultSocket = connState.socket;
                resultSocket.EndConnect(result);
                if (resultSocket.Connected)
                {
                    frame.SetMessage("连接服务器成功");
                    frame.StartPing(connState.ipAddress);
                    frame.ToggleConnectButton();
                    var state = new StateObject();
                    Tcp_WaitForData(state);
                    //发送连接请求数据
                    var dbname = Encoding.UTF8.GetBytes(connState.playerName);
                    var tosend = new byte[dbname.Length + 1];
                    tosend[0] = (byte) dbname.Length;
                    Array.Copy(dbname, 0, tosend, 1, dbname.Length);
                    SendData(DataPack.GetPackedData((byte) ServerCode.AddPlayer, tosend));
                }
            }
            catch (SocketException ex)
            {
                if (ex.ErrorCode == 10061)
                {
                    frame.ShowMessageBox("连接失败", "服务器拒绝连接，可能由于目标服务器存在防火墙或未做端口映射");
                }
                if (ex.ErrorCode == 10060)
                {
                    frame.ShowMessageBox("连接失败", "无法建立指定IP地址的连接，请确保端口号和IP地址填写正确");
                }
                if (client != null)
                {
                    client.Close();
                    client = null;
                }
                frame.SetMessage("未连接或建立服务器");
            }
        }

        /// <summary>
        ///     断开连接
        /// </summary>
        public void Disconnect()
        {
            if (client != null && client.Connected)
            {
                client.Close();
                client = null;
            }
            if (isUdp && udpSocket != null)
            {
                if (keepAliveThread != null)
                {
                    keepAliveThread.Abort();
                }
                udpSocket.Close();
                udpSocket = null;
            }
            frame.RemoveAll();
            frame.SetMessage("未连接或建立服务器");
            frame.ToggleConnectButton();
        }

        #endregion

        #region TCP相关函数

        private void Tcp_WaitForData(StateObject state)
        {
            try
            {
                if (client != null && client.Connected)
                {
                    client.BeginReceive(state.buffer, 0, state.BufferSize, 0,
                        Tcp_ProcessDataRecieved, state);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogException(ex);
            }
        }

        private void Tcp_ProcessDataRecieved(IAsyncResult result)
        {
            var state = (StateObject) result.AsyncState;
            if (client == null || !client.Connected)
            {
                //没有连接的话，则不处理
                return;
            }
            try
            {
                var recievedCount = client.EndReceive(result);
                if (recievedCount > 0)
                {
                    var unpack = new DataUnpack(this, null, null);
                    unpack.ProcessBinaryData(state.buffer, 0, recievedCount, ref state.lastDataBuf);
                    Tcp_WaitForData(state);
                }
                else
                {
                    //断开连接
                    Disconnect();
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
                Disconnect(); //移除客户端
            }
            catch (Exception ex)
            {
                ErrorLogger.LogException(ex);
                Disconnect();
            }
        }

        #endregion

        #region 命令处理

        private void SendUdpEnd()
        {
            isUdp = false;
            var toSend = DataPack.GetPackedData((byte) ServerCode.UdpEnd, Encoding.UTF8.GetBytes(clientId));
            SendData(toSend);
        }

        private void ProcessFull()
        {
            frame.ShowMessageBox("提示", "服务器人数已满", MessageBoxIcon.Information);
        }

        private void ProcessClientId(byte[] data)
        {
            var clientId = Encoding.UTF8.GetString(data, 0, data.Length - 1);
            this.clientId = clientId;
            frame.StartCatcher(data[36]);
            if (isUdp)
            {
                StartUdp();
            }
        }

        private void ProcessPlayer(byte[] data, bool isAdd)
        {
            var nameLength = data[0];
            var playerName = Encoding.UTF8.GetString(data, 1, nameLength);
            if (isAdd)
            {
                frame.AddPlayer(playerName);
            }
            else
            {
                frame.RemovePlayer(playerName);
            }
        }

        private void ProcessPlayerList(byte[] data)
        {
            int playerCount = data[0];
            var fixedlength = 1 + playerCount;
            byte[] single = null;
            var namepos = fixedlength;
            for (var i = 1; i < fixedlength; i++)
            {
                var namelength = data[i];
                single = new byte[namelength];
                Array.Copy(data, namepos, single, 0, namelength);
                var name = Encoding.UTF8.GetString(single);
                frame.AddPlayer(name);
                namepos += namelength;
            }
        }

        #endregion

        #region 数据发送

        /// <summary>
        ///     发送数据
        /// </summary>
        /// <param name="data"></param>
        public void SendData(byte[] data)
        {
            if (client != null && client.Connected)
            {
                client.BeginSend(data, 0, data.Length, 0, null, null);
            }
        }

        public void SendPSPData(byte[] data)
        {
            SendData(DataPack.GetPackedData((byte) ServerCode.PSPData, data));
        }

        public void SendChatData(string msg)
        {
            var chatData = Encoding.UTF8.GetBytes(msg);
            SendData(DataPack.GetPackedData((byte) ServerCode.Chat, chatData));
        }

        #endregion
    }
}