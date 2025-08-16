using AssetParser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using UELocalizationsTool.Controls;
using UELocalizationsTool.Core.Hash;
using UELocalizationsTool.Core.locres;
using UELocalizationsTool.Forms;
using UELocalizationsTool.Helper;

namespace UELocalizationsTool
{
    public partial class FrmMain : NForm
    {
        IAsset Asset;
        readonly String ToolName = Application.ProductName + " " + Application.ProductVersion + " Hikaro Edition";
        string FilePath = "";
        bool SortApply = false;
        public FrmMain()
        {
            InitializeComponent();
            dataGridView1.RowCountChanged += (x, y) => this.UpdateCounter();
            ResetControls();
            pictureBox1.Height = menuStrip1.Height;
            darkModeToolStripMenuItem.Checked = Properties.Settings.Default.DarkMode;
        }

        private async void OpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "All localizations files|*.uasset;*.locres;*.umap|Uasset File|*.uasset|Locres File|*.locres|Umap File|*.umap";
            ofd.Title = "Open localizations File";


            if (ofd.ShowDialog() == DialogResult.OK)
            {
                await LoadFile(ofd.FileName);
            }
        }


        public async Task LoadFile(string filePath)
        {
            ResetControls();
            ControlsMode(false);

            try
            {
                StatusMessage("Відкриття файлу...", "Відкриття файлу, зачекайте.");

                if (filePath.ToLower().EndsWith(".locres"))
                {
                    Asset = await Task.Run(() => new LocresFile(filePath));
                    locresOprationsToolStripMenuItem.Visible = true;
                    CreateBackupList();
                }
                else if (filePath.ToLower().EndsWith(".uasset") || filePath.ToLower().EndsWith(".umap"))
                {
                    IUasset Uasset = await Task.Run(() => Uexp.GetUasset(filePath));
                    Uasset.UseMethod2 = Uasset.UseMethod2 ? Uasset.UseMethod2 : Method2.Checked;
                    Asset = await Task.Run(() => new Uexp(Uasset));
                    CreateBackupList();
                    if (!Asset.IsGood)
                    {
                        StateLabel.Text = "Увага: файл прочитано не повністю, деякі тексти можуть бути відсутні.";
                    }
                }

                this.FilePath = filePath;
                this.Text = ToolName + " - " + Path.GetFileName(FilePath);
                ControlsMode(true);
                CloseFromState();
            }
            catch (Exception ex)
            {
                CloseFromState();
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateBackupList()
        {
            Asset.AddItemsToDataGridView(dataGridView1);
        }

        private void ResetControls()
        {
            FilePath = "";
            StateLabel.Text = "";
            DataCount.Text = "";
            Text = ToolName;
            SortApply = false;
            locresOprationsToolStripMenuItem.Visible = false;
        }

        private void ControlsMode(bool Enabled)
        {
            var controls = new ToolStripItem[]
            {
                saveToolStripMenuItem, exportAllTextToolStripMenuItem,
                importAllTextToolStripMenuItem, undoToolStripMenuItem,
                redoToolStripMenuItem, filterToolStripMenuItem,
                noNamesToolStripMenuItem, withNamesToolStripMenuItem,
                clearFilterToolStripMenuItem, csvFileToolStripMenuItem,
                importAllTextByKeystoolStripMenuItem, importNewLinesFromCSVtoolStripMenuItem
            };

            foreach (var control in controls)
            {
                control.Enabled = Enabled;
            }
        }
        enum ExportType
        {
            NoNames = 0,
            WithNames
        }

        private void ExportAll(ExportType exportType)
        {

            if (this.SortApply && !(Asset is LocresFile)) SortDataGrid(2, true);

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Text File|*.txt";
            sfd.Title = "Export All Text";
            sfd.FileName = Path.GetFileName(FilePath) + ".txt";


            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (var stream = new StreamWriter(sfd.FileName))
                    {
                        if (exportType == ExportType.WithNames)
                        {
                            stream.WriteLine(@"[~NAMES-INCLUDED~]//Don't edit or remove this line.");
                        }

                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (exportType == ExportType.WithNames)
                            {
                                stream.WriteLine($"{row.Cells["ID"].Value}={row.Cells["Text"].Value}");
                                continue;
                            }
                            stream.WriteLine(row.Cells["Text"].Value.ToString());
                        }

                    }
                    if (dataGridView1.IsFilterApplied)
                    {
                        MessageBox.Show("Successful export!\n Remember to apply the same filter you using right now before 'import'.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Successful export!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch
                {
                    MessageBox.Show("Can't write export file!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        private async void ImportAllTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text File|*.txt;*.csv";
            ofd.Title = "Import All Text";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (this.SortApply && !(Asset is LocresFile)) SortDataGrid(2, true);

                try
                {
                    if (ofd.FileName.EndsWith(".csv", StringComparison.InvariantCulture))
                    {
                        await CSVFile.Instance.Load(this.dataGridView1, ofd.FileName);

                        if (dataGridView1.IsFilterApplied)
                        {
                            MessageBox.Show("Successful import!\nRemember to apply the same filter you using right now before 'import'.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Successful import!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        // Завантаження даних з TXT-файлу
                        string[] DataGridStrings = System.IO.File.ReadAllLines(ofd.FileName);

                        if (DataGridStrings.Length < dataGridView1.Rows.Count)
                        {
                            MessageBox.Show("This file doesn't contain enough strings for reimport", "Out of range", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        bool hasNames = DataGridStrings[0].StartsWith("[~NAMES-INCLUDED~]", StringComparison.OrdinalIgnoreCase);
                        IEnumerable<string> importLines = hasNames ? DataGridStrings.Skip(1) : DataGridStrings;

                        for (int n = 0; n < dataGridView1.Rows.Count; n++)
                        {
                            string line = importLines.ElementAtOrDefault(n);
                            if (line == null) break;

                            string valueToImport = hasNames
                                ? line.Split(new[] { '=' }, 2).Skip(1).FirstOrDefault() ?? string.Empty
                                : line;

                            dataGridView1.SetValue(dataGridView1.Rows[n].Cells["Text"], valueToImport);
                        }

                        MessageBox.Show("Successful import!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ToolName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void SaveFile(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            if (FilePath.ToLower().EndsWith(".locres"))
            {
                sfd.Filter = "locres File|*.locres";
            }
            else if (FilePath.ToLower().EndsWith(".uasset"))
            {
                sfd.Filter = "Uasset File|*.uasset";
            }
            else if (FilePath.ToLower().EndsWith(".umap"))
            {
                sfd.Filter = "Umap File|*.umap";
            }

            sfd.Title = "Save localizations file";
            sfd.FileName = Path.GetFileNameWithoutExtension(FilePath) + "_NEW";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    StatusMessage("Збереження файлу...", "Збереження файлу, зачекайте.");
                    await Task.Run(() =>
                    {
                        Asset.LoadFromDataGridView(dataGridView1);
                        Asset.SaveFile(sfd.FileName);
                    });
                    MessageBox.Show("Успішно збережено.", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                CloseFromState();
            }
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Copy();
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Paste();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void FontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Dialog Font select
            FontDialog fd = new FontDialog();
            fd.Font = dataGridView1.Font;
            if (fd.ShowDialog() == DialogResult.OK)
            {
                dataGridView1.Font = fd.Font;
                dataGridView1.AutoResizeRows();
            }
        }

        private void RightToLeftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.RightToLeft = dataGridView1.RightToLeft == RightToLeft.Yes ? RightToLeft.No : RightToLeft.Yes;
        }


        private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Undo();
        }

        private void RedoToolStripMenuItem_Click(object sender, EventArgs e)
        {

            dataGridView1.Redo();
        }

        private void CommandLinesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Program.commandlines, "Список CLI команд", MessageBoxButtons.OK);
        }

        private void AboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new FrmAbout(this).ShowDialog();
        }

        private void ClearFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.ClearFilter();
        }

        private void UpdateCounter()
        {
            DataCount.Text = "К-сть рядків: " + dataGridView1.Rows.Count;
        }

        private void NoNamesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportAll(ExportType.NoNames);
        }

        private void WithNamesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportAll(ExportType.WithNames);
        }

        private void ValueToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        void SortDataGrid(int Cel, bool Ascending)
        {
            this.SortApply = true;
            if (Ascending)
            {
                dataGridView1.Sort(dataGridView1.Columns[Cel], System.ComponentModel.ListSortDirection.Ascending);
                return;
            }
            dataGridView1.Sort(dataGridView1.Columns[Cel], System.ComponentModel.ListSortDirection.Descending);
        }

        private void AscendingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SortDataGrid(0, true);
        }

        private void DescendingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SortDataGrid(0, false);
        }

        private void AscendingToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SortDataGrid(1, true);
        }

        private void DescendingToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SortDataGrid(1, false);
        }

        private void DataGridView1_Sorted(object sender, EventArgs e)
        {
            this.SortApply = true;
        }

        private void FrmMain_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.All;
            }
        }

        private async void FrmMain_DragDrop(object sender, DragEventArgs e)
        {
            string[] array = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (array.Length >= 1 && (array[0].EndsWith(".uasset") || array[0].EndsWith(".umap") || array[0].EndsWith(".locres")))
            {
                await LoadFile(array[0]);
            }
        }

        private void Method2_CheckedChanged(object sender, EventArgs e)
        {

            if (Method2.Checked)
            {
                pictureBox1.Visible = true;
                fileToolStripMenuItem.Margin = new Padding(5, 0, 0, 0);
            }
            else
            {
                pictureBox1.Visible = false;
                fileToolStripMenuItem.Margin = new Padding(0, 0, 0, 0);
            }
        }

        private void DarkModeToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            bool IsDark = Properties.Settings.Default.DarkMode;
            Properties.Settings.Default.DarkMode = darkModeToolStripMenuItem.Checked;
            Properties.Settings.Default.Save();

            if (IsDark != darkModeToolStripMenuItem.Checked)
                Application.Restart();
        }

        private async void CsvFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "CSV File|*.csv";
            sfd.Title = "Export All Text";
            sfd.FileName = Path.GetFileName(FilePath) + ".csv";


            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Add 'await' to ensure the program waits for the save operation to complete.
                    await CSVFile.Instance.Save(this.dataGridView1, sfd.FileName);

                    if (dataGridView1.IsFilterApplied)
                    {
                        MessageBox.Show("Експортування успішне!\n Не забудьте застосувати той самий фільтр, перед імпортом.", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Експортування успішне!", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch
                {
                    MessageBox.Show("Не вдається записати файл!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        private void Find_Click(object sender, EventArgs e)
        {
            searchBox.Show();
        }

        private void FilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Filter();
        }

        private void DataGridView1_FilterApplied(object sender, EventArgs e)
        {
            filterToolStripMenuItem.Visible = false;
            clearFilterToolStripMenuItem.Visible = true;
        }

        private void DataGridView1_FilterCleared(object sender, EventArgs e)
        {
            filterToolStripMenuItem.Visible = true;
            clearFilterToolStripMenuItem.Visible = false;
        }

        private void RemoveSelectedRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count == 0)
            {
                MessageBox.Show("Не вибрано рядків для видалення.", "Не вдалося видалити", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            DialogResult result = MessageBox.Show("Ви впевнені, що хочете видалити вибрані рядки?", "Підтвердження", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                dataGridView1.BeginEdit(false);
                var rowsToRemove = dataGridView1.SelectedCells.OfType<DataGridViewCell>()
                                                    .Select(c => c.OwningRow)
                                                    .Distinct()
                                                    .ToList();

                foreach (var row in rowsToRemove)
                {
                    if (row.Index >= 0 && row.Index < dataGridView1.Rows.Count)
                    {
                        dataGridView1.Rows.Remove(row);
                    }
                }
                dataGridView1.EndEdit();
            }
        }

        private void EditSelectedRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 1 || dataGridView1.SelectedCells.Count == 0)
            {
                MessageBox.Show("Виберіть один рядок для редагування.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var EntryEditor = new FrmLocresEntryEditor(dataGridView1, (LocresFile)Asset);
            if (EntryEditor.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    EntryEditor.EditRow(dataGridView1);
                    MessageBox.Show("Рядок успішно відредаговано.", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Сталася помилка під час редагування рядка:\n " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void AddNewRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var EntryEditor = new FrmLocresEntryEditor(this, (LocresFile)Asset);

            if (EntryEditor.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    dataGridView1.BeginEdit(false);
                    EntryEditor.AddRow(dataGridView1);
                    dataGridView1.EndEdit();
                    MessageBox.Show("Новий рядок успішно додано.", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Сталася помилка під час додавання рядка:\n " + ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void MergeLocresFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Locres File(s)|*.locres";
            ofd.Title = "Select localization file(s)";
            ofd.Multiselect = true;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                StatusMessage("Merging locres files...", "Merging locres files, please wait.");

                try
                {
                    var dataTable = await Task.Run(() =>
                    {
                        var tempDataTable = new System.Data.DataTable();

                        if (dataGridView1.DataSource is System.Data.DataTable sourceDataTable)
                        {
                            foreach (System.Data.DataColumn col in sourceDataTable.Columns)
                            {
                                tempDataTable.Columns.Add(col.ColumnName, col.DataType);
                            }
                        }

                        foreach (string fileName in ofd.FileNames)
                        {
                            LocresFile locresFile = new LocresFile(fileName);
                            foreach (var names in locresFile)
                            {
                                foreach (var table in names)
                                {
                                    string name = string.IsNullOrEmpty(names.Name) ? table.Key : $"{names.Name}::{table.Key}";
                                    string textValue = table.Value;
                                    tempDataTable.Rows.Add(name, textValue, new HashTable(names.NameHash, table.KeyHash, table.ValueHash));
                                }
                            }
                        }
                        return tempDataTable;
                    });

                    ((System.Data.DataTable)dataGridView1.DataSource).Merge(dataTable);
                    MessageBox.Show("Locres file(s) merged successfully.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while merging locres file(s):\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    CloseFromState();
                }
            }
        }

        private void StatusMessage(string title, string message)
        {
            StatusTitle.Text = title;
            StatusText.Text = message;
            StatusBlock.Visible = true;
        }

        private void CloseFromState()
        {
            StatusBlock.Visible = false;
        }

        private async void MergeUassetFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // The ofd variable needs to be declared here to be used in this method.
            OpenFileDialog ofd = new OpenFileDialog();
            // It's also a good practice to set the filter and title here as well.
            ofd.Filter = "Uasset/Umap File(s)|*.uasset;*.umap";
            ofd.Title = "Select uasset file(s)";
            ofd.Multiselect = true;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                StatusMessage("Merging uasset files...", "Merging uasset files, please wait.");

                try
                {
                    var dataTable = await Task.Run(() =>
                    {
                        var tempDataTable = new System.Data.DataTable();
                        if (dataGridView1.DataSource is System.Data.DataTable sourceDataTable)
                        {
                            foreach (System.Data.DataColumn col in sourceDataTable.Columns)
                            {
                                tempDataTable.Columns.Add(col.ColumnName, col.DataType);
                            }
                        }

                        foreach (string fileName in ofd.FileNames)
                        {
                            Uexp uexpAsset = new Uexp(Uexp.GetUasset(fileName), true);
                            var locresasset = Asset as LocresFile;

                            foreach (var Strings in uexpAsset.StringNodes)
                            {
                                var hashTable = new HashTable(locresasset.CalcHash(Strings.NameSpace), locresasset.CalcHash(Strings.Key), Strings.Value.StrCrc32());
                                tempDataTable.Rows.Add(Strings.GetName(), Strings.Value, hashTable);
                            }
                        }
                        return tempDataTable;
                    });

                    ((System.Data.DataTable)dataGridView1.DataSource).Merge(dataTable);
                    MessageBox.Show("Uasset file(s) merged successfully.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while merging uasset file(s):\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    CloseFromState();
                }
            }
        }

        private void ReplaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            searchBox.ShowReplacePanel();
        }

        private async void TransferTextHashFromOriginalLocresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Declare ofd here as well, because each method is a separate scope.
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Locres File(s)|*.locres";
            ofd.Title = "Select localization file(s)";
            ofd.Multiselect = true;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                StatusMessage("Перенесення хешів тексту...", "Перенесення хешів тексту, зачекайте.");
                var sourceHashes = new Dictionary<string, HashTable>();

                try
                {
                    foreach (string fileName in ofd.FileNames)
                    {
                        LocresFile locresFile = await Task.Run(() => new LocresFile(fileName));
                        foreach (var names in locresFile)
                        {
                            foreach (var table in names)
                            {
                                string name = string.IsNullOrEmpty(names.Name) ? table.Key : $"{names.Name}::{table.Key}";
                                sourceHashes[name] = new HashTable(names.NameHash, table.KeyHash, table.ValueHash);
                            }
                        }
                    }

                    foreach (DataGridViewRow gridRow in dataGridView1.Rows)
                    {
                        if (gridRow.Cells["ID"].Value != null)
                        {
                            string gridName = gridRow.Cells["ID"].Value.ToString();
                            if (sourceHashes.ContainsKey(gridName))
                            {
                                gridRow.Cells["Hash Table"].Value = sourceHashes[gridName];
                            }
                        }
                    }

                    MessageBox.Show("Хеші файлу(-ів) .locres успішно перенесено.", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Сталася помилка під час перенесення хешів .locres:\n{ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                CloseFromState();
            }
        }

        private async void MergeLocresFileStableNEWToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Locres File(s)|*.locres";
            ofd.Title = "Select localization file(s)";
            ofd.Multiselect = true;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                StatusMessage("Об’єднання .locres...", "Об’єднання .locres, зачекайте.");

                try
                {
                    var existingNames = new HashSet<string>();
                    if (dataGridView1.DataSource is System.Data.DataTable sourceDataTable)
                    {
                        foreach (System.Data.DataRow row in sourceDataTable.Rows)
                        {
                            if (row["ID"] != System.DBNull.Value)
                            {
                                existingNames.Add(row["ID"].ToString());
                            }
                        }
                    }

                    System.Data.DataTable newRowsTable = new System.Data.DataTable();
                    newRowsTable.Columns.Add("ID", typeof(string));
                    newRowsTable.Columns.Add("Text", typeof(string));
                    newRowsTable.Columns.Add("Hash Table", typeof(HashTable));

                    foreach (string fileName in ofd.FileNames)
                    {
                        LocresFile locresFile = await Task.Run(() => new LocresFile(fileName));
                        foreach (var names in locresFile)
                        {
                            foreach (var table in names)
                            {
                                string name = string.IsNullOrEmpty(names.Name) ? table.Key : $"{names.Name}::{table.Key}";
                                string textValue = table.Value;
                                var hashTable = new HashTable(names.NameHash, table.KeyHash, table.ValueHash);

                                if (!existingNames.Contains(name))
                                {
                                    newRowsTable.Rows.Add(name, textValue, hashTable);
                                    existingNames.Add(name);
                                }
                            }
                        }
                    }

                    if (dataGridView1.DataSource is System.Data.DataTable currentDataTable && newRowsTable.Rows.Count > 0)
                    {
                        currentDataTable.Merge(newRowsTable);
                    }

                    MessageBox.Show(".locres успішно об’єднано.", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Сталася помилка під час об’єднання .locres:\n{ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                CloseFromState();
            }
        }

        private async void ImportAllTextByKeystoolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "CSV File|*.csv";
            ofd.Title = "Import All Text (by keys)";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (this.SortApply && !(Asset is LocresFile)) SortDataGrid(2, true);

                if (ofd.FileName.EndsWith(".csv", StringComparison.InvariantCulture))
                {
                    try
                    {
                        // This is the key change: use 'await' to wait for the method to finish.
                        await CSVFile.Instance.LoadByKeys(this.dataGridView1, ofd.FileName);
                        MessageBox.Show("Імпортування виконано!", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, ToolName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private async void ImportNewLinesFromCSVtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "CSV File|*.csv";
            ofd.Title = "Import New Lines from CSV";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (this.SortApply && !(Asset is LocresFile)) SortDataGrid(2, true);

                if (ofd.FileName.EndsWith(".csv", StringComparison.InvariantCulture))
                {
                    try
                    {
                        await CSVFile.Instance.LoadNewLines((NDataGridView)this.dataGridView1, ofd.FileName, (LocresFile)Asset);
                        MessageBox.Show($"Нові рядки успішно імпортовані!", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, ToolName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}