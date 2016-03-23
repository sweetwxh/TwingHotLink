using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace TwingHotLink
{
    public partial class ProcessKiller : Form
    {
        private readonly List<Process> existProc;

        public ProcessKiller(List<Process> existProc)
        {
            InitializeComponent();
            this.existProc = existProc;
            ShowProcess();
        }

        private void ShowProcess()
        {
            foreach (var pro in existProc)
            {
                var info = new ProcInfo(pro.Id, pro.ProcessName);
                procList.Items.Add(info);
            }
        }

        private void killSingleBtn_Click(object sender, EventArgs e)
        {
            if (procList.SelectedIndex < 0)
            {
                return;
            }
            var info = procList.SelectedItem as ProcInfo;
            var pro = existProc.First(ep => ep.Id == info.Id);
            try
            {
                pro.Kill();
            }
            catch
            {
            }
            existProc.Remove(pro);
            procList.Items.Remove(info);
        }

        private void killAllBtn_Click(object sender, EventArgs e)
        {
            foreach (var proc in existProc)
            {
                try
                {
                    proc.Kill();
                }
                catch
                {
                }
            }
            existProc.Clear();
            procList.Items.Clear();
        }
    }

    internal class ProcInfo
    {
        public ProcInfo(int id, string procName)
        {
            Id = id;
            ProcName = procName;
        }

        public int Id { get; }

        public string ProcName { get; }
    }
}