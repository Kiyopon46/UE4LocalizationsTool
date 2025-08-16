namespace UELocalizationsTool
{
    partial class FrmFilter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFilter));
            this.button1 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Add = new System.Windows.Forms.Button();
            this.RemoveSelected = new System.Windows.Forms.Button();
            this.ClearList = new System.Windows.Forms.Button();
            this.matchcase = new System.Windows.Forms.CheckBox();
            this.BtnClose = new System.Windows.Forms.Button();
            this.regularexpression = new System.Windows.Forms.CheckBox();
            this.reversemode = new System.Windows.Forms.CheckBox();
            this.Columns = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ColumnPanel = new System.Windows.Forms.Panel();
            this.ColumnPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(348, 278);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 28);
            this.button1.TabIndex = 1;
            this.button1.Text = "Apply";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(16, 31);
            this.listBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.listBox1.Name = "listBox1";
            this.listBox1.ScrollAlwaysVisible = true;
            this.listBox1.Size = new System.Drawing.Size(228, 244);
            this.listBox1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Filter Values:";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(304, 31);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(275, 22);
            this.textBox1.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(255, 34);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "Input:";
            // 
            // Add
            // 
            this.Add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Add.Location = new System.Drawing.Point(588, 28);
            this.Add.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Add.Name = "Add";
            this.Add.Size = new System.Drawing.Size(153, 28);
            this.Add.TabIndex = 6;
            this.Add.Text = "Add";
            this.Add.UseVisualStyleBackColor = true;
            this.Add.Click += new System.EventHandler(this.Add_Click);
            // 
            // RemoveSelected
            // 
            this.RemoveSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RemoveSelected.Location = new System.Drawing.Point(588, 64);
            this.RemoveSelected.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.RemoveSelected.Name = "RemoveSelected";
            this.RemoveSelected.Size = new System.Drawing.Size(151, 28);
            this.RemoveSelected.TabIndex = 7;
            this.RemoveSelected.Text = "Remove Selected";
            this.RemoveSelected.UseVisualStyleBackColor = true;
            this.RemoveSelected.Click += new System.EventHandler(this.RemoveSelected_Click);
            // 
            // ClearList
            // 
            this.ClearList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ClearList.Location = new System.Drawing.Point(588, 100);
            this.ClearList.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ClearList.Name = "ClearList";
            this.ClearList.Size = new System.Drawing.Size(151, 28);
            this.ClearList.TabIndex = 8;
            this.ClearList.Text = "Clear List";
            this.ClearList.UseVisualStyleBackColor = true;
            this.ClearList.Click += new System.EventHandler(this.ClearList_Click);
            // 
            // matchcase
            // 
            this.matchcase.AutoSize = true;
            this.matchcase.Location = new System.Drawing.Point(304, 71);
            this.matchcase.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.matchcase.Name = "matchcase";
            this.matchcase.Size = new System.Drawing.Size(98, 20);
            this.matchcase.TabIndex = 9;
            this.matchcase.Text = "Match case";
            this.matchcase.UseVisualStyleBackColor = true;
            // 
            // BtnClose
            // 
            this.BtnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnClose.Location = new System.Drawing.Point(588, 135);
            this.BtnClose.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(151, 28);
            this.BtnClose.TabIndex = 10;
            this.BtnClose.Text = "Close";
            this.BtnClose.UseVisualStyleBackColor = true;
            this.BtnClose.Click += new System.EventHandler(this.Close_Click);
            // 
            // regularexpression
            // 
            this.regularexpression.AutoSize = true;
            this.regularexpression.Location = new System.Drawing.Point(304, 100);
            this.regularexpression.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.regularexpression.Name = "regularexpression";
            this.regularexpression.Size = new System.Drawing.Size(146, 20);
            this.regularexpression.TabIndex = 11;
            this.regularexpression.Text = "Regular expression";
            this.regularexpression.UseVisualStyleBackColor = true;
            this.regularexpression.CheckedChanged += new System.EventHandler(this.RegularExpression_CheckedChanged);
            // 
            // reversemode
            // 
            this.reversemode.AutoSize = true;
            this.reversemode.Location = new System.Drawing.Point(304, 127);
            this.reversemode.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.reversemode.Name = "reversemode";
            this.reversemode.Size = new System.Drawing.Size(119, 20);
            this.reversemode.TabIndex = 12;
            this.reversemode.Text = "Reverse mode";
            this.reversemode.UseVisualStyleBackColor = true;
            // 
            // Columns
            // 
            this.Columns.FormattingEnabled = true;
            this.Columns.Location = new System.Drawing.Point(77, 4);
            this.Columns.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Columns.Name = "Columns";
            this.Columns.Size = new System.Drawing.Size(207, 24);
            this.Columns.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 7);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 16);
            this.label2.TabIndex = 14;
            this.label2.Text = "Column:";
            // 
            // ColumnPanel
            // 
            this.ColumnPanel.Controls.Add(this.Columns);
            this.ColumnPanel.Controls.Add(this.label2);
            this.ColumnPanel.Location = new System.Drawing.Point(281, 155);
            this.ColumnPanel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ColumnPanel.Name = "ColumnPanel";
            this.ColumnPanel.Size = new System.Drawing.Size(299, 33);
            this.ColumnPanel.TabIndex = 15;
            this.ColumnPanel.Visible = false;
            // 
            // FrmFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(776, 311);
            this.Controls.Add(this.ColumnPanel);
            this.Controls.Add(this.reversemode);
            this.Controls.Add(this.regularexpression);
            this.Controls.Add(this.BtnClose);
            this.Controls.Add(this.matchcase);
            this.Controls.Add(this.ClearList);
            this.Controls.Add(this.RemoveSelected);
            this.Controls.Add(this.Add);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.button1);
            this.Cursor = System.Windows.Forms.Cursors.AppStarting;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(794, 358);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(794, 358);
            this.Name = "FrmFilter";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Фільтрування";
            this.Load += new System.EventHandler(this.FrmFilter_Load);
            this.ColumnPanel.ResumeLayout(false);
            this.ColumnPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Add;
        private System.Windows.Forms.Button RemoveSelected;
        private System.Windows.Forms.Button ClearList;
        private System.Windows.Forms.CheckBox matchcase;
        private System.Windows.Forms.Button BtnClose;
        private System.Windows.Forms.CheckBox regularexpression;
        private System.Windows.Forms.CheckBox reversemode;
        private System.Windows.Forms.ComboBox Columns;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel ColumnPanel;
    }
}