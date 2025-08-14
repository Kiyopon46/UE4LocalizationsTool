using AssetParser;
using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using UE4LocalizationsTool.Core.locres;

namespace UE4LocalizationsTool.Helper
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
                BadDataFound = null // ігнорувати зайві стовпці
            };
        }

        public void Load(NDataGridView dataGrid, string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, GetConfig()))
            {
                if (HasHeader)
                {
                    csv.Read();
                    csv.ReadHeader(); // пропускаємо заголовок
                }

                int rowIndex = 0;
                foreach (DataGridViewRow row in dataGrid.Rows)
                {
                    if (!csv.Read()) break; // якщо CSV закінчився

                    var record = csv.Parser.Record;
                    if (record.Length < 3) continue;

                    var value = record[2];
                    if (!string.IsNullOrEmpty(value))
                        dataGrid.SetValue(row.Cells["Text value"], value);

                    rowIndex++;
                }
            }
        }

        public void LoadByKeys(NDataGridView dataGrid, string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, GetConfig()))
            {
                if (HasHeader)
                {
                    csv.Read();
                    csv.ReadHeader(); // пропускаємо заголовок
                }

                while (csv.Read())
                {
                    var record = csv.Parser.Record;
                    if (record.Length < 3 || string.IsNullOrEmpty(record[0]) || record[0].StartsWith("#"))
                        continue;

                    var key = record[0];
                    var value = record[2];
                    if (string.IsNullOrEmpty(value)) continue;

                    foreach (DataGridViewRow row in dataGrid.Rows)
                    {
                        if (row.IsNewRow) continue;
                        if (row.Cells["Name"].Value != null && row.Cells["Name"].Value.ToString() == key)
                        {
                            dataGrid.SetValue(row.Cells["Text value"], value);
                            break;
                        }
                    }
                }
            }
        }

        public void LoadNewLines(NDataGridView dataGrid, string filePath, LocresFile asset)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, GetConfig()))
            {
                if (HasHeader)
                {
                    csv.Read();
                    csv.ReadHeader(); // пропускаємо заголовок
                }

                while (csv.Read())
                {
                    var record = csv.Parser.Record;
                    if (record.Length < 2 || string.IsNullOrEmpty(record[1])) continue;

                    string rowName = record[0];
                    string value = record[1];

                    if (record.Length > 2 && !string.IsNullOrEmpty(record[2]))
                        value = record[2];

                    var dt = (System.Data.DataTable)dataGrid.DataSource;
                    var hashTable = new HashTable
                    {
                        NameHash = 0,
                        KeyHash = 0,
                        ValueHash = asset.CalcHashExperimental(value)
                    };

                    dt.Rows.Add(rowName, value, hashTable);
                }
            }
        }

        public void Save(DataGridView dataGrid, string filePath)
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
        }

        public string[] Load(string filePath, bool NoNames = false)
        {
            var list = new List<string>();
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, GetConfig()))
            {
                while (csv.Read())
                {
                    var record = csv.Parser.Record;
                    if (record == null || record.Length == 0) continue;
                    list.Add(Merge(record, NoNames));
                }
            }
            return list.ToArray();
        }

        private string Merge(string[] strings, bool NoNames = false)
        {
            int i = 0;
            int CollsCount = !NoNames ? 2 : 3;
            string text = "";
            if (!NoNames && strings.Length > 0 && strings[i++] != "[~PATHFile~]")
                text += strings[i - 1] + "=";
            else if (strings.Length > 0)
                return strings[i];
            else
                return "";

            if (strings.Length < CollsCount || string.IsNullOrEmpty(strings.LastOrDefault()))
                text += strings.Length > i ? strings[i++] : "";
            else
                text += strings.LastOrDefault();

            return text;
        }

        public void Save(List<List<string>> strings, string filePath, bool NoNames = true)
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
        }
    }
}