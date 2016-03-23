namespace TwingHotLink
{
    partial class ServerFrame
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
            this.tipsLable = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.playerId = new System.Windows.Forms.TextBox();
            this.buildBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.maxConnection = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.maxConnection)).BeginInit();
            this.SuspendLayout();
            // 
            // tipsLable
            // 
            this.tipsLable.AutoSize = true;
            this.tipsLable.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tipsLable.ForeColor = System.Drawing.Color.Red;
            this.tipsLable.Location = new System.Drawing.Point(17, 9);
            this.tipsLable.Name = "tipsLable";
            this.tipsLable.Size = new System.Drawing.Size(173, 24);
            this.tipsLable.TabIndex = 0;
            this.tipsLable.Text = "如果你是内网用户，确保已经在\r\n路由器中对设置的端口做了映射";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "我的ID";
            // 
            // playerId
            // 
            this.playerId.Location = new System.Drawing.Point(69, 47);
            this.playerId.MaxLength = 15;
            this.playerId.Name = "playerId";
            this.playerId.Size = new System.Drawing.Size(115, 21);
            this.playerId.TabIndex = 1;
            // 
            // buildBtn
            // 
            this.buildBtn.Location = new System.Drawing.Point(66, 112);
            this.buildBtn.Name = "buildBtn";
            this.buildBtn.Size = new System.Drawing.Size(75, 23);
            this.buildBtn.TabIndex = 3;
            this.buildBtn.Text = "确定";
            this.buildBtn.UseVisualStyleBackColor = true;
            this.buildBtn.Click += new System.EventHandler(this.buildBtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "最大人数";
            // 
            // maxConnection
            // 
            this.maxConnection.Location = new System.Drawing.Point(69, 77);
            this.maxConnection.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.maxConnection.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.maxConnection.Name = "maxConnection";
            this.maxConnection.Size = new System.Drawing.Size(39, 21);
            this.maxConnection.TabIndex = 2;
            this.maxConnection.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // ServerFrame
            // 
            this.AcceptButton = this.buildBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(207, 157);
            this.Controls.Add(this.maxConnection);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buildBtn);
            this.Controls.Add(this.playerId);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tipsLable);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(213, 185);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(213, 185);
            this.Name = "ServerFrame";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "建立游戏";
            ((System.ComponentModel.ISupportInitialize)(this.maxConnection)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label tipsLable;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox playerId;
        private System.Windows.Forms.Button buildBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown maxConnection;
    }
}