using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace UE4LocalizationsTool.Controls
{
    public partial class SearchBox : UserControl
    {
        private const string DefaultColumnName = "Text value";
        private string _columnName = DefaultColumnName;

        [Browsable(true)]
        public NDataGridView DataGridView { get; set; }
        private int _currentRowIndex = -1;
        private int _currentColumnIndex = -1;

        public SearchBox()
        {
            InitializeComponent();
            Hide();
        }

        public SearchBox(NDataGridView dataGrid)
        {
            DataGridView = dataGrid;
            InitializeComponent();
            Hide();
        }

        private void SearchHide_Click(object sender, System.EventArgs e)
        {
            Hide();
            listView1.Visible = false;
            label2.Text = string.Empty;
        }

        // Оптимізований метод IsMatch
        // Тепер приймає текст для пошуку як параметр, що усуває повторне звернення до InputSearch.Text
        private bool IsMatch(string value, string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
                return false;

            return value.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        // Оптимізований і спрощений метод FindCell
        private DataGridViewCell FindCell(int startRowIndex, string searchText, bool searchUpwards = false)
        {
            int step = searchUpwards ? -1 : 1;
            int count = DataGridView.Rows.Count;

            // Якщо таблиця порожня або колонка не існує, виходимо
            if (count == 0 || !DataGridView.Columns.Contains(_columnName))
                return null;

            // Цикл пошуку
            for (int i = 0; i < count; i++)
            {
                int rowIndex = (startRowIndex + i * step + count) % count;

                // Перевірка на вихід за межі, щоб уникнути помилок
                if (rowIndex < 0 || rowIndex >= count) continue;

                DataGridViewCell cell = DataGridView.Rows[rowIndex].Cells[_columnName];
                if (cell.Value != null && IsMatch(cell.Value.ToString(), searchText))
                {
                    return cell;
                }
            }
            return null;
        }

        private void FindNext_Click(object sender, EventArgs e)
        {
            if (DataGridView.Rows.Count == 0 || string.IsNullOrWhiteSpace(InputSearch.Text))
            {
                Failedmessage();
                return;
            }

            // Починаємо пошук з наступного рядка після поточного
            int startIndex = _currentRowIndex == -1 ? 0 : _currentRowIndex + 1;
            var cell = FindCell(startIndex, InputSearch.Text);

            if (cell != null)
            {
                SelectCell(cell.RowIndex, cell.ColumnIndex);
            }
            else
            {
                Failedmessage();
            }
        }

        private void FindPrevious_Click(object sender, EventArgs e)
        {
            if (DataGridView.Rows.Count == 0 || string.IsNullOrWhiteSpace(InputSearch.Text))
            {
                Failedmessage();
                return;
            }

            // Починаємо пошук з попереднього рядка від поточного
            int startIndex = _currentRowIndex == -1 ? DataGridView.Rows.Count - 1 : _currentRowIndex - 1;
            var cell = FindCell(startIndex, InputSearch.Text, true);

            if (cell != null)
            {
                SelectCell(cell.RowIndex, cell.ColumnIndex);
            }
            else
            {
                Failedmessage();
            }
        }

        private void Failedmessage()
        {
            MessageBox.Show(
                text: $"The searched value '{InputSearch.Text}' not found.",
                caption: "Search Result",
                buttons: MessageBoxButtons.OK,
                icon: MessageBoxIcon.Warning
            );
        }

        private void SelectCell(int rowIndex, int colIndex)
        {
            if (DataGridView == null || rowIndex < 0 || colIndex < 0 || rowIndex >= DataGridView.Rows.Count || colIndex >= DataGridView.Columns.Count)
                return;

            DataGridView.ClearSelection();
            DataGridView.Rows[rowIndex].Selected = true;
            DataGridView.FirstDisplayedScrollingRowIndex = rowIndex;
            _currentRowIndex = rowIndex;
            _currentColumnIndex = colIndex;
        }

        private void InputSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FindNext_Click(sender, e);
            }
        }

        public new void Show()
        {
            base.Show();
            InputSearch.Focus();
            if (DataGridView?.CurrentCell != null)
            {
                InputSearch.Text = DataGridView.CurrentCell.Value?.ToString();
            }
            label2.Text = string.Empty;
        }

        public void ShowReplacePanel()
        {
            Replacepanel.Visible = true;
            Show();
            txtReplace.Focus();
        }

        // Оптимізований метод CountTotalMatches
        // Тепер перевіряє тільки одну колонку, що значно швидше
        public int CountTotalMatches()
        {
            if (string.IsNullOrWhiteSpace(InputSearch.Text) || DataGridView.Rows.Count == 0 || !DataGridView.Columns.Contains(_columnName))
            {
                return 0;
            }

            int totalMatches = 0;
            string searchText = InputSearch.Text;
            foreach (DataGridViewRow row in DataGridView.Rows)
            {
                if (row.Cells[_columnName].Value != null && IsMatch(row.Cells[_columnName].Value.ToString(), searchText))
                {
                    totalMatches++;
                }
            }
            return totalMatches;
        }

        private void FindAll_Click(object sender, EventArgs e)
        {
            Replacepanel.Visible = false;

            if (DataGridView.Rows.Count == 0 || string.IsNullOrWhiteSpace(InputSearch.Text))
            {
                Failedmessage();
                return;
            }

            listView1.Items.Clear();
            string searchText = InputSearch.Text;

            // Оптимізований цикл
            if (DataGridView.Columns.Contains(_columnName))
            {
                foreach (DataGridViewRow row in DataGridView.Rows)
                {
                    if (row.Cells[_columnName].Value != null && IsMatch(row.Cells[_columnName].Value.ToString(), searchText))
                    {
                        var item = new ListViewItem((row.Index + 1).ToString());
                        item.SubItems.Add(row.Cells[_columnName].Value.ToString());
                        item.Tag = row;
                        listView1.Items.Add(item);
                    }
                }
            }

            listView1.Visible = true;
            label2.Text = $"Total matches: {listView1.Items.Count}";
        }

        private void Replace_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(InputSearch.Text) || string.IsNullOrWhiteSpace(txtReplace.Text))
            {
                MessageBox.Show("Search or replace text cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var selectedCell = DataGridView.SelectedCells.Count > 0 ? DataGridView.SelectedCells[0] : null;

            if (selectedCell != null && IsMatch(selectedCell.Value?.ToString(), InputSearch.Text))
            {
                ReplaceCell(selectedCell);
            }
            else
            {
                var cell = FindCell(_currentRowIndex, InputSearch.Text);
                if (cell != null)
                {
                    SelectCell(cell.RowIndex, cell.ColumnIndex);
                    ReplaceCell(cell);
                }
                else
                {
                    Failedmessage();
                }
            }
        }

        // Винесено в окремий приватний метод для чистоти коду
        private void ReplaceCell(DataGridViewCell cell)
        {
            if (cell?.Value == null) return;
            DataGridView.SetValue(cell, Regex.Replace(cell.Value.ToString(), InputSearch.Text, txtReplace.Text, RegexOptions.IgnoreCase));
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Replacepanel.Visible = false;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count == 0)
                return;
            var cell = (listView1.SelectedItems[0].Tag as DataGridViewRow).Cells[_columnName];
            SelectCell(cell.RowIndex, cell.ColumnIndex);
        }

        private void ReplaceAll_Click(object sender, EventArgs e)
        {
            if (DataGridView.Rows.Count == 0)
            {
                MessageBox.Show("No data found.", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrWhiteSpace(InputSearch.Text) || string.IsNullOrWhiteSpace(txtReplace.Text))
            {
                MessageBox.Show("Search or replace text cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int totalMatches = 0;
            string searchText = InputSearch.Text;
            string replaceText = txtReplace.Text;

            // Оптимізація для швидкого оновлення UI
            DataGridView.SuspendLayout();
            try
            {
                foreach (DataGridViewRow row in DataGridView.Rows)
                {
                    if (DataGridView.Columns.Contains(_columnName))
                    {
                        if (row.Cells[_columnName].Value != null && IsMatch(row.Cells[_columnName].Value.ToString(), searchText))
                        {
                            DataGridView.SetValue(row.Cells[_columnName], Regex.Replace(row.Cells[_columnName].Value.ToString(), searchText, replaceText, RegexOptions.IgnoreCase));
                            totalMatches++;
                        }
                    }
                }
            }
            finally
            {
                DataGridView.ResumeLayout();
            }

            MessageBox.Show($"Total matches replaced: {totalMatches}", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}