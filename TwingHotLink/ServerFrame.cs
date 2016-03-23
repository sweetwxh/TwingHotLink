using System;
using System.Windows.Forms;
using TwingHotLink.Tools;

namespace TwingHotLink
{
    public partial class ServerFrame : Form
    {
        private readonly IMainFrame frame;

        public ServerFrame(IMainFrame frame)
        {
            InitializeComponent();
            this.frame = frame;
            InitUI();
        }

        private void InitUI()
        {
            playerId.Text = GlobleSetting.PlayerID;
        }

        /// <summary>
        ///     点击建立服务器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buildBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(playerId.Text.Trim()))
            {
                MessageBox.Show("请输入一个ID", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            frame.StartServer(playerId.Text.Trim(), (int) maxConnection.Value);
            Close();
        }
    }
}