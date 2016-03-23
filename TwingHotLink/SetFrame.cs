using System;
using System.Linq;
using System.Windows.Forms;
using TwingHotLink.Tools;

namespace TwingHotLink
{
    public partial class SetFrame : Form
    {
        public SetFrame()
        {
            InitializeComponent();
            ReadAdapterList();
            ReadSettings();
        }

        /// <summary>
        ///     读取可用的网卡列表
        /// </summary>
        private void ReadAdapterList()
        {
            try
            {
                var nd = new NetworkDevices();
                var devices = nd.GetDevices();
                if (devices != null)
                {
                    /*
                    foreach (DevicesData device in devices)
                    {
                        adapterList.Items.Add(device);
                    }
                     * */
                    adapterList.DataSource = devices;
                    if (GlobleSetting.Adapter.Equals("Null"))
                    {
                        //如果为设置网卡，则默认选取第一个
                        adapterList.SelectedIndex = -1;
                    }
                    else
                    {
                        if (devices.Any(e => e.Name.Equals(GlobleSetting.Adapter)))
                        {
                            adapterList.SelectedValue = GlobleSetting.Adapter;
                        }
                        else
                        {
                            adapterList.SelectedIndex = -1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ReadSettings()
        {
            playerId.Text = GlobleSetting.PlayerID;
            pingTimeout.Value = GlobleSetting.PingTimeout;
            tcpPort.Text = GlobleSetting.TcpPort.ToString();
            udpPort.Text = GlobleSetting.UdpPort.ToString();
            clientUdp.Text = GlobleSetting.ClientPort.ToString();
            requestTimeout.Value = GlobleSetting.RequestTimeout;
            if (GlobleSetting.IsAsync)
            {
                AsyncMode.Checked = true;
            }
            else
            {
                SyncMode.Checked = true;
            }
            syncCheck.Checked = GlobleSetting.SyncMode;
        }

        /// <summary>
        ///     更改滑块值事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void requestTimeout_ValueChanged(object sender, EventArgs e)
        {
            currentRTValue.Text = "当前设定值：" + requestTimeout.Value;
        }

        /// <summary>
        ///     保存设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void applyBtn_Click(object sender, EventArgs e)
        {
            //检测输入
            var tcpPortValue = int.Parse(tcpPort.Text.Trim());
            var udpPortValue = int.Parse(udpPort.Text.Trim());
            var clientPortValue = int.Parse(clientUdp.Text.Trim());

            if (tcpPortValue < 1000 || tcpPortValue > 65535)
            {
                MessageBox.Show("值范围为1000-65535", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (udpPortValue < 1000 || udpPortValue > 65535)
            {
                MessageBox.Show("值范围为1000-65535", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (clientPortValue < 1000 || clientPortValue > 65535)
            {
                MessageBox.Show("值范围为1000-65535", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            GlobleSetting.Adapter = adapterList.SelectedValue.ToString();
            GlobleSetting.PlayerID = playerId.Text.Trim();
            GlobleSetting.PingTimeout = (int) pingTimeout.Value;
            GlobleSetting.TcpPort = tcpPortValue;
            GlobleSetting.UdpPort = udpPortValue;
            GlobleSetting.ClientPort = clientPortValue;
            GlobleSetting.RequestTimeout = requestTimeout.Value;
            GlobleSetting.IsAsync = AsyncMode.Checked;
            GlobleSetting.SyncMode = syncCheck.Checked;

            GlobleSetting.SaveSetting();

            MessageBox.Show("保存设置成功，请重新建立游戏或加入游戏", "设置成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }

        private void numOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar > 57 || (e.KeyChar > 8 && e.KeyChar < 47) || e.KeyChar < 8)
            {
                e.Handled = true;
            }
        }
    }
}