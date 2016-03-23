namespace TwingHotLink
{
    partial class TwingFrame
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TwingFrame));
            this.menuToolBar = new System.Windows.Forms.MenuStrip();
            this.buildMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.joinMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.bridgeMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.setMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.playerList = new System.Windows.Forms.ListBox();
            this.stcLable1 = new System.Windows.Forms.Label();
            this.AdBrowser = new System.Windows.Forms.WebBrowser();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.IPBrowser = new System.Windows.Forms.WebBrowser();
            this.nameLable = new System.Windows.Forms.Label();
            this.ipLable = new System.Windows.Forms.Label();
            this.iconBox_psp = new System.Windows.Forms.PictureBox();
            this.pspSigLable = new System.Windows.Forms.Label();
            this.iconBox_server = new System.Windows.Forms.PictureBox();
            this.connLable = new System.Windows.Forms.Label();
            this.bottomPanel = new System.Windows.Forms.Panel();
            this.pingTime = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.removeBtn = new System.Windows.Forms.Button();
            this.chatBtn = new System.Windows.Forms.Button();
            this.menuToolBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconBox_psp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconBox_server)).BeginInit();
            this.bottomPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuToolBar
            // 
            this.menuToolBar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("menuToolBar.BackgroundImage")));
            this.menuToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buildMenu,
            this.joinMenu,
            this.bridgeMenu,
            this.setMenu,
            this.aboutMenu});
            this.menuToolBar.Location = new System.Drawing.Point(0, 0);
            this.menuToolBar.Name = "menuToolBar";
            this.menuToolBar.Size = new System.Drawing.Size(360, 25);
            this.menuToolBar.TabIndex = 0;
            // 
            // buildMenu
            // 
            this.buildMenu.Name = "buildMenu";
            this.buildMenu.Size = new System.Drawing.Size(68, 21);
            this.buildMenu.Text = "建立游戏";
            this.buildMenu.Click += new System.EventHandler(this.buildMenu_Click);
            // 
            // joinMenu
            // 
            this.joinMenu.Name = "joinMenu";
            this.joinMenu.Size = new System.Drawing.Size(68, 21);
            this.joinMenu.Text = "加入游戏";
            this.joinMenu.Click += new System.EventHandler(this.joinMenu_Click);
            // 
            // bridgeMenu
            // 
            this.bridgeMenu.Name = "bridgeMenu";
            this.bridgeMenu.Size = new System.Drawing.Size(83, 21);
            this.bridgeMenu.Text = "启动Bridge";
            this.bridgeMenu.Click += new System.EventHandler(this.bridgeMenu_Click);
            // 
            // setMenu
            // 
            this.setMenu.Name = "setMenu";
            this.setMenu.Size = new System.Drawing.Size(44, 21);
            this.setMenu.Text = "设置";
            this.setMenu.Click += new System.EventHandler(this.setMenu_Click);
            // 
            // aboutMenu
            // 
            this.aboutMenu.Name = "aboutMenu";
            this.aboutMenu.Size = new System.Drawing.Size(78, 21);
            this.aboutMenu.Text = "关于Twing";
            this.aboutMenu.Click += new System.EventHandler(this.aboutMenu_Click);
            // 
            // playerList
            // 
            this.playerList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.playerList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.playerList.FormattingEnabled = true;
            this.playerList.ItemHeight = 12;
            this.playerList.Location = new System.Drawing.Point(12, 57);
            this.playerList.Name = "playerList";
            this.playerList.Size = new System.Drawing.Size(106, 132);
            this.playerList.TabIndex = 0;
            // 
            // stcLable1
            // 
            this.stcLable1.AutoSize = true;
            this.stcLable1.Font = new System.Drawing.Font("微软雅黑", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.stcLable1.Location = new System.Drawing.Point(3, 31);
            this.stcLable1.Name = "stcLable1";
            this.stcLable1.Size = new System.Drawing.Size(69, 19);
            this.stcLable1.TabIndex = 0;
            this.stcLable1.Text = "玩家列表";
            // 
            // AdBrowser
            // 
            this.AdBrowser.AllowNavigation = false;
            this.AdBrowser.Location = new System.Drawing.Point(0, 226);
            this.AdBrowser.Margin = new System.Windows.Forms.Padding(0);
            this.AdBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.AdBrowser.Name = "AdBrowser";
            this.AdBrowser.ScrollBarsEnabled = false;
            this.AdBrowser.Size = new System.Drawing.Size(360, 90);
            this.AdBrowser.TabIndex = 0;
            this.AdBrowser.Url = new System.Uri("http://www.ngjoy.com/AdPublish.html", System.UriKind.Absolute);
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Enabled = false;
            this.vScrollBar1.Location = new System.Drawing.Point(121, 25);
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(18, 201);
            this.vScrollBar1.TabIndex = 0;
            // 
            // IPBrowser
            // 
            this.IPBrowser.AllowNavigation = false;
            this.IPBrowser.Location = new System.Drawing.Point(152, 32);
            this.IPBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.IPBrowser.Name = "IPBrowser";
            this.IPBrowser.ScrollBarsEnabled = false;
            this.IPBrowser.Size = new System.Drawing.Size(189, 20);
            this.IPBrowser.TabIndex = 0;
            this.IPBrowser.Url = new System.Uri("http://www.ngjoy.com/ShowIP.aspx", System.UriKind.Absolute);
            // 
            // nameLable
            // 
            this.nameLable.AutoSize = true;
            this.nameLable.Location = new System.Drawing.Point(150, 56);
            this.nameLable.Name = "nameLable";
            this.nameLable.Size = new System.Drawing.Size(89, 12);
            this.nameLable.TabIndex = 0;
            this.nameLable.Text = "我的ID：无名氏";
            // 
            // ipLable
            // 
            this.ipLable.AutoSize = true;
            this.ipLable.Location = new System.Drawing.Point(150, 78);
            this.ipLable.Name = "ipLable";
            this.ipLable.Size = new System.Drawing.Size(137, 12);
            this.ipLable.TabIndex = 0;
            this.ipLable.Text = "连接的服务器IP：未连接";
            // 
            // iconBox_psp
            // 
            this.iconBox_psp.Image = ((System.Drawing.Image)(resources.GetObject("iconBox_psp.Image")));
            this.iconBox_psp.Location = new System.Drawing.Point(156, 155);
            this.iconBox_psp.Name = "iconBox_psp";
            this.iconBox_psp.Size = new System.Drawing.Size(17, 17);
            this.iconBox_psp.TabIndex = 4;
            this.iconBox_psp.TabStop = false;
            // 
            // pspSigLable
            // 
            this.pspSigLable.AutoSize = true;
            this.pspSigLable.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pspSigLable.Location = new System.Drawing.Point(179, 153);
            this.pspSigLable.Name = "pspSigLable";
            this.pspSigLable.Size = new System.Drawing.Size(135, 21);
            this.pspSigLable.TabIndex = 0;
            this.pspSigLable.Text = "未检测到PSP信号";
            // 
            // iconBox_server
            // 
            this.iconBox_server.Image = ((System.Drawing.Image)(resources.GetObject("iconBox_server.Image")));
            this.iconBox_server.Location = new System.Drawing.Point(153, 185);
            this.iconBox_server.Name = "iconBox_server";
            this.iconBox_server.Size = new System.Drawing.Size(24, 23);
            this.iconBox_server.TabIndex = 5;
            this.iconBox_server.TabStop = false;
            // 
            // connLable
            // 
            this.connLable.AutoSize = true;
            this.connLable.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.connLable.Location = new System.Drawing.Point(179, 186);
            this.connLable.Name = "connLable";
            this.connLable.Size = new System.Drawing.Size(154, 21);
            this.connLable.TabIndex = 0;
            this.connLable.Text = "未连接或建立服务器";
            // 
            // bottomPanel
            // 
            this.bottomPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(254)))));
            this.bottomPanel.Controls.Add(this.pingTime);
            this.bottomPanel.Controls.Add(this.pictureBox1);
            this.bottomPanel.Location = new System.Drawing.Point(0, 318);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Size = new System.Drawing.Size(360, 40);
            this.bottomPanel.TabIndex = 0;
            // 
            // pingTime
            // 
            this.pingTime.AutoSize = true;
            this.pingTime.ForeColor = System.Drawing.Color.Red;
            this.pingTime.Location = new System.Drawing.Point(246, 19);
            this.pingTime.Name = "pingTime";
            this.pingTime.Size = new System.Drawing.Size(59, 12);
            this.pingTime.TabIndex = 0;
            this.pingTime.Text = "延迟：0ms";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(6, 6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(219, 29);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // removeBtn
            // 
            this.removeBtn.Location = new System.Drawing.Point(53, 195);
            this.removeBtn.Name = "removeBtn";
            this.removeBtn.Size = new System.Drawing.Size(65, 23);
            this.removeBtn.TabIndex = 6;
            this.removeBtn.Text = "移除玩家";
            this.removeBtn.UseVisualStyleBackColor = true;
            this.removeBtn.Click += new System.EventHandler(this.removeBtn_Click);
            // 
            // chatBtn
            // 
            this.chatBtn.Location = new System.Drawing.Point(5, 195);
            this.chatBtn.Name = "chatBtn";
            this.chatBtn.Size = new System.Drawing.Size(47, 23);
            this.chatBtn.TabIndex = 7;
            this.chatBtn.Text = "聊天";
            this.chatBtn.UseVisualStyleBackColor = true;
            this.chatBtn.Click += new System.EventHandler(this.chatBtn_Click);
            // 
            // TwingFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.ClientSize = new System.Drawing.Size(360, 359);
            this.Controls.Add(this.chatBtn);
            this.Controls.Add(this.removeBtn);
            this.Controls.Add(this.bottomPanel);
            this.Controls.Add(this.connLable);
            this.Controls.Add(this.iconBox_server);
            this.Controls.Add(this.pspSigLable);
            this.Controls.Add(this.iconBox_psp);
            this.Controls.Add(this.ipLable);
            this.Controls.Add(this.nameLable);
            this.Controls.Add(this.IPBrowser);
            this.Controls.Add(this.vScrollBar1);
            this.Controls.Add(this.AdBrowser);
            this.Controls.Add(this.stcLable1);
            this.Controls.Add(this.playerList);
            this.Controls.Add(this.menuToolBar);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(39)))), ((int)(((byte)(91)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuToolBar;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(376, 397);
            this.MinimumSize = new System.Drawing.Size(376, 397);
            this.Name = "TwingFrame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Twing Hot Link For PSP Ver 1.2.0";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TwingFrame_FormClosing);
            this.menuToolBar.ResumeLayout(false);
            this.menuToolBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconBox_psp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconBox_server)).EndInit();
            this.bottomPanel.ResumeLayout(false);
            this.bottomPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuToolBar;
        private System.Windows.Forms.ToolStripMenuItem buildMenu;
        private System.Windows.Forms.ToolStripMenuItem joinMenu;
        private System.Windows.Forms.ToolStripMenuItem setMenu;
        private System.Windows.Forms.ListBox playerList;
        private System.Windows.Forms.Label stcLable1;
        private System.Windows.Forms.WebBrowser AdBrowser;
        private System.Windows.Forms.VScrollBar vScrollBar1;
        private System.Windows.Forms.WebBrowser IPBrowser;
        private System.Windows.Forms.Label nameLable;
        private System.Windows.Forms.Label ipLable;
        private System.Windows.Forms.PictureBox iconBox_psp;
        private System.Windows.Forms.Label pspSigLable;
        private System.Windows.Forms.PictureBox iconBox_server;
        private System.Windows.Forms.Label connLable;
        private System.Windows.Forms.Panel bottomPanel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label pingTime;
        private System.Windows.Forms.Button removeBtn;
        private System.Windows.Forms.ToolStripMenuItem aboutMenu;
        private System.Windows.Forms.ToolStripMenuItem bridgeMenu;
        private System.Windows.Forms.Button chatBtn;
    }
}