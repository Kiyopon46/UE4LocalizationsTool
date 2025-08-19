using System;
using System.Windows.Forms;

namespace UELocalizationsTool.Controls
{
    partial class SearchBox
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SearchHide = new System.Windows.Forms.Label();
            this.FindPrevious = new System.Windows.Forms.Button();
            this.FindNext = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.FindAll = new System.Windows.Forms.Button();
            this.InputSearch = new UELocalizationsTool.Controls.NTextBox();
            this.Replacepanel = new System.Windows.Forms.Panel();
            this.ReplaceAll = new System.Windows.Forms.Button();
            this.txtReplace = new UELocalizationsTool.Controls.NTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Replace = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.listView1 = new System.Windows.Forms.ListView();
            this.RowIndex = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CellValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1.SuspendLayout();
            this.Replacepanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // SearchHide
            // 
            this.SearchHide.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchHide.AutoSize = true;
            this.SearchHide.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SearchHide.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.SearchHide.Location = new System.Drawing.Point(775, 10);
            this.SearchHide.Name = "SearchHide";
            this.SearchHide.Size = new System.Drawing.Size(19, 20);
            this.SearchHide.TabIndex = 5;
            this.SearchHide.Text = "X";
            this.SearchHide.Click += new System.EventHandler(this.SearchHide_Click);
            // 
            // FindPrevious
            // 
            this.FindPrevious.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FindPrevious.Location = new System.Drawing.Point(537, 7);
            this.FindPrevious.Name = "FindPrevious";
            this.FindPrevious.Size = new System.Drawing.Size(153, 27);
            this.FindPrevious.TabIndex = 4;
            this.FindPrevious.Text = "Знайти попереднє";
            this.FindPrevious.UseVisualStyleBackColor = true;
            this.FindPrevious.Click += new System.EventHandler(this.FindPrevious_Click);
            // 
            // FindNext
            // 
            this.FindNext.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FindNext.Location = new System.Drawing.Point(406, 7);
            this.FindNext.Name = "FindNext";
            this.FindNext.Size = new System.Drawing.Size(115, 27);
            this.FindNext.TabIndex = 3;
            this.FindNext.Text = "Знайти далі";
            this.FindNext.UseVisualStyleBackColor = true;
            this.FindNext.Click += new System.EventHandler(this.FindNext_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label1.Location = new System.Drawing.Point(3, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Пошук:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.FindAll);
            this.panel1.Controls.Add(this.InputSearch);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.SearchHide);
            this.panel1.Controls.Add(this.FindNext);
            this.panel1.Controls.Add(this.FindPrevious);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 40);
            this.panel1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label2.Location = new System.Drawing.Point(740, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 20);
            this.label2.TabIndex = 8;
            // 
            // FindAll
            // 
            this.FindAll.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FindAll.Location = new System.Drawing.Point(280, 7);
            this.FindAll.Name = "FindAll";
            this.FindAll.Size = new System.Drawing.Size(110, 27);
            this.FindAll.TabIndex = 7;
            this.FindAll.Text = "Знайти все";
            this.FindAll.UseVisualStyleBackColor = true;
            this.FindAll.Click += new System.EventHandler(this.FindAll_Click);
            // 
            // InputSearch
            // 
            this.InputSearch.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.InputSearch.Location = new System.Drawing.Point(65, 7);
            this.InputSearch.Name = "InputSearch";
            this.InputSearch.PlaceholderText = "Введіть текст для пошуку...";
            this.InputSearch.Size = new System.Drawing.Size(200, 27);
            this.InputSearch.StopEnterKey = false;
            this.InputSearch.TabIndex = 0;
            this.InputSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.InputSearch_KeyDown);
            // 
            // Replacepanel
            // 
            this.Replacepanel.Controls.Add(this.ReplaceAll);
            this.Replacepanel.Controls.Add(this.txtReplace);
            this.Replacepanel.Controls.Add(this.label3);
            this.Replacepanel.Controls.Add(this.label4);
            this.Replacepanel.Controls.Add(this.Replace);
            this.Replacepanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.Replacepanel.Location = new System.Drawing.Point(0, 40);
            this.Replacepanel.Name = "Replacepanel";
            this.Replacepanel.Size = new System.Drawing.Size(800, 40);
            this.Replacepanel.TabIndex = 1;
            this.Replacepanel.Visible = false;
            // 
            // ReplaceAll
            // 
            this.ReplaceAll.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ReplaceAll.Location = new System.Drawing.Point(406, 7);
            this.ReplaceAll.Name = "ReplaceAll";
            this.ReplaceAll.Size = new System.Drawing.Size(115, 27);
            this.ReplaceAll.TabIndex = 6;
            this.ReplaceAll.Text = "Замінити все";
            this.ReplaceAll.UseVisualStyleBackColor = true;
            this.ReplaceAll.Click += new System.EventHandler(this.ReplaceAll_Click);
            // 
            // txtReplace
            // 
            this.txtReplace.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtReplace.Location = new System.Drawing.Point(65, 7);
            this.txtReplace.Name = "txtReplace";
            this.txtReplace.PlaceholderText = "Введіть текст для заміни...";
            this.txtReplace.Size = new System.Drawing.Size(200, 27);
            this.txtReplace.StopEnterKey = false;
            this.txtReplace.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label3.Location = new System.Drawing.Point(3, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 20);
            this.label3.TabIndex = 1;
            this.label3.Text = "Заміна:";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(775, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(19, 20);
            this.label4.TabIndex = 5;
            this.label4.Text = "X";
            this.label4.Click += new System.EventHandler(this.Label4_Click);
            // 
            // Replace
            // 
            this.Replace.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Replace.Location = new System.Drawing.Point(280, 7);
            this.Replace.Name = "Replace";
            this.Replace.Size = new System.Drawing.Size(110, 27);
            this.Replace.TabIndex = 3;
            this.Replace.Text = "Замінити";
            this.Replace.UseVisualStyleBackColor = true;
            this.Replace.Click += new System.EventHandler(this.Replace_Click);
            // 
            // listView1
            // 
            this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.RowIndex,
            this.CellValue});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Top;
            this.listView1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.listView1.FullRowSelect = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(0, 80);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(800, 215);
            this.listView1.TabIndex = 3;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.Visible = false;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.ListView1_SelectedIndexChanged);
            // 
            // RowIndex
            // 
            this.RowIndex.Text = "Індекс рядка";
            this.RowIndex.Width = 100;
            // 
            // CellValue
            // 
            this.CellValue.Text = "Текст";
            this.CellValue.Width = 593;
            // 
            // SearchBox
            // 
            this.AutoSize = true;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.Replacepanel);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Location = new System.Drawing.Point(155, 23);
            this.Name = "SearchBox";
            this.Size = new System.Drawing.Size(800, 295);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.Replacepanel.ResumeLayout(false);
            this.Replacepanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label SearchHide;
        private System.Windows.Forms.Button FindPrevious;
        private System.Windows.Forms.Button FindNext;

        private System.Windows.Forms.Label label1;
        public NTextBox InputSearch;
        private Panel panel1;
        private Button FindAll;
        private Panel Replacepanel;
        public NTextBox txtReplace;
        private Label label3;
        private Label label4;
        private Button Replace;
        private ColorDialog colorDialog1;
        private ListView listView1;
        private ColumnHeader RowIndex;
        private ColumnHeader CellValue;
        private Label label2;
        private Button ReplaceAll;
    }
}
