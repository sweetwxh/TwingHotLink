using System.Windows.Forms;

namespace TwingHotLink.Tools
{
    internal class ThreadSafeSetter
    {
        private readonly AddDelegate add;
        private readonly GetEnabledDelegate getEnabled;
        private readonly GetSelectedItemDelegate getSelectedItem;
        private readonly GetTextDelegate getText;
        private readonly RemoveDelegate remove;
        private readonly RemoveAllDelegate removeAll;
        private readonly SetAppendTextDelegate setAppendText;
        private readonly SetEnabledDelegate setEnabled;

        private readonly SetTextDelegate setText;

        private readonly Control target;

        /// <summary>
        ///     初始化线程安全类
        /// </summary>
        /// <param name="ctrl">需要进行线程安全的控件</param>
        public ThreadSafeSetter(Control ctrl)
        {
            target = ctrl;
            setText = SetText;
            setAppendText = SetAppendText;
            getText = GetText;
            add = Add;
            remove = Remove;
            removeAll = RemoveAll;
            setEnabled = SetEnabled;
            getEnabled = GetEnabled;
            getSelectedItem = GetSelectedItem;
        }

        /// <summary>
        ///     追加文本到末尾
        /// </summary>
        public string AppendText
        {
            set { SetAppendText(value); }
        }

        /// <summary>
        ///     设置或获取控件的文本
        /// </summary>
        public string Text
        {
            set { SetText(value); }
            get { return GetText(); }
        }

        public bool Enabled
        {
            set { SetEnabled(value); }
            get { return GetEnabled(); }
        }

        public string GetSelectedItem()
        {
            if (target.InvokeRequired)
            {
                var result = target.BeginInvoke(getSelectedItem);
                return (string) target.EndInvoke(result);
            }
            return ((ComboBox) target).SelectedItem.ToString();
        }

        public int Add(object item)
        {
            if (target.InvokeRequired)
            {
                var result = target.BeginInvoke(add, item);
                return (int) target.EndInvoke(result);
            }
            return ((ListBox) target).Items.Add(item);
        }

        public void Remove(object value)
        {
            if (target.InvokeRequired)
            {
                target.BeginInvoke(remove, value);
            }
            else
            {
                ((ListBox) target).Items.Remove(value);
            }
        }

        public void RemoveAll()
        {
            if (target.InvokeRequired)
            {
                target.BeginInvoke(removeAll);
            }
            else
            {
                ((ListBox) target).Items.Clear();
            }
        }

        /// <summary>
        ///     设置追加文本
        /// </summary>
        /// <param name="text">要追加的文本</param>
        private void SetAppendText(string text)
        {
            if (target.InvokeRequired)
            {
                target.BeginInvoke(setAppendText, text);
            }
            else
            {
                if (!text.Equals(""))
                {
                    text += "\r\n";
                    ((RichTextBox) target).AppendText(text);
                    ((RichTextBox) target).Select(((RichTextBox) target).Text.Length, 0);
                    ((RichTextBox) target).ScrollToCaret();
                }
            }
        }

        /// <summary>
        ///     设置文本
        /// </summary>
        /// <param name="text">要设置的文本</param>
        private void SetText(string text)
        {
            if (target.InvokeRequired)
            {
                target.BeginInvoke(setText, text);
            }
            else
            {
                target.Text = text;
            }
        }

        /// <summary>
        ///     获取文本
        /// </summary>
        /// <returns>文本</returns>
        private string GetText()
        {
            if (target.InvokeRequired)
            {
                var result = target.BeginInvoke(getText);
                return (string) target.EndInvoke(result);
            }
            return target.Text;
        }

        private void SetEnabled(bool isEnabled)
        {
            if (target.InvokeRequired)
            {
                target.BeginInvoke(setEnabled, isEnabled);
            }
            else
            {
                target.Enabled = isEnabled;
            }
        }

        private bool GetEnabled()
        {
            if (target.InvokeRequired)
            {
                var result = target.BeginInvoke(getEnabled);
                return (bool) target.EndInvoke(result);
            }
            return target.Enabled;
        }

        private delegate void SetTextDelegate(string text);

        private delegate void SetAppendTextDelegate(string text);

        private delegate string GetTextDelegate();

        private delegate int AddDelegate(object item);

        private delegate void RemoveDelegate(object value);

        private delegate void RemoveAllDelegate();

        private delegate void SetEnabledDelegate(bool isEnabled);

        private delegate bool GetEnabledDelegate();

        private delegate string GetSelectedItemDelegate();
    }
}