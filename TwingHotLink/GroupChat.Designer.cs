namespace TwingHotLink
{
    partial class GroupChat
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GroupChat));
            this.chatMenuStrip = new System.Windows.Forms.MenuStrip();
            this.defineItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.clearBtn = new System.Windows.Forms.Button();
            this.sendBtn = new System.Windows.Forms.Button();
            this.userArea = new System.Windows.Forms.TextBox();
            this.globleArea = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.phraseListBox = new System.Windows.Forms.ListBox();
            this.chatMenuStrip.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // chatMenuStrip
            // 
            this.chatMenuStrip.BackColor = System.Drawing.Color.Transparent;
            this.chatMenuStrip.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("chatMenuStrip.BackgroundImage")));
            this.chatMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.defineItem});
            this.chatMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.chatMenuStrip.Name = "chatMenuStrip";
            this.chatMenuStrip.Size = new System.Drawing.Size(662, 25);
            this.chatMenuStrip.TabIndex = 0;
            this.chatMenuStrip.Text = "ChatMenu";
            // 
            // defineItem
            // 
            this.defineItem.Name = "defineItem";
            this.defineItem.Size = new System.Drawing.Size(92, 21);
            this.defineItem.Text = "自定义快捷键";
            this.defineItem.Click += new System.EventHandler(this.defineItem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.clearBtn);
            this.groupBox1.Controls.Add(this.sendBtn);
            this.groupBox1.Controls.Add(this.userArea);
            this.groupBox1.Controls.Add(this.globleArea);
            this.groupBox1.Location = new System.Drawing.Point(12, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(420, 383);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "聊天";
            // 
            // clearBtn
            // 
            this.clearBtn.Location = new System.Drawing.Point(334, 312);
            this.clearBtn.Name = "clearBtn";
            this.clearBtn.Size = new System.Drawing.Size(75, 23);
            this.clearBtn.TabIndex = 3;
            this.clearBtn.Text = "清空";
            this.clearBtn.UseVisualStyleBackColor = true;
            this.clearBtn.Click += new System.EventHandler(this.clearBtn_Click);
            // 
            // sendBtn
            // 
            this.sendBtn.Location = new System.Drawing.Point(334, 354);
            this.sendBtn.Name = "sendBtn";
            this.sendBtn.Size = new System.Drawing.Size(75, 23);
            this.sendBtn.TabIndex = 1;
            this.sendBtn.Text = "发送";
            this.sendBtn.UseVisualStyleBackColor = true;
            this.sendBtn.Click += new System.EventHandler(this.sendBtn_Click);
            // 
            // userArea
            // 
            this.userArea.Location = new System.Drawing.Point(6, 312);
            this.userArea.MaxLength = 500;
            this.userArea.Multiline = true;
            this.userArea.Name = "userArea";
            this.userArea.Size = new System.Drawing.Size(322, 65);
            this.userArea.TabIndex = 0;
            this.userArea.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.userArea_KeyPress);
            // 
            // globleArea
            // 
            this.globleArea.BackColor = System.Drawing.Color.White;
            this.globleArea.Location = new System.Drawing.Point(6, 20);
            this.globleArea.Multiline = true;
            this.globleArea.Name = "globleArea";
            this.globleArea.ReadOnly = true;
            this.globleArea.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.globleArea.Size = new System.Drawing.Size(408, 286);
            this.globleArea.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.phraseListBox);
            this.groupBox2.Location = new System.Drawing.Point(438, 28);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(212, 383);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "快捷短语";
            // 
            // phraseListBox
            // 
            this.phraseListBox.DisplayMember = "Show";
            this.phraseListBox.FormattingEnabled = true;
            this.phraseListBox.ItemHeight = 12;
            this.phraseListBox.Location = new System.Drawing.Point(6, 20);
            this.phraseListBox.Name = "phraseListBox";
            this.phraseListBox.Size = new System.Drawing.Size(200, 352);
            this.phraseListBox.TabIndex = 0;
            this.phraseListBox.ValueMember = "Hotkey";
            this.phraseListBox.DoubleClick += new System.EventHandler(this.phraseListBox_DoubleClick);
            // 
            // GroupChat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(662, 421);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.chatMenuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.chatMenuStrip;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GroupChat";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Twing 聊天室";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GroupChat_FormClosing);
            this.chatMenuStrip.ResumeLayout(false);
            this.chatMenuStrip.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip chatMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem defineItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button sendBtn;
        private System.Windows.Forms.TextBox userArea;
        private System.Windows.Forms.TextBox globleArea;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button clearBtn;
        private System.Windows.Forms.ListBox phraseListBox;

    }
}