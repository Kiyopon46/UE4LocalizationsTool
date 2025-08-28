using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using UELocalizationsTool.Controls;
using UELocalizationsTool.Core.Hash;
using UELocalizationsTool.Core.locres;
using UELocalizationsTool.Properties;

namespace UELocalizationsTool.Forms
{
    public partial class FrmLocresEntryEditor : NForm
    {
        public string NameSpace { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }

        public HashTable HashTable
        {
            get
            {
                var hash = new HashTable()
                {
                    NameHash = uint.Parse(txtNameSapceHash.Text),
                    KeyHash = uint.Parse(txtKeyHash.Text),
                    ValueHash = uint.Parse(txtValueHash.Text),
                };

                if (txtExternID.Enabled && uint.TryParse(txtExternID.Text, out uint externID))
                    hash.ExternID = externID;

                return hash;
            }
        }

        public LocresFile Asset { get; set; }

        public FrmLocresEntryEditor()
        {
            InitializeComponent();
        }

        public FrmLocresEntryEditor(NDataGridView gridView, LocresFile asset)
        {
            InitializeComponent();
            Location = new Point(
                gridView.PointToScreen(Point.Empty).X + (gridView.Width - this.Width) / 2,
                gridView.PointToScreen(Point.Empty).Y + (gridView.Height - this.Height) / 2
            );

            this.Asset = asset;
            var items = gridView.CurrentCell.OwningRow.Cells["ID"].Value.ToString().Split(new string[] { "::" }, StringSplitOptions.None);

            if (items.Length == 2)
            {
                NameSpace = items[0];
                Key = items[1];
                Value = gridView.CurrentCell.OwningRow.Cells["Text"].Value.ToString();
            }
            else
            {
                Key = items[0];
                Value = gridView.CurrentCell.OwningRow.Cells["Text"].Value.ToString();
            }

            var Hashs = gridView.CurrentCell.OwningRow.Cells["Hash Table"].Value as HashTable;

            txtNameSapceHash.Text = Hashs.NameHash.ToString();
            txtKeyHash.Text = Hashs.KeyHash.ToString();
            txtValueHash.Text = Hashs.ValueHash.ToString();
            Print(Hashs);
        }

        public FrmLocresEntryEditor(Form form, LocresFile asset)
        {
            InitializeComponent();
            this.Location = new Point(form.Location.X + (form.Width - this.Width) / 2, form.Location.Y + (form.Height - this.Height) / 2);
            Apply.Text = Resources.Btn_Add;
            this.Asset = asset;
            BtnExternID.Enabled = (Asset != null && Asset.Version == LocresVersion.Optimized_CityHash64_ExternID_UTF16);
            txtExternID.Enabled = BtnExternID.Enabled;
        }

        private void Print(HashTable rowHash = null)
        {
            txtNameSpace.Text = NameSpace;
            txtKey.Text = Key;
            txtValue.Text = Value;

            uint externID = 0;

            if (rowHash != null && rowHash.ExternID != 0)
                externID = rowHash.ExternID;
            else if (Asset != null && Asset.Version == LocresVersion.Optimized_CityHash64_ExternID_UTF16)
            {
                var assetHash = Asset.GetHash(NameSpace, Key);
                if (assetHash != null)
                    externID = assetHash.ExternID;
            }

            if (externID != 0)
            {
                txtExternID.Text = externID.ToString();
                txtExternID.Enabled = true;
            }
            else
            {
                txtExternID.Text = "";
                txtExternID.Enabled = false;
            }

            BtnExternID.Enabled = (Asset != null && Asset.Version == LocresVersion.Optimized_CityHash64_ExternID_UTF16);
        }

        public void AddRow(NDataGridView gridView)
        {
            DataTable dt = (DataTable)gridView.DataSource;
            string RowName = GetName();

            uint externID = 0;

            if (txtExternID.Enabled && uint.TryParse(txtExternID.Text, out uint parsedID))
            {
                externID = parsedID;
            }
            else if (Asset != null && Asset.Version == LocresVersion.Optimized_CityHash64_ExternID_UTF16)
            {
                var allExternIDs = Asset.SelectMany(ns => ns).Select(st => st.ExternID);
                externID = (allExternIDs.Any() ? allExternIDs.Max() : 0) + 1;
            }

            var newHash = new HashTable()
            {
                NameHash = uint.Parse(txtNameSapceHash.Text),
                KeyHash = uint.Parse(txtKeyHash.Text),
                ValueHash = uint.Parse(txtValueHash.Text),
                ExternID = externID
            };

            dt.Rows.Add(RowName, Value, newHash);
        }

        private string GetName()
        {
            string RowName;
            if (!string.IsNullOrEmpty(NameSpace))
                RowName = NameSpace + "::" + Key;
            else
                RowName = Key;
            return RowName;
        }

        public void EditRow(NDataGridView DGV)
        {
            DGV.SetValue(DGV.CurrentCell.OwningRow.Cells["ID"], GetName());
            DGV.SetValue(DGV.CurrentCell.OwningRow.Cells["Text"], txtValue.Text);

            var hash = DGV.CurrentCell.OwningRow.Cells["Hash Table"].Value as HashTable;
            if (hash != null)
            {
                hash.NameHash = uint.Parse(txtNameSapceHash.Text);
                hash.KeyHash = uint.Parse(txtKeyHash.Text);
                hash.ValueHash = uint.Parse(txtValueHash.Text);

                if (txtExternID.Enabled && uint.TryParse(txtExternID.Text, out uint externID))
                    hash.ExternID = externID;
            }

            DGV.SetValue(DGV.CurrentCell.OwningRow.Cells["Hash Table"], hash);
        }

        private void TxtNameSpace_TextChanged(object sender, EventArgs e)
        {
            NameSpace = txtNameSpace.Text;
        }

        private void TxtKey_TextChanged(object sender, EventArgs e)
        {
            Key = txtKey.Text;
        }

        private void TxtValue_TextChanged(object sender, EventArgs e)
        {
            Value = AssetParser.AssetHelper.ReplaceBreaklines(txtValue.Text);
        }

        private void BtnNameSpace_Click(object sender, EventArgs e)
        {
            txtNameSapceHash.Text = Asset.CalcHash(txtNameSpace.Text).ToString();
        }

        private void BtnKey_Click(object sender, EventArgs e)
        {
            txtKeyHash.Text = Asset.CalcHash(txtKey.Text).ToString();
        }

        private void BtnValue_Click(object sender, EventArgs e)
        {
            txtValueHash.Text = txtValue.Text.StrCrc32().ToString();
        }

        private void BtnExternID_Click(object sender, EventArgs e)
        {
            if (Asset != null && Asset.Version == LocresVersion.Optimized_CityHash64_ExternID_UTF16)
            {
                var allExternIDs = Asset.SelectMany(ns => ns).Select(st => st.ExternID);

                uint newExternID = (allExternIDs.Any() ? allExternIDs.Max() : 0) + 1;

                txtExternID.Text = newExternID.ToString();
                txtExternID.Enabled = true;
            }
            else
            {
                txtExternID.Text = "0";
                txtExternID.Enabled = false;
            }
        }

        private void Apply_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNameSapceHash.Text) ||
                string.IsNullOrEmpty(txtKeyHash.Text) ||
                string.IsNullOrEmpty(txtValueHash.Text))
            {
                MessageBox.Show("NameSpace or Key or Value is empty");
                return;
            }

            if (!uint.TryParse(txtNameSapceHash.Text, out _) ||
                !uint.TryParse(txtKeyHash.Text, out _) ||
                !uint.TryParse(txtValueHash.Text, out _))
            {
                MessageBox.Show("NameSpace or Key or Value is not a number");
                return;
            }

            if (txtExternID.Enabled)
            {
                HashTable.ExternID = uint.TryParse(txtExternID.Text, out uint externID) ? externID : 0;
            }
        }
    }
}
