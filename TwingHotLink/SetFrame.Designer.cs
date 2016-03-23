namespace TwingHotLink
{
    partial class SetFrame
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
            this.label1 = new System.Windows.Forms.Label();
            this.adapterList = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.playerId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.clientUdp = new System.Windows.Forms.TextBox();
            this.udpPort = new System.Windows.Forms.TextBox();
            this.tcpPort = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pingTimeout = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.toolTips = new System.Windows.Forms.ToolTip(this.components);
            this.requestTimeout = new System.Windows.Forms.TrackBar();
            this.AsyncMode = new System.Windows.Forms.RadioButton();
            this.SyncMode = new System.Windows.Forms.RadioButton();
            this.syncCheck = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.currentRTValue = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.applyBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pingTimeout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.requestTimeout)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "联机使用的网卡";
            // 
            // adapterList
            // 
            this.adapterList.DisplayMember = "Description";
            this.adapterList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.adapterList.FormattingEnabled = true;
            this.adapterList.Location = new System.Drawing.Point(12, 31);
            this.adapterList.Name = "adapterList";
            this.adapterList.Size = new System.Drawing.Size(300, 20);
            this.adapterList.TabIndex = 1;
            this.adapterList.ValueMember = "Name";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.playerId);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(14, 66);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(298, 53);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "个人设置";
            // 
            // playerId
            // 
            this.playerId.Location = new System.Drawing.Point(57, 20);
            this.playerId.MaxLength = 15;
            this.playerId.Name = "playerId";
            this.playerId.Size = new System.Drawing.Size(133, 21);
            this.playerId.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "我的ID";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.clientUdp);
            this.groupBox2.Controls.Add(this.udpPort);
            this.groupBox2.Controls.Add(this.tcpPort);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.pingTimeout);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(14, 125);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(298, 145);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "网络参数";
            // 
            // clientUdp
            // 
            this.clientUdp.Location = new System.Drawing.Point(99, 111);
            this.clientUdp.MaxLength = 5;
            this.clientUdp.Name = "clientUdp";
            this.clientUdp.Size = new System.Drawing.Size(91, 21);
            this.clientUdp.TabIndex = 4;
            this.toolTips.SetToolTip(this.clientUdp, "此端口号可任意填写一个未被占用的端口号，如果你是\r\n局域网用户，请确保你的路由器支持NAT穿透或者已经\r\n映射了此端口到你的本机IP。可在连接服务器时启动UDP");
            this.clientUdp.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numOnly_KeyPress);
            // 
            // udpPort
            // 
            this.udpPort.Location = new System.Drawing.Point(99, 81);
            this.udpPort.MaxLength = 5;
            this.udpPort.Name = "udpPort";
            this.udpPort.Size = new System.Drawing.Size(91, 21);
            this.udpPort.TabIndex = 3;
            this.toolTips.SetToolTip(this.udpPort, "如果你在局域网环境中开启服务器，请映射此端口到你\r\n的本机IP，如果你加入服务器，请联系建立服务器的人\r\n确认端口号，默认为9121。");
            this.udpPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numOnly_KeyPress);
            // 
            // tcpPort
            // 
            this.tcpPort.Location = new System.Drawing.Point(99, 51);
            this.tcpPort.MaxLength = 5;
            this.tcpPort.Name = "tcpPort";
            this.tcpPort.Size = new System.Drawing.Size(91, 21);
            this.tcpPort.TabIndex = 2;
            this.toolTips.SetToolTip(this.tcpPort, "如果你在局域网环境中开启服务器，请映射此端口到你\r\n的本机IP，如果你加入服务器，请联系建立服务器的人\r\n确认端口号，默认为9120。");
            this.tcpPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numOnly_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 114);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "客户端UDP端口";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 84);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "服务器UDP端口";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "服务器TCP端口";
            // 
            // pingTimeout
            // 
            this.pingTimeout.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.pingTimeout.Location = new System.Drawing.Point(99, 20);
            this.pingTimeout.Maximum = new decimal(new int[] {
            30000,
            0,
            0,
            0});
            this.pingTimeout.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.pingTimeout.Name = "pingTimeout";
            this.pingTimeout.Size = new System.Drawing.Size(75, 21);
            this.pingTimeout.TabIndex = 1;
            this.pingTimeout.Value = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(40, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "Ping间隔";
            // 
            // toolTips
            // 
            this.toolTips.AutomaticDelay = 100;
            this.toolTips.AutoPopDelay = 20000;
            this.toolTips.InitialDelay = 100;
            this.toolTips.ReshowDelay = 20;
            // 
            // requestTimeout
            // 
            this.requestTimeout.Location = new System.Drawing.Point(57, 20);
            this.requestTimeout.Maximum = 100;
            this.requestTimeout.Minimum = 1;
            this.requestTimeout.Name = "requestTimeout";
            this.requestTimeout.Size = new System.Drawing.Size(235, 45);
            this.requestTimeout.TabIndex = 1;
            this.toolTips.SetToolTip(this.requestTimeout, "流畅度值设定越低，越流畅，但是对服务器的网络带宽要求\r\n也更高，如果设置过低，会导致严重的延迟从而掉线，请根\r\n据自己的带宽合理设置（仅建立服务器要求设置）");
            this.requestTimeout.Value = 50;
            this.requestTimeout.ValueChanged += new System.EventHandler(this.requestTimeout_ValueChanged);
            // 
            // AsyncMode
            // 
            this.AsyncMode.AutoSize = true;
            this.AsyncMode.Location = new System.Drawing.Point(64, 94);
            this.AsyncMode.Name = "AsyncMode";
            this.AsyncMode.Size = new System.Drawing.Size(95, 16);
            this.AsyncMode.TabIndex = 4;
            this.AsyncMode.TabStop = true;
            this.AsyncMode.Text = "无线网卡模式";
            this.toolTips.SetToolTip(this.AsyncMode, "无线网卡模式为异步数据发送模式，服务器\r\n处理效率高，但是要求带宽比较大，可以保\r\n证联机的效果（仅建立服务器要求设置）\r\n");
            this.AsyncMode.UseVisualStyleBackColor = true;
            // 
            // SyncMode
            // 
            this.SyncMode.AutoSize = true;
            this.SyncMode.Location = new System.Drawing.Point(165, 94);
            this.SyncMode.Name = "SyncMode";
            this.SyncMode.Size = new System.Drawing.Size(89, 16);
            this.SyncMode.TabIndex = 5;
            this.SyncMode.TabStop = true;
            this.SyncMode.Text = "USB增强模式";
            this.toolTips.SetToolTip(this.SyncMode, "USB增强模式为同步数据发送模式，服务器\r\n处理效率较低，主要为保证USB联机的稳定\r\n性，采用USB联机请尽量使用USB2.0，并且\r\n关闭PSP的节能模式（仅" +
                    "建立服务器要求设置）");
            this.SyncMode.UseVisualStyleBackColor = true;
            // 
            // syncCheck
            // 
            this.syncCheck.AutoSize = true;
            this.syncCheck.Location = new System.Drawing.Point(64, 128);
            this.syncCheck.Name = "syncCheck";
            this.syncCheck.Size = new System.Drawing.Size(120, 16);
            this.syncCheck.TabIndex = 7;
            this.syncCheck.Text = "启用带宽节省模式";
            this.toolTips.SetToolTip(this.syncCheck, "带宽节省模式尽量降低服务器端的带宽使用量，\r\n以提高联机的稳定性。推荐低于（或等于）4M\r\n的用户在建立服务器时开启，该模式牺牲服务\r\n器处理效率换取稳定性。启用" +
                    "该模式配合USB增\r\n强模式，可最大化的节省带宽资源（仅建立服务\r\n器要求设置）。");
            this.syncCheck.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.syncCheck);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.SyncMode);
            this.groupBox3.Controls.Add(this.AsyncMode);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.currentRTValue);
            this.groupBox3.Controls.Add(this.requestTimeout);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Location = new System.Drawing.Point(14, 276);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(298, 159);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "其他设定";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(22, 129);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 12);
            this.label9.TabIndex = 6;
            this.label9.Text = "带宽";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(22, 96);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 3;
            this.label8.Text = "模式";
            // 
            // currentRTValue
            // 
            this.currentRTValue.AutoSize = true;
            this.currentRTValue.Location = new System.Drawing.Point(197, 68);
            this.currentRTValue.Name = "currentRTValue";
            this.currentRTValue.Size = new System.Drawing.Size(89, 12);
            this.currentRTValue.TabIndex = 2;
            this.currentRTValue.Text = "当前设定值：50";
            this.currentRTValue.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 26);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "流畅度";
            // 
            // applyBtn
            // 
            this.applyBtn.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.applyBtn.Location = new System.Drawing.Point(76, 447);
            this.applyBtn.Name = "applyBtn";
            this.applyBtn.Size = new System.Drawing.Size(75, 23);
            this.applyBtn.TabIndex = 5;
            this.applyBtn.Text = "保存";
            this.applyBtn.UseVisualStyleBackColor = true;
            this.applyBtn.Click += new System.EventHandler(this.applyBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBtn.Location = new System.Drawing.Point(173, 447);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 6;
            this.cancelBtn.Text = "取消";
            this.cancelBtn.UseVisualStyleBackColor = true;
            // 
            // SetFrame
            // 
            this.AcceptButton = this.applyBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelBtn;
            this.ClientSize = new System.Drawing.Size(324, 479);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.applyBtn);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.adapterList);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetFrame";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "设置";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pingTimeout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.requestTimeout)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox adapterList;
        private System.Windows.Forms.TextBox playerId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown pingTimeout;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox clientUdp;
        private System.Windows.Forms.TextBox udpPort;
        private System.Windows.Forms.TextBox tcpPort;
        private System.Windows.Forms.ToolTip toolTips;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TrackBar requestTimeout;
        private System.Windows.Forms.Label currentRTValue;
        private System.Windows.Forms.Button applyBtn;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.RadioButton SyncMode;
        private System.Windows.Forms.RadioButton AsyncMode;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox syncCheck;
        private System.Windows.Forms.Label label9;
    }
}