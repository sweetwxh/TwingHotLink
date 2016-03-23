using System;
using System.Collections.Generic;
using System.Media;
using System.Windows.Forms;
using TwingHotLink.Chat;
using TwingHotLink.Properties;
using TwingHotLink.Tools;

namespace TwingHotLink
{
    public partial class GroupChat : Form, IChat
    {
        private readonly AppendChatAreaDelegate appendChatArea;
        private readonly IMainFrame frame;

        private SortedDictionary<string, string> phraseList;
        private readonly SoundPlayer sound;

        public GroupChat(IMainFrame frame)
        {
            InitializeComponent();
            sound = new SoundPlayer(Resources.msg);
            this.frame = frame;
            appendChatArea = AppendChatArea;
            LoadPhrase();
        }

        public void LoadPhrase()
        {
            var pm = new PhraseMapping();
            phraseList = pm.PhraseList;

            //清空快捷列表
            phraseListBox.Items.Clear();
            foreach (var phrase in phraseList)
            {
                var item = new PhraseItem(phrase.Key, phrase.Key + " - " + phrase.Value);
                phraseListBox.Items.Add(item);
            }
        }

        private void userArea_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SendToServer();
                e.Handled = true;
            }
        }

        private void SendToServer()
        {
            if (string.IsNullOrEmpty(userArea.Text.Trim()))
            {
                return;
            }
            var msg = string.Empty;
            if (phraseList.ContainsKey(userArea.Text.Trim()))
            {
                msg = phraseList[userArea.Text.Trim()];
            }
            else
            {
                msg = userArea.Text.Trim();
            }
            var toSend = GlobleSetting.PlayerID + " " + string.Format("{0:HH:mm:ss}", DateTime.Now) + "\r\n" + msg;
            frame.SendChat(toSend);
            userArea.Text = "";
            userArea.Focus();
        }

        public void SetMsg(string msg)
        {
            msg += "\r\n\r\n";
            AppendChatArea(msg);
            sound.Play();
        }

        private void AppendChatArea(string msg)
        {
            if (globleArea.InvokeRequired)
            {
                globleArea.BeginInvoke(appendChatArea, msg);
            }
            else
            {
                globleArea.Text += msg;
                globleArea.SelectionStart = globleArea.Text.Length;
                globleArea.SelectionLength = 0;
                globleArea.ScrollToCaret();
            }
        }

        private void sendBtn_Click(object sender, EventArgs e)
        {
            SendToServer();
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            globleArea.Text = "";
        }

        private void GroupChat_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            e.Cancel = true;
        }

        private void phraseListBox_DoubleClick(object sender, EventArgs e)
        {
            userArea.Text = ((PhraseItem) phraseListBox.SelectedItem).Hotkey;
            SendToServer();
        }

        /// <summary>
        ///     打开定义窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void defineItem_Click(object sender, EventArgs e)
        {
            var define = new DefineChat(this);
            define.ShowDialog();
        }

        private delegate void AppendChatAreaDelegate(string msg);
    }

    internal class PhraseItem
    {
        public PhraseItem(string hotKey, string show)
        {
            Hotkey = hotKey;
            Show = show;
        }

        public string Hotkey { get; }

        public string Show { get; }
    }
}