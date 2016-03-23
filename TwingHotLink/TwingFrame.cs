using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using TwingHotLink.Client;
using TwingHotLink.Server;
using TwingHotLink.Tools;

/***
 * 作者：吴兴华
 * 日期：2010-12-16
 * 最后修改日期：2011-01-04
 * Copryright:StepOn Technology
 * 
 * 2011-01-04：
 * 1.修复一处文字错误
 * 2.修正移除玩家的Bug
 * */

namespace TwingHotLink
{
    public partial class TwingFrame : Form, IMainFrame
    {
        private DataCatcher catcher;

        private GroupChat chat;
        private GameClient client;
        private ThreadSafeSetter connSetter;
        private ThreadSafeSetter ipSetter;
        private ThreadSafeSetter listSetter;
        private PingTool ping;
        private ThreadSafeSetter pingSetter;

        private ThreadSafeSetter pspSetter;
        private ThreadSafeSetter removeSetter;
        private GameServer server;

        public TwingFrame()
        {
            InitializeComponent();
            InitGlobleSetting();
            InitUI();
            InitSetter();
            ProcCheck();
        }

        public void SetMessage(string msg)
        {
            connSetter.Text = msg;
        }

        public void SetPSPMessage(string msg)
        {
            pspSetter.Text = msg;
        }

        public void ShowMessageBox(string caption, string msg)
        {
            MessageBox.Show(msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void ShowMessageBox(string caption, string msg, MessageBoxIcon icon)
        {
            MessageBox.Show(msg, caption, MessageBoxButtons.OK, icon);
        }

        public void ToggleConnectButton()
        {
            try
            {
                if (joinMenu.Text.Equals("加入游戏"))
                {
                    joinMenu.Text = "离开游戏";
                    if (server == null || !server.IsServerStarted)
                    {
                        removeSetter.Enabled = false;
                    }
                }
                else
                {
                    joinMenu.Text = "加入游戏";
                    //这里表示客户端已经关闭
                    removeSetter.Enabled = true;
                    ipSetter.Text = "连接的服务器IP：未连接";
                    ClosePing();
                    CloseCatcher();
                    if (client != null)
                    {
                        client = null;
                    }
                }
            }
            catch
            {
            }
        }

        public void ToggleBuildButton()
        {
            try
            {
                if (buildMenu.Text.Equals("建立游戏"))
                {
                    buildMenu.Text = "关闭游戏";
                    joinMenu.Enabled = false;
                }
                else
                {
                    buildMenu.Text = "建立游戏";
                    joinMenu.Enabled = true;
                }
            }
            catch
            {
            }
        }

        public void AddPlayer(string name)
        {
            listSetter.Add(name);
        }

        public void RemovePlayer(string name)
        {
            listSetter.Remove(name);
        }

        public void RemoveAll()
        {
            listSetter.RemoveAll();
        }

        public void StartCatcher(int requestTimeout)
        {
            catcher = new DataCatcher(this, GlobleSetting.Adapter, requestTimeout);
            catcher.PSPDataCaped += catcher_PSPDataCaped;
            catcher.StartCapture();
        }

        public void StartServer(string playerId, int maxConnection)
        {
            server = new GameServer(this, playerId, maxConnection);
            server.StartServer();
            nameLable.Text = "我的ID：" + playerId;
            if (!GlobleSetting.ServerMode)
            {
                //启动客户端
                StartClient(playerId, "127.0.0.1", false);
            }
        }

        public void StartClient(string playerId, string ipAddress, bool isUdp)
        {
            client = new GameClient(this, isUdp);
            client.DataRecived += client_DataRecived;
            client.Connect(ipAddress, playerId);
            nameLable.Text = "我的ID：" + playerId;
            ipLable.Text = "连接的服务器IP：" + ipAddress;
        }

        public void StartPing(string ipAddress)
        {
            ping = new PingTool(pingSetter, ipAddress);
        }

        #region IMainFrame 成员

        public void SendChat(string msg)
        {
            if (client != null)
            {
                client.SendChatData(msg);
            }
        }

        #endregion

        #region IMainFrame 成员

        public void RecivedChat(string msg)
        {
            if (chat != null && !chat.IsDisposed)
            {
                chat.SetMsg(msg);
            }
        }

        #endregion

        private void ProcCheck()
        {
            var procMan = new ProcessManager();
            procMan.CheckProcess();
        }

        private void InitSetter()
        {
            pspSetter = new ThreadSafeSetter(pspSigLable);
            connSetter = new ThreadSafeSetter(connLable);
            listSetter = new ThreadSafeSetter(playerList);
            removeSetter = new ThreadSafeSetter(removeBtn);
            pingSetter = new ThreadSafeSetter(pingTime);
            ipSetter = new ThreadSafeSetter(ipLable);

            chat = new GroupChat(this);
        }

        private void InitGlobleSetting()
        {
            var setting = new GlobleSetting();
        }

        private void InitUI()
        {
            nameLable.Text = "我的ID：" + GlobleSetting.PlayerID;
        }

        private void buildMenu_Click(object sender, EventArgs e)
        {
            if (buildMenu.Text.Equals("建立游戏"))
            {
                if (!CheckAdapterState())
                {
                    ShowMessageBox("提示", "请在设置中配置正确的网卡", MessageBoxIcon.Information);
                    return;
                }
                var serverFrame = new ServerFrame(this);
                serverFrame.ShowDialog();
            }
            else
            {
                CloseServer();
            }
        }

        private void joinMenu_Click(object sender, EventArgs e)
        {
            if (connLable.Text.Equals("正在连接服务器..."))
            {
                ShowMessageBox("提示", "正在连接中", MessageBoxIcon.Information);
                return;
            }
            if (joinMenu.Text.Equals("加入游戏"))
            {
                if (!CheckAdapterState())
                {
                    ShowMessageBox("提示", "请在设置中配置正确的网卡", MessageBoxIcon.Information);
                    return;
                }
                var clientFrame = new ClientFrame(this);
                clientFrame.ShowDialog();
            }
            else
            {
                CloseClient();
            }
        }

        private void bridgeMenu_Click(object sender, EventArgs e)
        {
            var fileLocation = Environment.CurrentDirectory + "\\bridge.exe";
            Process.Start(fileLocation);
        }

        private void aboutMenu_Click(object sender, EventArgs e)
        {
            var about = new AboutTwing();
            about.ShowDialog();
        }

        private void setMenu_Click(object sender, EventArgs e)
        {
            var setFrame = new SetFrame();
            setFrame.ShowDialog();
        }

        private void catcher_PSPDataCaped(PSPDataArgs e)
        {
            if (client != null)
            {
                client.SendPSPData(e.PSPData);
            }
        }

        private void client_DataRecived(DataRecivedArgs e)
        {
            if (catcher != null)
            {
                catcher.SendData(e.RecievedData);
            }
        }

        /// <summary>
        ///     检测网卡是否选择正确
        /// </summary>
        /// <returns></returns>
        private bool CheckAdapterState()
        {
            if (GlobleSetting.Adapter.Equals("Null"))
            {
                return false;
            }
            try
            {
                var nd = new NetworkDevices();
                var devices = nd.GetDevices();
                if (devices != null)
                {
                    if (!devices.Any(e => e.Name.Equals(GlobleSetting.Adapter)))
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void ClosePing()
        {
            if (ping != null)
            {
                ping.Close();
                ping = null;
            }
        }

        private void CloseServer()
        {
            if (server != null)
            {
                if (server.IsServerStarted)
                {
                    server.CloseServer();
                }
                server = null;
            }
        }

        private void CloseClient()
        {
            if (client != null)
            {
                client.Disconnect();
                client = null;
            }
        }

        private void CloseCatcher()
        {
            if (catcher != null)
            {
                catcher.Close();
                catcher = null;
            }
        }

        private void CloseAll()
        {
            ClosePing();
            CloseCatcher();
            CloseClient();
            CloseServer();
        }

        private void TwingFrame_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseAll();
        }

        private void removeBtn_Click(object sender, EventArgs e)
        {
            server.CloseClient(playerList.SelectedItem.ToString());
        }

        private void chatBtn_Click(object sender, EventArgs e)
        {
            if (chat == null || chat.IsDisposed)
            {
                chat = new GroupChat(this);
            }
            chat.Show();
        }
    }
}