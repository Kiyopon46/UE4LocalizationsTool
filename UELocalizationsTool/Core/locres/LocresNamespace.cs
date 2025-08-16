using AssetParser;
using Helper.MemoryList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UELocalizationsTool.Core.Hash;

namespace UELocalizationsTool.Core.locres
{
    public class HashTable
    {
        public uint NameHash { get; set; }
        public uint KeyHash { get; set; }
        public uint ValueHash { get; set; }

        public HashTable()
        {

        }
        public HashTable(uint NameHash, uint KeyHash = 0, uint ValueHash = 0)
        {
            this.NameHash = NameHash;
            this.KeyHash = KeyHash;
            this.ValueHash = ValueHash;
        }

        public override string ToString()
        {
            return $"NameHash: {NameHash} KeyHash: {KeyHash} ValueHash: {ValueHash}";
        }
    }

    public class NameSpaceTable : List<StringTable>
    {
        public uint NameHash { get; set; } = 0;
        public string Name { get; set; }
        public NameSpaceTable(string TableName, uint NameHash = 0)
        {
            this.Name = TableName;
            this.NameHash = NameHash;
        }

        public StringTable this[string key]
        {
            get
            {
                int index = FindIndex(x => x.Key == key);
                if (index >= 0)
                {
                    return this[index];
                }
                throw new KeyNotFoundException($"key '{key}' not found.");
            }
            set
            {
                int index = FindIndex(x => x.Key == key);
                if (index >= 0)
                {
                    this[index] = value;
                }
                else
                {
                    throw new KeyNotFoundException($"key '{key}' not found.");
                }
            }
        }

        public bool ContainsKey(string key)
        {
            return FindIndex(x => x.Key == key) >= 0;
        }

        public void RemoveKey(string key)
        {
            int index = FindIndex(x => x.Key == key);
            if (index >= 0)
            {
                RemoveAt(index);
            }
        }

        //why?! i don't know but it's work :D
        public new void Add(StringTable table)
        {
            table.root = this;
            base.Add(table);
        }
    }

    public class StringTable
    {
        public uint KeyHash { get; set; }
        public string Key { get; set; }
        public uint ValueHash { get; set; }
        public string Value { get; set; }
        public uint ExternID { get; set; } = 0;
        public NameSpaceTable root;

        public StringTable()
        {

        }

        public StringTable(string TableKey, string TableValue, uint keyHash = 0, uint ValueHash = 0, uint ExternID = 0)
        {
            this.Key = TableKey;
            this.Value = TableValue;
            this.KeyHash = keyHash;
            this.ValueHash = ValueHash;
            this.ExternID = ExternID;
        }
    }

    public enum LocresVersion : byte
    {
        Legacy = 0,
        Compact,
        Optimized,
        Optimized_CityHash64_UTF16,
        Optimized_CityHash64_ExternID_UTF16,
    }

    public class LocresFile : List<NameSpaceTable>, IAsset
    {
        public NameSpaceTable this[string Name]
        {
            get
            {
                int index = FindIndex(x => x.Name == Name);
                if (index >= 0)
                {
                    return this[index];
                }
                throw new KeyNotFoundException($"key '{Name}' not found.");
            }
            set
            {
                int index = FindIndex(x => x.Name == Name);
                if (index >= 0)
                {
                    this[index] = value;
                }
                else
                {
                    throw new KeyNotFoundException($"key '{Name}' not found.");
                }
            }
        }

        bool ContainsKey(string key)
        {
            return FindIndex(x => x.Name == key) >= 0;
        }

        //{7574140E-4A67-FC03-4A15-909DC3377F1B}
        private readonly byte[] MagicGUID = { 0x0E, 0x14, 0x74, 0x75, 0x67, 0x4A, 0x03, 0xFC, 0x4A, 0x15, 0x90, 0x9D, 0xC3, 0x37, 0x7F, 0x1B };
        public bool IsGood { get; set; } = true;
        MemoryList locresData;
        public LocresVersion Version;
        public LocresFile(string FilePath)
        {
            locresData = new MemoryList(FilePath);
            Load();
        }

        void Load()
        {
            locresData.Seek(0);
            byte[] fileGUID = locresData.GetBytes(16);

            // Визначаємо версію
            if (fileGUID.SequenceEqual(MagicGUID))
                Version = (LocresVersion)locresData.GetByteValue();
            else
            {
                Version = LocresVersion.Legacy;
                locresData.Seek(0);
            }

            if (Version > LocresVersion.Optimized_CityHash64_ExternID_UTF16)
                throw new Exception("Unsupported locres version");

            string[] localizedStrings = new string[0];

            // Compact/Optimized формати з масивом рядків
            if (Version >= LocresVersion.Compact)
            {
                int localizedStringOffset = (int)locresData.GetInt64Value();
                int currentPos = locresData.GetPosition();

                locresData.Seek(localizedStringOffset);

                int localizedStringCount = locresData.GetIntValue();
                localizedStrings = new string[localizedStringCount];

                for (int i = 0; i < localizedStringCount; i++)
                {
                    localizedStrings[i] = locresData.GetStringUE();

                    if (Version >= LocresVersion.Optimized)
                        locresData.Skip(4); // ref count
                }

                locresData.Seek(currentPos);
            }

            // Legacy формат
            if (Version == LocresVersion.Legacy)
            {
                int hashTableCount = locresData.GetIntValue();

                for (int i = 0; i < hashTableCount; i++)
                {
                    string nsHash = locresData.GetStringUE();
                    int stringCount = locresData.GetIntValue();

                    for (int j = 0; j < stringCount; j++)
                    {
                        string keyHash = locresData.GetStringUE();
                        uint valueHash = locresData.GetUIntValue();
                        string value = locresData.GetStringUE();
                        AddString(nsHash, keyHash, value, ValueHash: valueHash);
                    }
                }
                return;
            }

            // Пропуск ключів у Optimized форматах (не використовується)
            if (Version >= LocresVersion.Optimized)
                locresData.Skip(4);

            int namespaceCount = locresData.GetIntValue();

            for (int n = 0; n < namespaceCount; n++)
            {
                string nsStr;
                uint nsHash;
                ReadTextKey(locresData, Version, out nsHash, out nsStr);

                uint keyCount = locresData.GetUIntValue();

                for (int k = 0; k < keyCount; k++)
                {
                    string keyStr;
                    uint keyHash;
                    ReadTextKey(locresData, Version, out keyHash, out keyStr);

                    uint valueHash = locresData.GetUIntValue();

                    if (Version >= LocresVersion.Compact)
                    {
                        int localizedIndex = locresData.GetIntValue();
                        string value = localizedStrings[localizedIndex];

                        uint externID = 0;
                        if (Version == LocresVersion.Optimized_CityHash64_ExternID_UTF16)
                            externID = locresData.GetUIntValue();

                        AddString(nsStr, keyStr, value, nsHash, keyHash, valueHash);
                        if (Version == LocresVersion.Optimized_CityHash64_ExternID_UTF16)
                            this[nsStr][keyStr].ExternID = externID;
                    }
                }
            }
        }

        void ReadTextKey(MemoryList memoryList, LocresVersion locresVersion, out uint StrHash, out string Str)
        {
            StrHash = 0;
            Str = "";
            if (locresVersion >= LocresVersion.Optimized)
            {
                StrHash = memoryList.GetUIntValue();//crc32
            }

            Str = memoryList.GetStringUE();
        }

        //build
        private void Save()
        {
            locresData.Clear();

            if (Version == LocresVersion.Legacy)
            {
                BuildLegacyFile();
                return;
            }

            // Заголовок файлу
            locresData.SetBytes(MagicGUID);
            locresData.SetByteValue((byte)Version);

            // Місце для offset масиву рядків
            long localizedStringOffsetPos = locresData.GetPosition();
            locresData.SetInt64Value(0);

            // Кількість ключів для Optimized форматів (не використовується, але потрібна)
            if (Version >= LocresVersion.Optimized)
            {
                int totalKeys = this.Sum(ns => ns.Count);
                locresData.SetIntValue(totalKeys);
            }

            locresData.SetIntValue(Count); // namespace count

            // Збираємо унікальні локалізовані рядки
            var stringTable = new List<StringEntry>();

            foreach (var ns in this)
            {
                if (Version >= LocresVersion.Optimized)
                {
                    locresData.SetUIntValue(ns.NameHash != 0 ? ns.NameHash : CalcHash(ns.Name));
                }

                locresData.SetStringUE(ns.Name);
                locresData.SetIntValue(ns.Count);

                foreach (var entry in ns)
                {
                    if (Version >= LocresVersion.Optimized)
                    {
                        locresData.SetUIntValue(entry.KeyHash != 0 ? entry.KeyHash : CalcHash(entry.Key));
                    }

                    locresData.SetStringUE(entry.Key);

                    locresData.SetUIntValue(entry.ValueHash != 0 ? entry.ValueHash : entry.Value.StrCrc32());

                    int stringIndex = stringTable.FindIndex(x => x.Text == entry.Value);
                    if (stringIndex == -1)
                    {
                        stringIndex = stringTable.Count;
                        stringTable.Add(new StringEntry() { Text = entry.Value, RefCount = 1 });
                    }
                    else
                    {
                        stringTable[stringIndex].RefCount++;
                    }

                    locresData.SetIntValue(stringIndex);

                    if (Version == LocresVersion.Optimized_CityHash64_ExternID_UTF16)
                        locresData.SetUIntValue(entry.ExternID);
                }
            }

            // Запис масиву локалізованих рядків
            int localizedStringOffset = locresData.GetPosition();
            locresData.SetIntValue(stringTable.Count);

            foreach (var s in stringTable)
            {
                locresData.SetStringUE(s.Text);
                if (Version >= LocresVersion.Optimized)
                    locresData.SetIntValue(s.RefCount);
            }

            // Встановлюємо offset у заголовку
            locresData.Seek((int)localizedStringOffsetPos);
            locresData.SetInt64Value(localizedStringOffset);
        }

        public void SaveFile(string FilPath)
        {
            Save();
            locresData.WriteFile(FilPath);
        }



        private void BuildLegacyFile()
        {
            locresData.SetIntValue(Count);


            foreach (var names in this)
            {
                locresData.SetStringUE(names.Name, true);
                locresData.SetIntValue(names.Count);
                foreach (var table in names)
                {
                    locresData.SetStringUE(table.Key, true);

                    if (table.ValueHash == 0)
                    {
                        locresData.SetUIntValue(table.Value.StrCrc32());
                    }
                    else
                    {
                        locresData.SetUIntValue(table.ValueHash);
                    }

                    locresData.SetStringUE(table.Value, true);
                }
            }


        }

        public void AddString(string NameSpace, string key, string value, uint NameSpaceHash = 0, uint keyHash = 0, uint ValueHash = 0, uint ExternID = 0)
        {
            // Додаємо простір імен, якщо його ще немає
            if (!ContainsKey(NameSpace))
            {
                Add(new NameSpaceTable(NameSpace, NameSpaceHash));
            }

            var nsTable = this[NameSpace];

            // Додаємо ключ, якщо його ще немає
            if (!nsTable.ContainsKey(key))
            {
                var newEntry = new StringTable(key, value, keyHash, ValueHash, ExternID);

                // Якщо версія v4, зберігаємо ExternID
                if (Version == LocresVersion.Optimized_CityHash64_ExternID_UTF16)
                    newEntry.ExternID = ExternID;

                nsTable.Add(newEntry);
            }
            else
            {
                var existingEntry = nsTable[key];
                existingEntry.Value = value;

                if (Version == LocresVersion.Optimized_CityHash64_ExternID_UTF16)
                    existingEntry.ExternID = ExternID;

                if (keyHash != 0) existingEntry.KeyHash = keyHash;
                if (ValueHash != 0) existingEntry.ValueHash = ValueHash;
                if (NameSpaceHash != 0) nsTable.NameHash = NameSpaceHash;
            }
        }

        public void RemoveString(string NameSpace, string key)
        {
            this[NameSpace].RemoveKey(key);
        }

        public void RemoveNameSpace(string NameSpace)
        {
            int index = FindIndex(x => x.Name == NameSpace);
            if (index >= 0)
            {
                RemoveAt(index);
            }
        }
        public uint Optimized_CityHash64_UTF16Hash(string value)
        {
            var Hash = CityHash.CityHash64(Encoding.Unicode.GetBytes(value));
            return (uint)Hash + ((uint)(Hash >> 32) * 23);
        }


        public void AddItemsToDataGridView(DataGridView dataGrid)
        {
            dataGrid.DataSource = null;
            dataGrid.Rows.Clear();
            dataGrid.Columns.Clear();

            var dataTable = new System.Data.DataTable();
            dataTable.Columns.Add("ID", typeof(string));
            dataTable.Columns.Add("Text", typeof(string));
            dataTable.Columns.Add("Hash Table", typeof(HashTable));

            foreach (var names in this)
            {
                foreach (var table in names)
                {
                    string name = string.IsNullOrEmpty(names.Name) ? table.Key : names.Name + "::" + table.Key;
                    string textValue = table.Value;
                    dataTable.Rows.Add(name, textValue, new HashTable(names.NameHash, table.KeyHash, table.ValueHash));
                }
            }
            dataGrid.DataSource = dataTable;
            dataGrid.Columns["Text"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGrid.Columns["Hash Table"].Visible = false;
        }

        public void LoadFromDataGridView(DataGridView dataGrid)
        {
            Clear();
            foreach (DataGridViewRow row in dataGrid.Rows)
            {
                //0-> NameSpace
                //1-> Key
                var items = row.Cells["ID"].Value.ToString().Split(new string[] { "::" }, StringSplitOptions.None);

                string NameSpaceStr = "";
                string KeyStr = "";

                if (items.Length == 2)
                {
                    NameSpaceStr = items[0];
                    KeyStr = items[1];

                }
                else
                {
                    KeyStr = items[0];
                }
                var HashTable = row.Cells["Hash Table"].Value as HashTable;
                AddString(NameSpaceStr, KeyStr, row.Cells["Text"].Value.ToString(), HashTable.NameHash, HashTable.KeyHash, HashTable.ValueHash);
            }
        }

        public List<List<string>> ExtractTexts()
        {
            var strings = new List<List<string>>();
            foreach (var names in this)
            {
                foreach (var table in names)
                {
                    if (!string.IsNullOrEmpty(names.Name))
                        strings.Add(new List<string>() { names.Name + "::" + table.Key, table.Value });
                    else
                        strings.Add(new List<string>() { table.Key, table.Value });

                }
            }
            return strings;
        }

        public void ImportTexts(List<List<string>> strings)
        {
            int i = 0;
            foreach (var names in this)
            {
                foreach (var table in names)
                {
                    table.Value = strings[i++][1];
                }
            }
        }

        public uint CalcHash(string Str)
        {
            if (string.IsNullOrEmpty(Str))
                return 0;

            if (Version == LocresVersion.Optimized_CityHash64_UTF16 ||
                Version == LocresVersion.Optimized_CityHash64_ExternID_UTF16) // locres v4
                return Optimized_CityHash64_UTF16Hash(Str);
            else if (Version >= LocresVersion.Optimized)
                return Str.StrCrc32();
            else
                return 0;
        }

        // Generate hash anyway (don't return 0)
        public uint CalcHashExperimental(string Str)
        {
            if (string.IsNullOrEmpty(Str))
            {
                return 0;
            }

            if (Version == LocresVersion.Optimized_CityHash64_UTF16)
                return Optimized_CityHash64_UTF16Hash(Str);
            else
                return Str.StrCrc32();
        }

        public class StringEntry
        {
            public string Text { get; set; }
            public int RefCount { get; set; }
        }
    }
}
