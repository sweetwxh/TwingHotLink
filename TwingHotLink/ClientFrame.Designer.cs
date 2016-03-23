namespace TwingHotLink
{
    partial class ClientFrame
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
            this.components = new System.ComponentModel.Container();
            this.tipLable1 = new System.Windows.Forms.Label();
            this.playerId = new System.Windows.Forms.TextBox();
            this.tipLable2 = new System.Windows.Forms.Label();
            this.joinBtn = new System.Windows.Forms.Button();
            this.enableUDP = new System.Windows.Forms.CheckBox();
            this.udpTips = new System.Windows.Forms.ToolTip(this.components);
            this.serverIp = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tipLable1
            // 
            this.tipLable1.AutoSize = true;
            this.tipLable1.Location = new System.Drawing.Point(16, 9);
            this.tipLable1.Name = "tipLable1";
            this.tipLable1.Size = new System.Drawing.Size(41, 12);
            this.tipLable1.TabIndex = 0;
            this.tipLable1.Text = "我的ID";
            // 
            // playerId
            // 
            this.playerId.Location = new System.Drawing.Point(63, 6);
            this.playerId.MaxLength = 15;
            this.playerId.Name = "playerId";
            this.playerId.Size = new System.Drawing.Size(136, 21);
            this.playerId.TabIndex = 1;
            // 
            // tipLable2
            // 
            this.tipLable2.AutoSize = true;
            this.tipLable2.Location = new System.Drawing.Point(4, 41);
            this.tipLable2.Name = "tipLable2";
            this.tipLable2.Size = new System.Drawing.Size(53, 12);
            this.tipLable2.TabIndex = 2;
            this.tipLable2.Text = "服务器IP";
            // 
            // joinBtn
            // 
            this.joinBtn.Location = new System.Drawing.Point(70, 96);
            this.joinBtn.Name = "joinBtn";
            this.joinBtn.Size = new System.Drawing.Size(75, 23);
            this.joinBtn.TabIndex = 4;
            this.joinBtn.Text = "确定";
            this.joinBtn.UseVisualStyleBackColor = true;
            this.joinBtn.Click += new System.EventHandler(this.joinBtn_Click);
            // 
            // enableUDP
            // 
            this.enableUDP.AutoSize = true;
            this.enableUDP.Location = new System.Drawing.Point(74, 71);
            this.enableUDP.Name = "enableUDP";
            this.enableUDP.Size = new System.Drawing.Size(66, 16);
            this.enableUDP.TabIndex = 6;
            this.enableUDP.Text = "开启UDP";
            this.udpTips.SetToolTip(this.enableUDP, "请确保你的防火墙支持NAT穿透，或者你可以对你\r\n设置的UDP端口进行映射，如果开启UDP无法看到\r\n其他玩家，请尝试关闭UDP");
            this.enableUDP.UseVisualStyleBackColor = true;
            // 
            // udpTips
            // 
            this.udpTips.AutomaticDelay = 100;
            this.udpTips.AutoPopDelay = 20000;
            this.udpTips.InitialDelay = 100;
            this.udpTips.ReshowDelay = 20;
            // 
            // serverIp
            // 
            this.serverIp.Location = new System.Drawing.Point(63, 38);
            this.serverIp.Name = "serverIp";
            this.serverIp.Size = new System.Drawing.Size(136, 21);
            this.serverIp.TabIndex = 7;
            // 
            // ClientFrame
            // 
            this.AcceptButton = this.joinBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(214, 142);
            this.Controls.Add(this.serverIp);
            this.Controls.Add(this.enableUDP);
            this.Controls.Add(this.joinBtn);
            this.Controls.Add(this.tipLable2);
            this.Controls.Add(this.playerId);
            this.Controls.Add(this.tipLable1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(220, 170);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(220, 170);
            this.Name = "ClientFrame";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "加入游戏";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label tipLable1;
        private System.Windows.Forms.TextBox playerId;
        private System.Windows.Forms.Label tipLable2;
        private System.Windows.Forms.Button joinBtn;
        private System.Windows.Forms.CheckBox enableUDP;
        private System.Windows.Forms.ToolTip udpTips;
        private System.Windows.Forms.TextBox serverIp;
    }
}