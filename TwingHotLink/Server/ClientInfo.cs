using System;
using System.Net;
using System.Net.Sockets;
using TwingHotLink.Tools;

/***
 * 作者：吴兴华
 * 日期：2010-12-16
 * 最后修改日期：2011-01-04
 * Copryright:StepOn Technology
 * 
 * 2011-01-04：
 * 1.增加了同步发送模式的选择，以便节省上行带宽占用量
 * */

namespace TwingHotLink.Server
{
    /// <summary>
    ///     客户端信息
    /// </summary>
    internal class ClientInfo
    {
        /// <summary>
        ///     客户端Socket
        /// </summary>
        private readonly Socket _clientSocket;

        /// <summary>
        ///     客户端ID（GUID）
        /// </summary>
        private readonly string _clientID = string.Empty;

        /// <summary>
        ///     最后一次发送的剩余数据缓存，16kb
        /// </summary>
        public byte[] lastDataBuf = null;

        //数据发送模式设定
        private readonly object sendLock = new object();

        private readonly Action<byte[]> tcpSendFunc;
        private readonly Action<byte[]> udpSendFunc;

        /// <summary>
        ///     初始化客户端类
        /// </summary>
        /// <param name="clientSocket">该客户端的Socket端口</param>
        /// <param name="recvArg">接收数据参数</param>
        /// <param name="sendArg">发送数据参数</param>
        public ClientInfo(Socket clientSocket, SocketAsyncEventArgs recvArg, SocketAsyncEventArgs sendArg)
        {
            _clientSocket = clientSocket;
            _clientID = Guid.NewGuid().ToString();
            revcEventArg = recvArg;
            sendEventArg = sendArg;
            InitUdp();
            if (GlobleSetting.SyncMode)
            {
                tcpSendFunc = SendSyncData;
                udpSendFunc = SendSyncUdpData;
                _clientSocket.SendTimeout = GlobleSetting.SendTimeout;
                udpSocket.SendTimeout = GlobleSetting.SendTimeout;
            }
            else
            {
                tcpSendFunc = SendAsynData;
                udpSendFunc = SendAsynUdpData;
            }
        }

        /// <summary>
        ///     接受数据缓存，8KB(数据池)
        /// </summary>
        public byte[] sendBuffer { get; set; }

        /// <summary>
        ///     发送数据缓存，8kb(数据池)
        /// </summary>
        public byte[] recvBuffer { get; set; }

        /// <summary>
        ///     接受的SocketAsyncEventArgs
        /// </summary>
        public SocketAsyncEventArgs revcEventArg { get; private set; }

        /// <summary>
        ///     发送数据的SocketAsyncEventArgs
        /// </summary>
        public SocketAsyncEventArgs sendEventArg { get; private set; }

        /// <summary>
        ///     玩家姓名
        /// </summary>
        public string PlayerName { get; set; }

        /// <summary>
        ///     玩家地址
        /// </summary>
        public string PlayerAddress { get; set; }

        /// <summary>
        ///     获取客户端Socket
        /// </summary>
        public Socket ClientSocket
        {
            get { return _clientSocket; }
        }

        /// <summary>
        ///     客户端ID，连接时动态生成
        /// </summary>
        public string ClientID
        {
            get { return _clientID; }
        }

        /// <summary>
        ///     客户端是否已发送请求信息
        /// </summary>
        public bool IsClientAdd { get; set; }

        /// <summary>
        ///     发送数据到客户端
        /// </summary>
        /// <param name="data">需要发送的二进制数据</param>
        /// <exception cref="发送数据Socket异常"></exception>
        public void SendData(byte[] data)
        {
            if (_clientSocket.Connected)
            {
                try
                {
                    tcpSendFunc(data);
                }
                catch (Exception ex)
                {
                    ErrorLogger.LogException(ex);
                }
            }
        }

        private void SendSyncData(byte[] data)
        {
            lock (sendLock)
            {
                _clientSocket.Send(data);
            }
        }

        private void SendAsynData(byte[] data)
        {
            /**
             * 这里采用异步发送，而非SAEA。因为SAEA在同一客户端实例当中只允许一个
             * SAEA被使用，如果要用SAEA，则需进行同步处理，在客户端数量很少的情况
             * 下，可以采用普通的异步发送
             * **/
            //Array.Copy(data, 0, sendBuffer, sendEventArg.Offset, data.Length);
            //sendEventArg.SetBuffer(sendEventArg.Offset, data.Length);
            //this._clientSocket.SendAsync(sendEventArg);
            _clientSocket.BeginSend(data, 0, data.Length, 0, null, null);
        }

        #region UDP相关函数

        private Socket udpSocket;

        private void InitUdp()
        {
            udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        }

        /// <summary>
        ///     发送UDP数据
        /// </summary>
        /// <param name="data"></param>
        public void SendUdpData(byte[] data)
        {
            try
            {
                udpSendFunc(data);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogException(ex);
            }
        }

        private void SendSyncUdpData(byte[] data)
        {
            lock (sendLock)
            {
                udpSocket.SendTo(data, ClientEndPoint);
            }
        }

        private void SendAsynUdpData(byte[] data)
        {
            udpSocket.BeginSendTo(data, 0, data.Length, 0, ClientEndPoint, null, null);
        }

        /// <summary>
        ///     客户端是否开启UDP接收
        /// </summary>
        public bool IsUdp { get; set; } = false;

        /// <summary>
        ///     设置客户端终端
        /// </summary>
        public EndPoint ClientEndPoint { get; set; }

        #endregion
    }
}