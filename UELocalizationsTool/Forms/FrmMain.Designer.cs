using System;
using System.Windows.Forms;
using UELocalizationsTool.Controls;

namespace UELocalizationsTool
{
    partial class FrmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenFile = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportAllTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noNamesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.withNamesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.csvFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importAllTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importAllTextByKeystoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importNewLinesFromCSVtoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.find = new System.Windows.Forms.ToolStripMenuItem();
            this.replaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sortToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ascendingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.descendingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.valueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ascendingToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.descendingToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.locresOprationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editSelectedRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeSelectedRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addNewRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mergeLocresFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mergeLocresFileStableNEWToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mergeUassetFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.transferTextHashFromOriginalLocresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fontToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.darkModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.Method2 = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.commandLinesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.DataCount = new System.Windows.Forms.ToolStripLabel();
            this.StateLabel = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.dataGridView1 = new NDataGridView();
            this.TextName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TextValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Index = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.searchBox = new UELocalizationsTool.Controls.SearchBox();
            this.StatusBlock = new System.Windows.Forms.Panel();
            this.StatusText = new System.Windows.Forms.Label();
            this.StatusTitle = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.StatusBlock.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.AllowDrop = true;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.locresOprationsToolStripMenuItem,
            this.ToolToolStripMenuItem,
            this.aboutToolStripMenuItem,
            this.DataCount,
            this.StateLabel});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.menuStrip1.Size = new System.Drawing.Size(982, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenFile,
            this.saveToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(59, 24);
            this.fileToolStripMenuItem.Text = "Файл";
            // 
            // OpenFile
            // 
            this.OpenFile.Name = "OpenFile";
            this.OpenFile.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.OpenFile.Size = new System.Drawing.Size(233, 26);
            this.OpenFile.Text = "Відкрити";
            this.OpenFile.Click += new System.EventHandler(this.OpenFile_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Enabled = false;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(233, 26);
            this.saveToolStripMenuItem.Text = "Зберегти як...";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.SaveFile);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(233, 26);
            this.exitToolStripMenuItem.Text = "Вихід";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportAllTextToolStripMenuItem,
            this.importAllTextToolStripMenuItem,
            this.importAllTextByKeystoolStripMenuItem,
            this.importNewLinesFromCSVtoolStripMenuItem,
            this.toolStripSeparator1,
            this.find,
            this.replaceToolStripMenuItem,
            this.filterToolStripMenuItem,
            this.clearFilterToolStripMenuItem,
            this.sortToolStripMenuItem,
            this.toolStripSeparator2,
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.copyToolStripMenuItem1,
            this.pasteToolStripMenuItem1});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(110, 24);
            this.editToolStripMenuItem.Text = "Редагування";
            // 
            // exportAllTextToolStripMenuItem
            // 
            this.exportAllTextToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.noNamesToolStripMenuItem,
            this.withNamesToolStripMenuItem,
            this.csvFileToolStripMenuItem});
            this.exportAllTextToolStripMenuItem.Enabled = false;
            this.exportAllTextToolStripMenuItem.Name = "exportAllTextToolStripMenuItem";
            this.exportAllTextToolStripMenuItem.Size = new System.Drawing.Size(328, 26);
            this.exportAllTextToolStripMenuItem.Text = "Експорт";
            // 
            // noNamesToolStripMenuItem
            // 
            this.noNamesToolStripMenuItem.Enabled = false;
            this.noNamesToolStripMenuItem.Name = "noNamesToolStripMenuItem";
            this.noNamesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.noNamesToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.noNamesToolStripMenuItem.Text = "TXT (без ID)";
            this.noNamesToolStripMenuItem.Click += new System.EventHandler(this.NoNamesToolStripMenuItem_Click);
            // 
            // withNamesToolStripMenuItem
            // 
            this.withNamesToolStripMenuItem.Enabled = false;
            this.withNamesToolStripMenuItem.Name = "withNamesToolStripMenuItem";
            this.withNamesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.withNamesToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.withNamesToolStripMenuItem.Text = "TXT";
            this.withNamesToolStripMenuItem.Click += new System.EventHandler(this.WithNamesToolStripMenuItem_Click);
            // 
            // csvFileToolStripMenuItem
            // 
            this.csvFileToolStripMenuItem.Name = "csvFileToolStripMenuItem";
            this.csvFileToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.csvFileToolStripMenuItem.Text = "CSV";
            this.csvFileToolStripMenuItem.Click += new System.EventHandler(this.CsvFileToolStripMenuItem_Click);
            // 
            // importAllTextToolStripMenuItem
            // 
            this.importAllTextToolStripMenuItem.Enabled = false;
            this.importAllTextToolStripMenuItem.Name = "importAllTextToolStripMenuItem";
            this.importAllTextToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.importAllTextToolStripMenuItem.Size = new System.Drawing.Size(328, 26);
            this.importAllTextToolStripMenuItem.Text = "Імпорт за індексами рядків";
            this.importAllTextToolStripMenuItem.Click += new System.EventHandler(this.ImportAllTextToolStripMenuItem_Click);
            // 
            // importAllTextByKeystoolStripMenuItem
            // 
            this.importAllTextByKeystoolStripMenuItem.Enabled = false;
            this.importAllTextByKeystoolStripMenuItem.Name = "importAllTextByKeystoolStripMenuItem";
            this.importAllTextByKeystoolStripMenuItem.Size = new System.Drawing.Size(328, 26);
            this.importAllTextByKeystoolStripMenuItem.Text = "Імпорт за ID (повільно)";
            this.importAllTextByKeystoolStripMenuItem.Click += new System.EventHandler(this.ImportAllTextByKeystoolStripMenuItem_Click);
            // 
            // importNewLinesFromCSVtoolStripMenuItem
            // 
            this.importNewLinesFromCSVtoolStripMenuItem.Enabled = false;
            this.importNewLinesFromCSVtoolStripMenuItem.Name = "importNewLinesFromCSVtoolStripMenuItem";
            this.importNewLinesFromCSVtoolStripMenuItem.Size = new System.Drawing.Size(328, 26);
            this.importNewLinesFromCSVtoolStripMenuItem.Text = "Імпорт нових рядків з CSV";
            this.importNewLinesFromCSVtoolStripMenuItem.Click += new System.EventHandler(this.ImportNewLinesFromCSVtoolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(325, 6);
            // 
            // find
            // 
            this.find.Name = "find";
            this.find.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.find.Size = new System.Drawing.Size(328, 26);
            this.find.Text = "Знайти";
            this.find.Click += new System.EventHandler(this.Find_Click);
            // 
            // replaceToolStripMenuItem
            // 
            this.replaceToolStripMenuItem.Name = "replaceToolStripMenuItem";
            this.replaceToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
            this.replaceToolStripMenuItem.Size = new System.Drawing.Size(328, 26);
            this.replaceToolStripMenuItem.Text = "Замінити";
            this.replaceToolStripMenuItem.Click += new System.EventHandler(this.ReplaceToolStripMenuItem_Click);
            // 
            // filterToolStripMenuItem
            // 
            this.filterToolStripMenuItem.Enabled = false;
            this.filterToolStripMenuItem.Name = "filterToolStripMenuItem";
            this.filterToolStripMenuItem.Size = new System.Drawing.Size(328, 26);
            this.filterToolStripMenuItem.Text = "Фільтрувати";
            this.filterToolStripMenuItem.Click += new System.EventHandler(this.FilterToolStripMenuItem_Click);
            // 
            // clearFilterToolStripMenuItem
            // 
            this.clearFilterToolStripMenuItem.Enabled = false;
            this.clearFilterToolStripMenuItem.Name = "clearFilterToolStripMenuItem";
            this.clearFilterToolStripMenuItem.Size = new System.Drawing.Size(328, 26);
            this.clearFilterToolStripMenuItem.Text = "Очистити фільтр";
            this.clearFilterToolStripMenuItem.Visible = false;
            this.clearFilterToolStripMenuItem.Click += new System.EventHandler(this.ClearFilterToolStripMenuItem_Click);
            // 
            // sortToolStripMenuItem
            // 
            this.sortToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nameToolStripMenuItem,
            this.valueToolStripMenuItem});
            this.sortToolStripMenuItem.Name = "sortToolStripMenuItem";
            this.sortToolStripMenuItem.Size = new System.Drawing.Size(328, 26);
            this.sortToolStripMenuItem.Text = "Сортувати";
            // 
            // nameToolStripMenuItem
            // 
            this.nameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ascendingToolStripMenuItem,
            this.descendingToolStripMenuItem});
            this.nameToolStripMenuItem.Name = "nameToolStripMenuItem";
            this.nameToolStripMenuItem.Size = new System.Drawing.Size(166, 26);
            this.nameToolStripMenuItem.Text = "За ID";
            // 
            // ascendingToolStripMenuItem
            // 
            this.ascendingToolStripMenuItem.Name = "ascendingToolStripMenuItem";
            this.ascendingToolStripMenuItem.Size = new System.Drawing.Size(234, 26);
            this.ascendingToolStripMenuItem.Text = "За зростанням (A-Z)";
            this.ascendingToolStripMenuItem.Click += new System.EventHandler(this.AscendingToolStripMenuItem_Click);
            // 
            // descendingToolStripMenuItem
            // 
            this.descendingToolStripMenuItem.Name = "descendingToolStripMenuItem";
            this.descendingToolStripMenuItem.Size = new System.Drawing.Size(234, 26);
            this.descendingToolStripMenuItem.Text = "За спаданням (Z-A)";
            this.descendingToolStripMenuItem.Click += new System.EventHandler(this.DescendingToolStripMenuItem_Click);
            // 
            // valueToolStripMenuItem
            // 
            this.valueToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ascendingToolStripMenuItem1,
            this.descendingToolStripMenuItem1});
            this.valueToolStripMenuItem.Name = "valueToolStripMenuItem";
            this.valueToolStripMenuItem.Size = new System.Drawing.Size(166, 26);
            this.valueToolStripMenuItem.Text = "За текстом";
            this.valueToolStripMenuItem.Click += new System.EventHandler(this.ValueToolStripMenuItem_Click);
            // 
            // ascendingToolStripMenuItem1
            // 
            this.ascendingToolStripMenuItem1.Name = "ascendingToolStripMenuItem1";
            this.ascendingToolStripMenuItem1.Size = new System.Drawing.Size(234, 26);
            this.ascendingToolStripMenuItem1.Text = "За зростанням (A-Z)";
            this.ascendingToolStripMenuItem1.Click += new System.EventHandler(this.AscendingToolStripMenuItem1_Click);
            // 
            // descendingToolStripMenuItem1
            // 
            this.descendingToolStripMenuItem1.Name = "descendingToolStripMenuItem1";
            this.descendingToolStripMenuItem1.Size = new System.Drawing.Size(234, 26);
            this.descendingToolStripMenuItem1.Text = "За спаданням (Z-A)";
            this.descendingToolStripMenuItem1.Click += new System.EventHandler(this.DescendingToolStripMenuItem1_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(325, 6);
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Enabled = false;
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+Z";
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(328, 26);
            this.undoToolStripMenuItem.Text = "Скасувати";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.UndoToolStripMenuItem_Click);
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Enabled = false;
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+Shift+Z/Ctrl+Y";
            this.redoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.Z)));
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(328, 26);
            this.redoToolStripMenuItem.Text = "Повторити";
            this.redoToolStripMenuItem.Click += new System.EventHandler(this.RedoToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem1
            // 
            this.copyToolStripMenuItem1.Name = "copyToolStripMenuItem1";
            this.copyToolStripMenuItem1.ShortcutKeyDisplayString = "Ctrl+C";
            this.copyToolStripMenuItem1.Size = new System.Drawing.Size(328, 26);
            this.copyToolStripMenuItem1.Text = "Копіювати";
            this.copyToolStripMenuItem1.Click += new System.EventHandler(this.CopyToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem1
            // 
            this.pasteToolStripMenuItem1.Name = "pasteToolStripMenuItem1";
            this.pasteToolStripMenuItem1.ShortcutKeyDisplayString = "Ctrl+V";
            this.pasteToolStripMenuItem1.Size = new System.Drawing.Size(328, 26);
            this.pasteToolStripMenuItem1.Text = "Вставити";
            this.pasteToolStripMenuItem1.Click += new System.EventHandler(this.PasteToolStripMenuItem_Click);
            // 
            // locresOprationsToolStripMenuItem
            // 
            this.locresOprationsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editSelectedRowToolStripMenuItem,
            this.removeSelectedRowToolStripMenuItem,
            this.addNewRowToolStripMenuItem,
            this.mergeLocresFileToolStripMenuItem,
            this.mergeLocresFileStableNEWToolStripMenuItem,
            this.mergeUassetFileToolStripMenuItem,
            this.toolStripSeparator5,
            this.transferTextHashFromOriginalLocresToolStripMenuItem});
            this.locresOprationsToolStripMenuItem.Name = "locresOprationsToolStripMenuItem";
            this.locresOprationsToolStripMenuItem.Size = new System.Drawing.Size(85, 24);
            this.locresOprationsToolStripMenuItem.Text = "Операції";
            // 
            // editSelectedRowToolStripMenuItem
            // 
            this.editSelectedRowToolStripMenuItem.Name = "editSelectedRowToolStripMenuItem";
            this.editSelectedRowToolStripMenuItem.Size = new System.Drawing.Size(366, 26);
            this.editSelectedRowToolStripMenuItem.Text = "Редагувати вибраний рядок";
            this.editSelectedRowToolStripMenuItem.Click += new System.EventHandler(this.EditSelectedRowToolStripMenuItem_Click);
            // 
            // removeSelectedRowToolStripMenuItem
            // 
            this.removeSelectedRowToolStripMenuItem.Name = "removeSelectedRowToolStripMenuItem";
            this.removeSelectedRowToolStripMenuItem.Size = new System.Drawing.Size(366, 26);
            this.removeSelectedRowToolStripMenuItem.Text = "Видалити вибрані рядки";
            this.removeSelectedRowToolStripMenuItem.Click += new System.EventHandler(this.RemoveSelectedRowToolStripMenuItem_Click);
            // 
            // addNewRowToolStripMenuItem
            // 
            this.addNewRowToolStripMenuItem.Name = "addNewRowToolStripMenuItem";
            this.addNewRowToolStripMenuItem.Size = new System.Drawing.Size(366, 26);
            this.addNewRowToolStripMenuItem.Text = "Додати новий рядок";
            this.addNewRowToolStripMenuItem.Click += new System.EventHandler(this.AddNewRowToolStripMenuItem_Click);
            // 
            // mergeLocresFileToolStripMenuItem
            // 
            this.mergeLocresFileToolStripMenuItem.Name = "mergeLocresFileToolStripMenuItem";
            this.mergeLocresFileToolStripMenuItem.Size = new System.Drawing.Size(366, 26);
            this.mergeLocresFileToolStripMenuItem.Text = "Об’єднати .locres";
            this.mergeLocresFileToolStripMenuItem.Click += new System.EventHandler(this.MergeLocresFileToolStripMenuItem_Click);
            // 
            // mergeLocresFileStableNEWToolStripMenuItem
            // 
            this.mergeLocresFileStableNEWToolStripMenuItem.Name = "mergeLocresFileStableNEWToolStripMenuItem";
            this.mergeLocresFileStableNEWToolStripMenuItem.Size = new System.Drawing.Size(366, 26);
            this.mergeLocresFileStableNEWToolStripMenuItem.Text = "Об’єднати .locres (робоче)";
            this.mergeLocresFileStableNEWToolStripMenuItem.Click += new System.EventHandler(this.MergeLocresFileStableNEWToolStripMenuItem_Click);
            // 
            // mergeUassetFileToolStripMenuItem
            // 
            this.mergeUassetFileToolStripMenuItem.Name = "mergeUassetFileToolStripMenuItem";
            this.mergeUassetFileToolStripMenuItem.Size = new System.Drawing.Size(366, 26);
            this.mergeUassetFileToolStripMenuItem.Text = "Об’єднати .uasset (бета)";
            this.mergeUassetFileToolStripMenuItem.Click += new System.EventHandler(this.MergeUassetFileToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(363, 6);
            // 
            // transferTextHashFromOriginalLocresToolStripMenuItem
            // 
            this.transferTextHashFromOriginalLocresToolStripMenuItem.Name = "transferTextHashFromOriginalLocresToolStripMenuItem";
            this.transferTextHashFromOriginalLocresToolStripMenuItem.Size = new System.Drawing.Size(366, 26);
            this.transferTextHashFromOriginalLocresToolStripMenuItem.Text = "Перенести хеші з оригінального .locres";
            this.transferTextHashFromOriginalLocresToolStripMenuItem.Click += new System.EventHandler(this.TransferTextHashFromOriginalLocresToolStripMenuItem_Click);
            // 
            // ToolToolStripMenuItem
            // 
            this.ToolToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fontToolStripMenuItem,
            this.darkModeToolStripMenuItem,
            this.toolStripSeparator4,
            this.Method2});
            this.ToolToolStripMenuItem.Name = "ToolToolStripMenuItem";
            this.ToolToolStripMenuItem.Size = new System.Drawing.Size(125, 24);
            this.ToolToolStripMenuItem.Text = "Налаштування";
            // 
            // fontToolStripMenuItem
            // 
            this.fontToolStripMenuItem.Name = "fontToolStripMenuItem";
            this.fontToolStripMenuItem.Size = new System.Drawing.Size(344, 26);
            this.fontToolStripMenuItem.Text = "Шрифт та розмір тексту";
            this.fontToolStripMenuItem.Click += new System.EventHandler(this.FontToolStripMenuItem_Click);
            // 
            // darkModeToolStripMenuItem
            // 
            this.darkModeToolStripMenuItem.CheckOnClick = true;
            this.darkModeToolStripMenuItem.Name = "darkModeToolStripMenuItem";
            this.darkModeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.D)));
            this.darkModeToolStripMenuItem.Size = new System.Drawing.Size(344, 26);
            this.darkModeToolStripMenuItem.Text = "Темна тема";
            this.darkModeToolStripMenuItem.ToolTipText = "Програма перевідкривається, якщо був відкритий файл – зміни втрачаються.";
            this.darkModeToolStripMenuItem.CheckedChanged += new System.EventHandler(this.DarkModeToolStripMenuItem_CheckedChanged);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(341, 6);
            // 
            // Method2
            // 
            this.Method2.CheckOnClick = true;
            this.Method2.Name = "Method2";
            this.Method2.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.M)));
            this.Method2.Size = new System.Drawing.Size(344, 26);
            this.Method2.Text = "Підтримка .uasset/.umap";
            this.Method2.CheckedChanged += new System.EventHandler(this.Method2_CheckedChanged);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.commandLinesToolStripMenuItem,
            this.aboutToolStripMenuItem1});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(30, 24);
            this.aboutToolStripMenuItem.Text = "?";
            // 
            // commandLinesToolStripMenuItem
            // 
            this.commandLinesToolStripMenuItem.Name = "commandLinesToolStripMenuItem";
            this.commandLinesToolStripMenuItem.Size = new System.Drawing.Size(222, 26);
            this.commandLinesToolStripMenuItem.Text = "Список CLI команд";
            this.commandLinesToolStripMenuItem.Click += new System.EventHandler(this.CommandLinesToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem1
            // 
            this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(222, 26);
            this.aboutToolStripMenuItem1.Text = "Про програму";
            this.aboutToolStripMenuItem1.Click += new System.EventHandler(this.AboutToolStripMenuItem1_Click);
            // 
            // DataCount
            // 
            this.DataCount.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.DataCount.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.DataCount.Name = "DataCount";
            this.DataCount.Size = new System.Drawing.Size(69, 21);
            this.DataCount.Text = "----------";
            // 
            // StateLabel
            // 
            this.StateLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.StateLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.StateLabel.ForeColor = System.Drawing.Color.Maroon;
            this.StateLabel.Name = "StateLabel";
            this.StateLabel.Size = new System.Drawing.Size(69, 21);
            this.StateLabel.Text = "----------";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(313, 6);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TextName,
            this.TextValue,
            this.Index});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.dataGridView1.IsFilterApplied = false;
            this.dataGridView1.Location = new System.Drawing.Point(0, 24);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView1.Size = new System.Drawing.Size(982, 499);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.FilterApplied += new System.EventHandler(this.DataGridView1_FilterApplied);
            this.dataGridView1.FilterCleared += new System.EventHandler(this.DataGridView1_FilterCleared);
            this.dataGridView1.Sorted += new System.EventHandler(this.DataGridView1_Sorted);
            // 
            // TextName
            // 
            this.TextName.HeaderText = "ID";
            this.TextName.MaxInputLength = 2147483647;
            this.TextName.MinimumWidth = 6;
            this.TextName.Name = "TextName";
            this.TextName.ReadOnly = true;
            this.TextName.Width = 125;
            // 
            // TextValue
            // 
            this.TextValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.TextValue.HeaderText = "Text";
            this.TextValue.MaxInputLength = 2147483647;
            this.TextValue.MinimumWidth = 6;
            this.TextValue.Name = "TextValue";
            // 
            // Index
            // 
            this.Index.HeaderText = "Index";
            this.Index.MinimumWidth = 6;
            this.Index.Name = "Index";
            this.Index.Visible = false;
            this.Index.Width = 125;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Green;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(7, 31);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // searchBox
            // 
            this.searchBox.AutoSize = true;
            this.searchBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.searchBox.DataGridView = this.dataGridView1;
            this.searchBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.searchBox.Location = new System.Drawing.Point(0, 523);
            this.searchBox.Margin = new System.Windows.Forms.Padding(4);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(982, 35);
            this.searchBox.TabIndex = 2;
            this.searchBox.Visible = false;
            // 
            // StatusBlock
            // 
            this.StatusBlock.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.StatusBlock.AutoSize = true;
            this.StatusBlock.BackColor = System.Drawing.Color.White;
            this.StatusBlock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.StatusBlock.Controls.Add(this.StatusText);
            this.StatusBlock.Controls.Add(this.StatusTitle);
            this.StatusBlock.Controls.Add(this.pictureBox2);
            this.StatusBlock.Location = new System.Drawing.Point(284, 202);
            this.StatusBlock.Margin = new System.Windows.Forms.Padding(4);
            this.StatusBlock.Name = "StatusBlock";
            this.StatusBlock.Size = new System.Drawing.Size(410, 140);
            this.StatusBlock.TabIndex = 8;
            this.StatusBlock.Visible = false;
            // 
            // StatusText
            // 
            this.StatusText.AutoSize = true;
            this.StatusText.Location = new System.Drawing.Point(141, 80);
            this.StatusText.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.StatusText.Name = "StatusText";
            this.StatusText.Size = new System.Drawing.Size(35, 16);
            this.StatusText.TabIndex = 2;
            this.StatusText.Text = "-------";
            // 
            // StatusTitle
            // 
            this.StatusTitle.AutoSize = true;
            this.StatusTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatusTitle.Location = new System.Drawing.Point(137, 27);
            this.StatusTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.StatusTitle.Name = "StatusTitle";
            this.StatusTitle.Size = new System.Drawing.Size(137, 39);
            this.StatusTitle.TabIndex = 0;
            this.StatusTitle.Text = "----------";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(12, 15);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(110, 110);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // FrmMain
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 558);
            this.Controls.Add(this.StatusBlock);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.searchBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1000, 600);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UE Localizations Tool";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.FrmMain_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.FrmMain_DragEnter);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.StatusBlock.ResumeLayout(false);
            this.StatusBlock.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenFile;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportAllTextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importAllTextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private NDataGridView dataGridView1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem find;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem commandLinesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem filterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem noNamesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem withNamesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sortToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem valueToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ascendingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem descendingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ascendingToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem descendingToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripMenuItem Method2;
        private System.Windows.Forms.ToolStripMenuItem darkModeToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn TextName;
        private System.Windows.Forms.DataGridViewTextBoxColumn TextValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn Index;
        private System.Windows.Forms.ToolStripMenuItem csvFileToolStripMenuItem;
        private Controls.SearchBox searchBox;
        private System.Windows.Forms.ToolStripMenuItem clearFilterToolStripMenuItem;
        private ToolStripLabel StateLabel;
        private ToolStripLabel DataCount;
        private ToolStripMenuItem locresOprationsToolStripMenuItem;
        private ToolStripMenuItem editSelectedRowToolStripMenuItem;
        private ToolStripMenuItem removeSelectedRowToolStripMenuItem;
        private ToolStripMenuItem addNewRowToolStripMenuItem;
        private ToolStripMenuItem mergeLocresFileToolStripMenuItem;
        private Label StatusText;
        private Label StatusTitle;
        private PictureBox pictureBox2;
        private Panel StatusBlock;
        private ToolStripMenuItem mergeUassetFileToolStripMenuItem;
        private ToolStripMenuItem replaceToolStripMenuItem;
        private ToolStripMenuItem transferTextHashFromOriginalLocresToolStripMenuItem;
        private ToolStripMenuItem mergeLocresFileStableNEWToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripMenuItem importAllTextByKeystoolStripMenuItem;
        private ToolStripMenuItem importNewLinesFromCSVtoolStripMenuItem;
        private ToolStripMenuItem fontToolStripMenuItem;
        private ToolStripMenuItem ToolToolStripMenuItem;
    }
}

