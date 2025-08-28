using UELocalizationsTool.Core.locres;
using UELocalizationsTool.Properties;

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
            this.label7 = new System.Windows.Forms.Label();
            this.txtExternID = new UELocalizationsTool.Controls.NTextBox();
            this.BtnExternID = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label3.Location = new System.Drawing.Point(14, 171);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = Resources.Lbl_Text;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label2.Location = new System.Drawing.Point(14, 88);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "ID";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label1.Location = new System.Drawing.Point(14, 10);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = Resources.Lbl_Namespace;
            // 
            // button2
            // 
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.button2.Location = new System.Drawing.Point(390, 410);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(110, 30);
            this.button2.TabIndex = 5;
            this.button2.Text = Resources.Btn_Cancel;
            this.button2.UseVisualStyleBackColor = true;
            // 
            // Apply
            // 
            this.Apply.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Apply.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Apply.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Apply.Location = new System.Drawing.Point(264, 410);
            this.Apply.Margin = new System.Windows.Forms.Padding(4);
            this.Apply.Name = "Apply";
            this.Apply.Size = new System.Drawing.Size(110, 30);
            this.Apply.TabIndex = 4;
            this.Apply.Text = Resources.Btn_Apply;
            this.Apply.UseVisualStyleBackColor = true;
            this.Apply.Click += new System.EventHandler(this.Apply_Click);
            // 
            // txtValue
            // 
            this.txtValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtValue.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtValue.Location = new System.Drawing.Point(14, 192);
            this.txtValue.Margin = new System.Windows.Forms.Padding(4);
            this.txtValue.Multiline = true;
            this.txtValue.Name = "txtValue";
            this.txtValue.PlaceholderText = "";
            this.txtValue.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtValue.Size = new System.Drawing.Size(737, 125);
            this.txtValue.StopEnterKey = true;
            this.txtValue.TabIndex = 3;
            this.txtValue.TextChanged += new System.EventHandler(this.TxtValue_TextChanged);
            // 
            // txtKey
            // 
            this.txtKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtKey.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtKey.Location = new System.Drawing.Point(14, 109);
            this.txtKey.Margin = new System.Windows.Forms.Padding(4);
            this.txtKey.Multiline = true;
            this.txtKey.Name = "txtKey";
            this.txtKey.PlaceholderText = "";
            this.txtKey.Size = new System.Drawing.Size(513, 42);
            this.txtKey.StopEnterKey = true;
            this.txtKey.TabIndex = 1;
            this.txtKey.TextChanged += new System.EventHandler(this.TxtKey_TextChanged);
            // 
            // txtNameSpace
            // 
            this.txtNameSpace.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNameSpace.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtNameSpace.Location = new System.Drawing.Point(14, 31);
            this.txtNameSpace.Margin = new System.Windows.Forms.Padding(4);
            this.txtNameSpace.Multiline = true;
            this.txtNameSpace.Name = "txtNameSpace";
            this.txtNameSpace.PlaceholderText = "";
            this.txtNameSpace.Size = new System.Drawing.Size(513, 42);
            this.txtNameSpace.StopEnterKey = true;
            this.txtNameSpace.TabIndex = 0;
            this.txtNameSpace.TextChanged += new System.EventHandler(this.TxtNameSpace_TextChanged);
            // 
            // txtNameSapceHash
            // 
            this.txtNameSapceHash.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNameSapceHash.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtNameSapceHash.Location = new System.Drawing.Point(534, 31);
            this.txtNameSapceHash.Margin = new System.Windows.Forms.Padding(4);
            this.txtNameSapceHash.Multiline = true;
            this.txtNameSapceHash.Name = "txtNameSapceHash";
            this.txtNameSapceHash.PlaceholderText = "";
            this.txtNameSapceHash.Size = new System.Drawing.Size(167, 42);
            this.txtNameSapceHash.StopEnterKey = true;
            this.txtNameSapceHash.TabIndex = 9;
            // 
            // txtKeyHash
            // 
            this.txtKeyHash.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtKeyHash.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtKeyHash.Location = new System.Drawing.Point(534, 109);
            this.txtKeyHash.Margin = new System.Windows.Forms.Padding(4);
            this.txtKeyHash.Multiline = true;
            this.txtKeyHash.Name = "txtKeyHash";
            this.txtKeyHash.PlaceholderText = "";
            this.txtKeyHash.Size = new System.Drawing.Size(167, 42);
            this.txtKeyHash.StopEnterKey = true;
            this.txtKeyHash.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label4.Location = new System.Drawing.Point(537, 10);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(129, 20);
            this.label4.TabIndex = 11;
            this.label4.Text = Resources.Lbl_NamespaceHash;
            // 
            // KeyHash
            // 
            this.KeyHash.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.KeyHash.AutoSize = true;
            this.KeyHash.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.KeyHash.Location = new System.Drawing.Point(537, 88);
            this.KeyHash.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.KeyHash.Name = "KeyHash";
            this.KeyHash.Size = new System.Drawing.Size(57, 20);
            this.KeyHash.TabIndex = 12;
            this.KeyHash.Text = Resources.Lbl_IDHash;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label6.Location = new System.Drawing.Point(14, 330);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 20);
            this.label6.TabIndex = 14;
            this.label6.Text = Resources.Lbl_TextHash;
            // 
            // txtValueHash
            // 
            this.txtValueHash.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtValueHash.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtValueHash.Location = new System.Drawing.Point(14, 354);
            this.txtValueHash.Margin = new System.Windows.Forms.Padding(4);
            this.txtValueHash.Multiline = true;
            this.txtValueHash.Name = "txtValueHash";
            this.txtValueHash.PlaceholderText = "";
            this.txtValueHash.Size = new System.Drawing.Size(171, 42);
            this.txtValueHash.StopEnterKey = true;
            this.txtValueHash.TabIndex = 13;
            // 
            // BtnNameSpace
            // 
            this.BtnNameSpace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnNameSpace.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.BtnNameSpace.Location = new System.Drawing.Point(707, 31);
            this.BtnNameSpace.Margin = new System.Windows.Forms.Padding(4);
            this.BtnNameSpace.Name = "BtnNameSpace";
            this.BtnNameSpace.Size = new System.Drawing.Size(44, 42);
            this.BtnNameSpace.TabIndex = 15;
            this.BtnNameSpace.Text = "Gen";
            this.BtnNameSpace.UseVisualStyleBackColor = true;
            this.BtnNameSpace.Click += new System.EventHandler(this.BtnNameSpace_Click);
            // 
            // BtnKey
            // 
            this.BtnKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnKey.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.BtnKey.Location = new System.Drawing.Point(707, 109);
            this.BtnKey.Margin = new System.Windows.Forms.Padding(4);
            this.BtnKey.Name = "BtnKey";
            this.BtnKey.Size = new System.Drawing.Size(44, 42);
            this.BtnKey.TabIndex = 16;
            this.BtnKey.Text = "Gen";
            this.BtnKey.UseVisualStyleBackColor = true;
            this.BtnKey.Click += new System.EventHandler(this.BtnKey_Click);
            // 
            // BtnValue
            // 
            this.BtnValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnValue.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.BtnValue.Location = new System.Drawing.Point(193, 354);
            this.BtnValue.Margin = new System.Windows.Forms.Padding(4);
            this.BtnValue.Name = "BtnValue";
            this.BtnValue.Size = new System.Drawing.Size(44, 42);
            this.BtnValue.TabIndex = 17;
            this.BtnValue.Text = "Gen";
            this.BtnValue.UseVisualStyleBackColor = true;
            this.BtnValue.Click += new System.EventHandler(this.BtnValue_Click);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label7.Location = new System.Drawing.Point(537, 330);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(186, 20);
            this.label7.TabIndex = 18;
            this.label7.Text = Resources.Lbl_ExternIDLocresV4Only;
            // 
            // txtExternID
            // 
            this.txtExternID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtExternID.Enabled = false;
            this.txtExternID.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtExternID.Location = new System.Drawing.Point(534, 354);
            this.txtExternID.Margin = new System.Windows.Forms.Padding(4);
            this.txtExternID.Multiline = true;
            this.txtExternID.Name = "txtExternID";
            this.txtExternID.PlaceholderText = "";
            this.txtExternID.Size = new System.Drawing.Size(167, 42);
            this.txtExternID.StopEnterKey = true;
            this.txtExternID.TabIndex = 19;
            // 
            // BtnExternID
            // 
            this.BtnExternID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnExternID.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.BtnExternID.Location = new System.Drawing.Point(707, 354);
            this.BtnExternID.Margin = new System.Windows.Forms.Padding(4);
            this.BtnExternID.Name = "BtnExternID";
            this.BtnExternID.Size = new System.Drawing.Size(44, 42);
            this.BtnExternID.TabIndex = 20;
            this.BtnExternID.Text = "Gen";
            this.BtnExternID.UseVisualStyleBackColor = true;
            this.BtnExternID.Click += new System.EventHandler(this.BtnExternID_Click);
            // 
            // FrmLocresEntryEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(762, 458);
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
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtExternID);
            this.Controls.Add(this.BtnExternID);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(780, 500);
            this.Name = "FrmLocresEntryEditor";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = Resources.Lbl_EntryEditor;
            this.MaximizeBox = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
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
        private System.Windows.Forms.Label label7;
        private Controls.NTextBox txtExternID;
        private System.Windows.Forms.Button BtnExternID;
    }
}