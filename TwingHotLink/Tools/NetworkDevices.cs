using System;
using System.Collections.Generic;
using PcapDotNet.Core;

/***
 * 作者：吴兴华
 * 日期：2010-12-16
 * 最后修改日期：2010-12-21
 * Copryright:StepOn Technology
 * */

namespace TwingHotLink.Tools
{
    /// <summary>
    ///     网卡驱动获取工具
    /// </summary>
    internal class NetworkDevices
    {
        public DevicesData[] GetDevices()
        {
            IList<LivePacketDevice> allDevices = LivePacketDevice.AllLocalMachine;

            if (allDevices.Count == 0)
            {
                throw new Exception("未发现可用的网络设备！");
            }
            var devices = new DevicesData[allDevices.Count];
            for (var i = 0; i < devices.Length; i++)
            {
                var device =
                    new DevicesData(
                        allDevices[i].Description.Replace("Network adapter '", "").Replace("' on local host", ""),
                        allDevices[i].Name);
                devices[i] = device;
            }
            return devices;
        }
    }

    internal class DevicesData
    {
        public DevicesData(string description, string name)
        {
            Description = description;
            Name = name;
        }

        public string Description { get; }

        public string Name { get; }
    }
}