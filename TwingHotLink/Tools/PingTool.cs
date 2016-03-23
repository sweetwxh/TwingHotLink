using System;
using System.Net.NetworkInformation;
using System.Threading;

namespace TwingHotLink.Tools
{
    internal class PingTool
    {
        private readonly AutoResetEvent doneEvent = new AutoResetEvent(false);
        private Ping ping = new Ping();
        private readonly Thread pingThread;
        private readonly int pingTimeout = GlobleSetting.PingTimeout;
        private readonly string serverIp;
        private readonly ThreadSafeSetter setter;

        public PingTool(ThreadSafeSetter setter, string serverIp)
        {
            this.setter = setter;
            this.serverIp = serverIp;
            ping.PingCompleted += ping_PingCompleted;
            ThreadStart pingStarter = Start;
            pingThread = new Thread(pingStarter);
            pingThread.Start();
        }

        private void Start()
        {
            try
            {
                while (ping != null)
                {
                    ping.SendAsync(serverIp, null);
                    Thread.Sleep(pingTimeout);
                    doneEvent.WaitOne();
                }
            }
            catch (Exception ex)
            {
                //ErrorLogger.LogException(ex);
            }
        }

        private void ping_PingCompleted(object sender, PingCompletedEventArgs e)
        {
            var reply = e.Reply;
            if (reply != null)
            {
                if (reply.Status == IPStatus.Success)
                {
                    setter.Text = "延时：" + reply.RoundtripTime + "ms";
                }
            }
            doneEvent.Set();
        }

        public void Close()
        {
            if (ping != null)
            {
                ping.Dispose();
                ping = null;
            }
            pingThread.Abort();
        }
    }
}