namespace TwingHotLink
{
    partial class DefineChat
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.phraseView = new System.Windows.Forms.DataGridView();
            this.HotKeyColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PhraseColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DelBtn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.hotKey = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.phrase = new System.Windows.Forms.TextBox();
            this.addBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.phraseView)).BeginInit();
            this.SuspendLayout();
            // 
            // phraseView
            // 
            this.phraseView.AllowUserToAddRows = false;
            this.phraseView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.phraseView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.phraseView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.HotKeyColumn,
            this.PhraseColumn,
            this.DelBtn});
            this.phraseView.Location = new System.Drawing.Point(12, 74);
            this.phraseView.Name = "phraseView";
            this.phraseView.RowTemplate.Height = 23;
            this.phraseView.Size = new System.Drawing.Size(328, 315);
            this.phraseView.TabIndex = 3;
            this.phraseView.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.phraseView_CellBeginEdit);
            this.phraseView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.phraseView_CellContentClick);
            this.phraseView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.phraseView_CellEndEdit);
            // 
            // HotKeyColumn
            // 
            this.HotKeyColumn.HeaderText = "快捷键";
            this.HotKeyColumn.MaxInputLength = 10;
            this.HotKeyColumn.Name = "HotKeyColumn";
            this.HotKeyColumn.Width = 70;
            // 
            // PhraseColumn
            // 
            this.PhraseColumn.HeaderText = "短语";
            this.PhraseColumn.MaxInputLength = 50;
            this.PhraseColumn.Name = "PhraseColumn";
            this.PhraseColumn.Width = 160;
            // 
            // DelBtn
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.NullValue = "删除";
            this.DelBtn.DefaultCellStyle = dataGridViewCellStyle5;
            this.DelBtn.HeaderText = "";
            this.DelBtn.Name = "DelBtn";
            this.DelBtn.Text = "删除";
            this.DelBtn.Width = 50;
            // 
            // hotKey
            // 
            this.hotKey.Location = new System.Drawing.Point(76, 12);
            this.hotKey.Name = "hotKey";
            this.hotKey.Size = new System.Drawing.Size(100, 21);
            this.hotKey.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "快捷键";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "短语";
            // 
            // phrase
            // 
            this.phrase.Location = new System.Drawing.Point(76, 47);
            this.phrase.Name = "phrase";
            this.phrase.Size = new System.Drawing.Size(188, 21);
            this.phrase.TabIndex = 1;
            // 
            // addBtn
            // 
            this.addBtn.Location = new System.Drawing.Point(270, 45);
            this.addBtn.Name = "addBtn";
            this.addBtn.Size = new System.Drawing.Size(54, 23);
            this.addBtn.TabIndex = 2;
            this.addBtn.Text = "新增";
            this.addBtn.UseVisualStyleBackColor = true;
            this.addBtn.Click += new System.EventHandler(this.addBtn_Click);
            // 
            // DefineChat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 401);
            this.Controls.Add(this.addBtn);
            this.Controls.Add(this.phrase);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.hotKey);
            this.Controls.Add(this.phraseView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DefineChat";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "自定义快捷短语";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DefineChat_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.phraseView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView phraseView;
        private System.Windows.Forms.TextBox hotKey;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox phrase;
        private System.Windows.Forms.Button addBtn;
        private System.Windows.Forms.DataGridViewTextBoxColumn HotKeyColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn PhraseColumn;
        private System.Windows.Forms.DataGridViewButtonColumn DelBtn;

    }
}