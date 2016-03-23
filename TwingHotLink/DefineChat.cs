using System;
using System.Windows.Forms;
using TwingHotLink.Chat;

namespace TwingHotLink
{
    public partial class DefineChat : Form
    {
        private readonly IChat chat;

        private string oldHotKey = string.Empty;
        private PhraseMapping pm;

        public DefineChat(IChat chat)
        {
            InitializeComponent();
            this.chat = chat;
            LoadPhrase();
        }

        private void LoadPhrase()
        {
            pm = new PhraseMapping();
            var list = pm.PhraseList;

            foreach (var item in list)
            {
                phraseView.Rows.Add(item.Key, item.Value);
            }
        }

        private void phraseView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                //删除
                var result = MessageBox.Show("确定删除短语？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    pm.RemovePhrase(phraseView.Rows[e.RowIndex].Cells[0].Value.ToString());
                    phraseView.Rows.RemoveAt(e.RowIndex);
                }
            }
        }

        private void phraseView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            oldHotKey = phraseView.Rows[e.RowIndex].Cells[0].Value.ToString();
        }

        private void phraseView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                //修改快捷键
                if (!string.IsNullOrEmpty(oldHotKey))
                {
                    var newHotKey = phraseView.Rows[e.RowIndex].Cells[0].Value.ToString().Trim();
                    if (pm.PhraseList.ContainsKey(newHotKey))
                    {
                        MessageBox.Show("快捷键重复", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        phraseView.Rows[e.RowIndex].Cells[0].Value = oldHotKey;
                        return;
                    }
                    pm.UpdataHotkey(oldHotKey.Trim(), newHotKey);
                }
            }
            else if (e.ColumnIndex == 1)
            {
                //修改短语
                if (!string.IsNullOrEmpty(oldHotKey))
                {
                    pm.UpdatePhrase(oldHotKey.Trim(), phraseView.Rows[e.RowIndex].Cells[1].Value.ToString().Trim());
                }
            }
            oldHotKey = string.Empty;
        }

        private void DefineChat_FormClosing(object sender, FormClosingEventArgs e)
        {
            pm.Save();
            chat.LoadPhrase();
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(hotKey.Text.Trim()))
            {
                MessageBox.Show("请输入快捷键", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(phrase.Text.Trim()))
            {
                MessageBox.Show("请输入短语", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (pm.PhraseList.ContainsKey(hotKey.Text.Trim()))
            {
                MessageBox.Show("快捷键重复", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            pm.AddPhrase(hotKey.Text.Trim(), phrase.Text.Trim());
            phraseView.Rows.Add(hotKey.Text.Trim(), phrase.Text.Trim());
            hotKey.Text = "";
            phrase.Text = "";
        }
    }
}