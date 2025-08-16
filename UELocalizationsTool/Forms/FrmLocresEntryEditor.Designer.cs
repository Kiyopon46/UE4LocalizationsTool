namespace UELocalizationsTool.Forms
{
    partial class FrmLocresEntryEditor
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
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.Apply = new System.Windows.Forms.Button();
            this.txtValue = new UELocalizationsTool.Controls.NTextBox();
            this.txtKey = new UELocalizationsTool.Controls.NTextBox();
            this.txtNameSpace = new UELocalizationsTool.Controls.NTextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.txtNameSapceHash = new UELocalizationsTool.Controls.NTextBox();
            this.txtKeyHash = new UELocalizationsTool.Controls.NTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.KeyHash = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtValueHash = new UELocalizationsTool.Controls.NTextBox();
            this.BtnNameSpace = new System.Windows.Forms.Button();
            this.BtnKey = new System.Windows.Forms.Button();
            this.BtnValue = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 182);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 16);
            this.label3.TabIndex = 8;
            this.label3.Text = "Текст";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 97);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "ID";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 21);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "Область імен";
            // 
            // button2
            // 
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(420, 404);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 28);
            this.button2.TabIndex = 5;
            this.button2.Text = "Скасувати";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // Apply
            // 
            this.Apply.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Apply.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Apply.Location = new System.Drawing.Point(280, 404);
            this.Apply.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Apply.Name = "Apply";
            this.Apply.Size = new System.Drawing.Size(100, 28);
            this.Apply.TabIndex = 4;
            this.Apply.Text = "Застосувати";
            this.Apply.UseVisualStyleBackColor = true;
            this.Apply.Click += new System.EventHandler(this.Apply_Click);
            // 
            // txtValue
            // 
            this.txtValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtValue.Location = new System.Drawing.Point(16, 202);
            this.txtValue.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtValue.Multiline = true;
            this.txtValue.Name = "txtValue";
            this.txtValue.PlaceholderText = "";
            this.txtValue.Size = new System.Drawing.Size(783, 118);
            this.txtValue.StopEnterKey = true;
            this.txtValue.TabIndex = 3;
            this.txtValue.TextChanged += new System.EventHandler(this.TxtValue_TextChanged);
            // 
            // txtKey
            // 
            this.txtKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtKey.Location = new System.Drawing.Point(16, 117);
            this.txtKey.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtKey.Multiline = true;
            this.txtKey.Name = "txtKey";
            this.txtKey.PlaceholderText = "";
            this.txtKey.Size = new System.Drawing.Size(544, 43);
            this.txtKey.StopEnterKey = true;
            this.txtKey.TabIndex = 1;
            this.txtKey.TextChanged += new System.EventHandler(this.TxtKey_TextChanged);
            // 
            // txtNameSpace
            // 
            this.txtNameSpace.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNameSpace.Location = new System.Drawing.Point(16, 41);
            this.txtNameSpace.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtNameSpace.Multiline = true;
            this.txtNameSpace.Name = "txtNameSpace";
            this.txtNameSpace.PlaceholderText = "";
            this.txtNameSpace.Size = new System.Drawing.Size(544, 41);
            this.txtNameSpace.StopEnterKey = true;
            this.txtNameSpace.TabIndex = 0;
            this.txtNameSpace.TextChanged += new System.EventHandler(this.TxtNameSpace_TextChanged);
            // 
            // txtNameSapceHash
            // 
            this.txtNameSapceHash.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNameSapceHash.Location = new System.Drawing.Point(569, 41);
            this.txtNameSapceHash.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtNameSapceHash.Multiline = true;
            this.txtNameSapceHash.Name = "txtNameSapceHash";
            this.txtNameSapceHash.PlaceholderText = "";
            this.txtNameSapceHash.Size = new System.Drawing.Size(192, 41);
            this.txtNameSapceHash.StopEnterKey = true;
            this.txtNameSapceHash.TabIndex = 9;
            // 
            // txtKeyHash
            // 
            this.txtKeyHash.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtKeyHash.Location = new System.Drawing.Point(569, 117);
            this.txtKeyHash.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtKeyHash.Multiline = true;
            this.txtKeyHash.Name = "txtKeyHash";
            this.txtKeyHash.PlaceholderText = "";
            this.txtKeyHash.Size = new System.Drawing.Size(193, 43);
            this.txtKeyHash.StopEnterKey = true;
            this.txtKeyHash.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(569, 17);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(115, 16);
            this.label4.TabIndex = 11;
            this.label4.Text = "Хеш області імен";
            // 
            // KeyHash
            // 
            this.KeyHash.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.KeyHash.AutoSize = true;
            this.KeyHash.Location = new System.Drawing.Point(569, 97);
            this.KeyHash.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.KeyHash.Name = "KeyHash";
            this.KeyHash.Size = new System.Drawing.Size(48, 16);
            this.KeyHash.TabIndex = 12;
            this.KeyHash.Text = "Хеш ID";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(488, 341);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 16);
            this.label6.TabIndex = 14;
            this.label6.Text = "Хеш тексту";
            // 
            // txtValueHash
            // 
            this.txtValueHash.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtValueHash.Location = new System.Drawing.Point(573, 329);
            this.txtValueHash.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtValueHash.Multiline = true;
            this.txtValueHash.Name = "txtValueHash";
            this.txtValueHash.PlaceholderText = "";
            this.txtValueHash.Size = new System.Drawing.Size(189, 36);
            this.txtValueHash.StopEnterKey = true;
            this.txtValueHash.TabIndex = 13;
            // 
            // BtnNameSpace
            // 
            this.BtnNameSpace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnNameSpace.Location = new System.Drawing.Point(769, 41);
            this.BtnNameSpace.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnNameSpace.Name = "BtnNameSpace";
            this.BtnNameSpace.Size = new System.Drawing.Size(40, 41);
            this.BtnNameSpace.TabIndex = 15;
            this.BtnNameSpace.Text = "Gen";
            this.BtnNameSpace.UseVisualStyleBackColor = true;
            this.BtnNameSpace.Click += new System.EventHandler(this.BtnNameSpace_Click);
            // 
            // BtnKey
            // 
            this.BtnKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnKey.Location = new System.Drawing.Point(769, 117);
            this.BtnKey.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnKey.Name = "BtnKey";
            this.BtnKey.Size = new System.Drawing.Size(40, 44);
            this.BtnKey.TabIndex = 16;
            this.BtnKey.Text = "Gen";
            this.BtnKey.UseVisualStyleBackColor = true;
            this.BtnKey.Click += new System.EventHandler(this.BtnKey_Click);
            // 
            // BtnValue
            // 
            this.BtnValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnValue.Location = new System.Drawing.Point(769, 329);
            this.BtnValue.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BtnValue.Name = "BtnValue";
            this.BtnValue.Size = new System.Drawing.Size(40, 37);
            this.BtnValue.TabIndex = 17;
            this.BtnValue.Text = "Gen";
            this.BtnValue.UseVisualStyleBackColor = true;
            this.BtnValue.Click += new System.EventHandler(this.BtnValue_Click);
            // 
            // FrmLocresEntryEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(816, 455);
            this.Controls.Add(this.BtnValue);
            this.Controls.Add(this.BtnKey);
            this.Controls.Add(this.BtnNameSpace);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtValueHash);
            this.Controls.Add(this.KeyHash);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtKeyHash);
            this.Controls.Add(this.txtNameSapceHash);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.Apply);
            this.Controls.Add(this.txtValue);
            this.Controls.Add(this.txtKey);
            this.Controls.Add(this.txtNameSpace);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MinimumSize = new System.Drawing.Size(686, 456);
            this.Name = "FrmLocresEntryEditor";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Редактор рядків";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.NTextBox txtNameSpace;
        private Controls.NTextBox txtKey;
        private Controls.NTextBox txtValue;
        private System.Windows.Forms.Button Apply;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolTip toolTip1;
        private Controls.NTextBox txtNameSapceHash;
        private Controls.NTextBox txtKeyHash;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label KeyHash;
        private System.Windows.Forms.Label label6;
        private Controls.NTextBox txtValueHash;
        private System.Windows.Forms.Button BtnNameSpace;
        private System.Windows.Forms.Button BtnKey;
        private System.Windows.Forms.Button BtnValue;
    }
}