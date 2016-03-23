using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml;

namespace TwingHotLink.Tools
{
    internal class ProcessManager
    {
        private List<string> discardProcess;
        private readonly XmlElement root;
        private readonly XmlDocument xmlDoc;

        public ProcessManager()
        {
            xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.Load("process.xml");
                root = xmlDoc.DocumentElement;
                GetDiscardProcessName();
            }
            catch
            {
                throw new Exception("配置文件丢失");
            }
        }

        private void GetDiscardProcessName()
        {
            discardProcess = new List<string>();
            if (root.HasChildNodes)
            {
                for (var i = 0; i < root.ChildNodes.Count; i++)
                {
                    var proName = root.ChildNodes[i].InnerText;
                    discardProcess.Add(proName);
                }
            }
        }

        public void CheckProcess()
        {
            var allPros = Process.GetProcesses();
            var existPros = new List<Process>();

            foreach (var single in discardProcess)
            {
                if (allPros.Any(e => e.ProcessName.ToLower().Contains(single.ToLower())))
                {
                    var temp = allPros.Where(e => e.ProcessName.ToLower().Contains(single.ToLower())).ToList();
                    existPros.AddRange(temp);
                }
            }

            if (existPros.Count > 0)
            {
                var killer = new ProcessKiller(existPros);
                killer.ShowDialog();
            }
        }
    }
}