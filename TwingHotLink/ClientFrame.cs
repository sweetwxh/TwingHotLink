using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using TwingHotLink.Tools;

namespace TwingHotLink
{
    public partial class ClientFrame : Form
    {
        private readonly IMainFrame frame;

        public ClientFrame(IMainFrame frame)
        {
            InitializeComponent();
            this.frame = frame;
            InitUI();
        }

        private void InitUI()
        {
            playerId.Text = GlobleSetting.PlayerID;
            serverIp.Text = GlobleSetting.LastAddress;
        }

        private void joinBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(playerId.Text.Trim()))
            {
                MessageBox.Show("请输入一个ID", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //判断IP地址
            var ipRegex =
                new Regex(
                    @"^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$");
            if (!ipRegex.IsMatch(serverIp.Text.Trim()))
            {
                MessageBox.Show("IP地址格式不正确", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            frame.StartClient(playerId.Text.Trim(), serverIp.Text.Trim(), enableUDP.Checked);
            GlobleSetting.PlayerID = playerId.Text.Trim();
            GlobleSetting.LastAddress = serverIp.Text.Trim();
            GlobleSetting.SaveAddress();
            Close();
        }
    }
}