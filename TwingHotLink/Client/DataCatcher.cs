using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using PcapDotNet.Core;
using PcapDotNet.Packets;
using TwingHotLink.Tools;

/***
 * 作者：吴兴华
 * 日期：2010-12-16
 * 最后修改日期：2010-12-21
 * Copryright:StepOn Technology
 * 
 * 2010-12-21：
 * 1.将线程启动放到本类中进行
 * */

namespace TwingHotLink.Client
{
    /// <summary>
    ///     PSP数据管理类
    /// </summary>
    internal class PSPDataArgs : EventArgs
    {
        public PSPDataArgs(byte[] pspData, int dataLength)
        {
            PSPData = pspData;
            DataLength = dataLength;
        }

        public byte[] PSPData { get; }

        public int DataLength { get; }
    }

    /// <summary>
    ///     PSP数据的捕获和发送类
    /// </summary>
    internal class DataCatcher
    {
        public delegate void PSPDataCapedHandler(PSPDataArgs e);

        private PacketCommunicator communicator;
        private readonly string deviceName;

        private readonly IMainFrame frame;
        private bool isPspFound;
        private DateTime lastRev;

        private Thread pspsigCheck;
        private readonly int requestTimeout = 50;
        private readonly object sendLock = new object();

        private readonly ManualResetEvent startDone;

        public DataCatcher(IMainFrame frame, string deviceName, int requestTimeout)
        {
            this.frame = frame;
            lastRev = DateTime.Now;
            this.deviceName = deviceName;
            this.requestTimeout = requestTimeout;
            startDone = new ManualResetEvent(false);
        }

        public event PSPDataCapedHandler PSPDataCaped;

        /// <summary>
        ///     开启数据捕获
        /// </summary>
        public void StartCapture()
        {
            //开启捕获线程
            ThreadStart catcherts = Start;
            var catcherThread = new Thread(catcherts);
            catcherThread.Start();

            //开启PSP信号检测线程
            ThreadStart checkMethod = CheckPSPSignal;
            pspsigCheck = new Thread(checkMethod);
            pspsigCheck.Start();
        }

        /// <summary>
        ///     开始捕获数据
        /// </summary>
        private void Start()
        {
            try
            {
                IList<LivePacketDevice> allDevices = LivePacketDevice.AllLocalMachine;

                if (allDevices.Count == 0)
                {
                    frame.ShowMessageBox("错误", "未发现可用的网络设备！");
                    startDone.Set();
                    return;
                }
                //选择MS LoopBack Driver
                var selectedDevice = allDevices.FirstOrDefault(ad => ad.Name.Equals(deviceName));
                if (selectedDevice == null)
                {
                    frame.ShowMessageBox("错误", "无法使用选择的网络适配器设备");
                    startDone.Set();
                    return;
                }
                communicator = selectedDevice.Open(65535,
                    PacketDeviceOpenAttributes.Promiscuous | PacketDeviceOpenAttributes.NoCaptureLocal, requestTimeout);

                using (var filter = communicator.CreateFilter("ether proto 35016"))
                {
                    communicator.SetFilter(filter);
                }
                startDone.Set();
                communicator.ReceivePackets(0, ProcessCapturedData);
            }
            catch (Exception ex)
            {
                if (!(ex is InvalidOperationException))
                    ErrorLogger.LogException(ex);
            }
        }

        private void ProcessCapturedData(Packet packet)
        {
            if (!isPspFound)
            {
                isPspFound = true;
                frame.SetPSPMessage("已检测到PSP信号");
            }
            lastRev = packet.Timestamp;
            if (PSPDataCaped != null)
            {
                var rev = packet.Buffer;
                var args = new PSPDataArgs(rev, packet.Length);
                PSPDataCaped(args);
            }
        }

        /// <summary>
        ///     检测PSP信号
        /// </summary>
        private void CheckPSPSignal()
        {
            var span = DateTime.Now - lastRev;
            if (span.Seconds > 5)
            {
                isPspFound = false;
                frame.SetPSPMessage("未检测到PSP信号");
            }
            Thread.Sleep(5000);
            CheckPSPSignal();
        }

        /// <summary>
        ///     发送数据到PSP
        /// </summary>
        /// <param name="data"></param>
        public void SendData(byte[] data)
        {
            try
            {
                lock (sendLock)
                {
                    var p = new Packet(data, DateTime.Now, DataLinkKind.Ethernet);
                    communicator.SendPacket(p);
                }
            }
            catch (Exception ex)
            {
                if (!(ex is NullReferenceException))
                    ErrorLogger.LogException(ex);
            }
        }

        public void Close()
        {
            startDone.WaitOne();
            if (communicator != null)
            {
                communicator.Break();
                communicator.Dispose();
                communicator = null;
            }
            pspsigCheck.Abort();
        }
    }
}