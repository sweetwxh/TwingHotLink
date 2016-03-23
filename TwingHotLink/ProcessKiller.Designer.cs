namespace TwingHotLink
{
    partial class ProcessKiller
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
            this.procList = new System.Windows.Forms.ListBox();
            this.killSingleBtn = new System.Windows.Forms.Button();
            this.killAllBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // procList
            // 
            this.procList.DisplayMember = "ProcName";
            this.procList.FormattingEnabled = true;
            this.procList.ItemHeight = 12;
            this.procList.Location = new System.Drawing.Point(12, 12);
            this.procList.Name = "procList";
            this.procList.Size = new System.Drawing.Size(274, 244);
            this.procList.TabIndex = 0;
            this.procList.ValueMember = "Id";
            // 
            // killSingleBtn
            // 
            this.killSingleBtn.Location = new System.Drawing.Point(37, 262);
            this.killSingleBtn.Name = "killSingleBtn";
            this.killSingleBtn.Size = new System.Drawing.Size(100, 23);
            this.killSingleBtn.TabIndex = 1;
            this.killSingleBtn.Text = "结束选定进程";
            this.killSingleBtn.UseVisualStyleBackColor = true;
            this.killSingleBtn.Click += new System.EventHandler(this.killSingleBtn_Click);
            // 
            // killAllBtn
            // 
            this.killAllBtn.Location = new System.Drawing.Point(161, 262);
            this.killAllBtn.Name = "killAllBtn";
            this.killAllBtn.Size = new System.Drawing.Size(100, 23);
            this.killAllBtn.TabIndex = 2;
            this.killAllBtn.Text = "结束所有进程";
            this.killAllBtn.UseVisualStyleBackColor = true;
            this.killAllBtn.Click += new System.EventHandler(this.killAllBtn_Click);
            // 
            // ProcessKiller
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(298, 300);
            this.Controls.Add(this.killAllBtn);
            this.Controls.Add(this.killSingleBtn);
            this.Controls.Add(this.procList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProcessKiller";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "进程提示 - 建议结束以保证联机稳定性";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox procList;
        private System.Windows.Forms.Button killSingleBtn;
        private System.Windows.Forms.Button killAllBtn;
    }
}