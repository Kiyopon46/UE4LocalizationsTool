using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using UELocalizationsTool.Core.locres;

namespace UELocalizationsTool.Helper
{
    public class CSVFile
    {
        public static CSVFile Instance { get; } = new CSVFile();
        public char Delimiter { get; set; } = ',';
        public bool HasHeader { get; set; } = true;

        private CsvConfiguration GetConfig()
        {
            return new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = HasHeader,
                Delimiter = Delimiter.ToString(),
                Quote = '"',
                ShouldQuote = args => true,
                BadDataFound = null
            };
        }

        public async Task Load(NDataGridView dataGrid, string filePath)
        {
            await Task.Run(() =>
            {
                var records = new List<dynamic>();
                using (var reader = new StreamReader(filePath))
                using (var csv = new CsvReader(reader, GetConfig()))
                {
                    records = csv.GetRecords<dynamic>().ToList();
                }

                var updates = new List<(int rowIndex, string newValue, bool shouldHighlight)>();

                for (int i = 0; i < Math.Min(dataGrid.Rows.Count, records.Count); i++)
                {
                    var record = (IDictionary<string, object>)records[i];

                    if (record.ContainsKey("Translation"))
                    {
                        var newValue = record["Translation"]?.ToString();

                        if (!string.IsNullOrWhiteSpace(newValue))
                        {
                            var originalValue = dataGrid.Rows[i].Cells["Text value"].Value?.ToString();
                            bool shouldHighlight = originalValue != newValue;

                            updates.Add((i, newValue, shouldHighlight));
                        }
                    }
                }

                dataGrid.Invoke((MethodInvoker)delegate
                {
                    dataGrid.SuspendLayout();

                    foreach (var update in updates)
                    {
                        if (update.shouldHighlight)
                        {
                            dataGrid.Rows[update.rowIndex].Cells["Text value"].Style.BackColor = Color.Yellow;
                        }
                        dataGrid.Rows[update.rowIndex].Cells["Text value"].Value = update.newValue;
                    }

                    dataGrid.ResumeLayout();
                });
            });
        }

        public async Task LoadByKeys(NDataGridView dataGrid, string filePath)
        {
            await Task.Run(() =>
            {
                var dataGridRows = new Dictionary<string, DataGridViewRow>();
                foreach (DataGridViewRow row in dataGrid.Rows)
                {
                    if (row.IsNewRow) continue;
                    var key = row.Cells["Name"].Value?.ToString();
                    if (!string.IsNullOrEmpty(key))
                    {
                        dataGridRows[key] = row;
                    }
                }

                var updates = new List<(DataGridViewRow row, string newValue, bool shouldHighlight)>();

                using (var reader = new StreamReader(filePath))
                using (var csv = new CsvReader(reader, GetConfig()))
                {
                    var records = csv.GetRecords<dynamic>().ToList();

                    foreach (var record in records)
                    {
                        var recordDict = (IDictionary<string, object>)record;
                        if (recordDict.ContainsKey("key") && recordDict.ContainsKey("Translation"))
                        {
                            string key = recordDict["key"]?.ToString();
                            string newValue = recordDict["Translation"]?.ToString();

                            if (!string.IsNullOrEmpty(key) && dataGridRows.ContainsKey(key))
                            {
                                var rowToUpdate = dataGridRows[key];
                                var originalValue = rowToUpdate.Cells["Text value"].Value?.ToString();

                                if (!string.IsNullOrWhiteSpace(newValue))
                                {
                                    bool shouldHighlight = originalValue != newValue;
                                    updates.Add((rowToUpdate, newValue, shouldHighlight));
                                }
                            }
                        }
                    }
                }

                dataGrid.Invoke((MethodInvoker)delegate
                {
                    dataGrid.SuspendLayout();

                    foreach (var update in updates)
                    {
                        if (update.shouldHighlight)
                        {
                            update.row.Cells["Text value"].Style.BackColor = Color.Yellow;
                        }
                        update.row.Cells["Text value"].Value = update.newValue;
                    }

                    dataGrid.ResumeLayout();
                });
            });
        }

        public async Task LoadNewLines(NDataGridView dataGrid, string filePath, LocresFile asset)
        {
            await Task.Run(() =>
            {
                var dt = dataGrid.DataSource as System.Data.DataTable;
                if (dt == null) return;

                var existingNames = new HashSet<string>();
                foreach (DataRow row in dt.Rows)
                {
                    if (row["Name"] != DBNull.Value)
                    {
                        existingNames.Add(row["Name"].ToString());
                    }
                }

                using (var reader = new StreamReader(filePath))
                using (var csv = new CsvReader(reader, GetConfig()))
                {
                    var records = csv.GetRecords<dynamic>().ToList();

                    foreach (var record in records)
                    {
                        var recordDict = (IDictionary<string, object>)record;
                        if (recordDict.ContainsKey("key") && recordDict.ContainsKey("Translation"))
                        {
                            string rowName = recordDict["key"]?.ToString();
                            string value = recordDict["Translation"]?.ToString();

                            if (!string.IsNullOrEmpty(rowName) && !string.IsNullOrEmpty(value) && !existingNames.Contains(rowName))
                            {
                                var hashTable = new HashTable
                                {
                                    NameHash = 0,
                                    KeyHash = 0,
                                    ValueHash = asset.CalcHashExperimental(value)
                                };
                                dt.Rows.Add(rowName, value, hashTable);
                                existingNames.Add(rowName);
                            }
                        }
                    }
                }
            });
        }

        public async Task Save(DataGridView dataGrid, string filePath)
        {
            await Task.Run(() =>
            {
                using (var writer = new StreamWriter(filePath))
                using (var csv = new CsvWriter(writer, GetConfig()))
                {
                    if (HasHeader)
                    {
                        csv.WriteField("key");
                        csv.WriteField("source");
                        csv.WriteField("Translation");
                        csv.NextRecord();
                    }

                    foreach (DataGridViewRow row in dataGrid.Rows)
                    {
                        if (row.IsNewRow) continue;
                        csv.WriteField(row.Cells["Name"].Value?.ToString() ?? "");
                        csv.WriteField(row.Cells["Text value"].Value?.ToString() ?? "");
                        csv.WriteField("");
                        csv.NextRecord();
                    }
                }
            });
        }

        public async Task<string[]> Load(string filePath, bool NoNames = false)
        {
            return await Task.Run(() =>
            {
                var list = new List<string>();
                using (var reader = new StreamReader(filePath))
                using (var csv = new CsvReader(reader, GetConfig()))
                {
                    var records = csv.GetRecords<dynamic>().ToList();

                    foreach (var record in records)
                    {
                        var recordDict = (IDictionary<string, object>)record;
                        string key = recordDict.ContainsKey("key") ? recordDict["key"]?.ToString() : null;
                        string translation = recordDict.ContainsKey("Translation") ? recordDict["Translation"]?.ToString() : null;
                        string source = recordDict.ContainsKey("source") ? recordDict["source"]?.ToString() : null;

                        if (NoNames)
                        {
                            if (!string.IsNullOrEmpty(translation))
                            {
                                list.Add(translation);
                            }
                            else if (!string.IsNullOrEmpty(source))
                            {
                                list.Add(source);
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(translation))
                            {
                                list.Add($"{key}={translation}");
                            }
                            else if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(source))
                            {
                                list.Add($"{key}={source}");
                            }
                        }
                    }
                }
                return list.ToArray();
            });
        }

        public async Task Save(List<List<string>> strings, string filePath, bool NoNames = true)
        {
            await Task.Run(() =>
            {
                using (var writer = new StreamWriter(filePath))
                using (var csv = new CsvWriter(writer, GetConfig()))
                {
                    if (HasHeader)
                    {
                        if (NoNames)
                        {
                            csv.WriteField("source");
                            csv.WriteField("Translation");
                        }
                        else
                        {
                            csv.WriteField("key");
                            csv.WriteField("source");
                            csv.WriteField("Translation");
                        }
                        csv.NextRecord();
                    }

                    foreach (var row in strings)
                    {
                        if (NoNames)
                        {
                            csv.WriteField(row.Count > 1 ? row[1] ?? "" : "");
                            csv.WriteField("");
                        }
                        else
                        {
                            csv.WriteField(row.Count > 0 ? row[0] ?? "" : "");
                            csv.WriteField(row.Count > 1 ? row[1] ?? "" : "");
                            csv.WriteField("");
                        }
                        csv.NextRecord();
                    }
                }
            });
        }
    }
}