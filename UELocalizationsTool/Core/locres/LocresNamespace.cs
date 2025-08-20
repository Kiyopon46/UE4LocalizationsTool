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
        public uint ExternID { get; set; } = 0;

        public HashTable() { }

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
        private Dictionary<string, StringTable> keyLookup = new Dictionary<string, StringTable>();

        public uint NameHash { get; set; } = 0;
        public string Name { get; set; }

        public NameSpaceTable(string TableName, uint NameHash = 0)
        {
            this.Name = TableName;
            this.NameHash = NameHash;
        }

        public StringTable this[string key]
        {
            get => keyLookup[key];
            set => keyLookup[key] = value;
        }

        public bool ContainsKey(string key) => keyLookup.ContainsKey(key);

        public void RemoveKey(string key)
        {
            if (keyLookup.TryGetValue(key, out var table))
            {
                base.Remove(table);
                keyLookup.Remove(key);
            }
        }

        public new void Add(StringTable table)
        {
            table.root = this;
            base.Add(table);
            keyLookup[table.Key] = table;
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

        public StringTable() { }

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
        private Dictionary<string, NameSpaceTable> nsLookup = new Dictionary<string, NameSpaceTable>();
        private readonly byte[] MagicGUID = { 0x0E, 0x14, 0x74, 0x75, 0x67, 0x4A, 0x03, 0xFC, 0x4A, 0x15, 0x90, 0x9D, 0xC3, 0x37, 0x7F, 0x1B };

        MemoryList locresData;
        public LocresVersion Version;
        public bool IsGood { get; set; } = true;

        public LocresFile(string FilePath)
        {
            locresData = new MemoryList(FilePath);
            Load();
        }

        public new void Add(NameSpaceTable ns)
        {
            base.Add(ns);
            nsLookup[ns.Name] = ns;
        }

        public NameSpaceTable this[string Name]
        {
            get => nsLookup[Name];
            set => nsLookup[Name] = value;
        }

        public bool ContainsKey(string Name) => nsLookup.ContainsKey(Name);

        private void Load()
        {
            locresData.Seek(0);
            byte[] fileGUID = locresData.GetBytes(16);

            bool guidsEqual = true;
            for (int i = 0; i < 16; i++)
            {
                if (fileGUID[i] != MagicGUID[i])
                {
                    guidsEqual = false;
                    break;
                }
            }

            Version = guidsEqual ? (LocresVersion)locresData.GetByteValue() : LocresVersion.Legacy;
            if (!guidsEqual) locresData.Seek(0);

            if (Version > LocresVersion.Optimized_CityHash64_ExternID_UTF16)
                throw new Exception("Дана версія locres не підтримується");

            List<string> localizedStrings = null;

            if (Version >= LocresVersion.Compact)
            {
                int localizedStringOffset = (int)locresData.GetInt64Value();
                int currentPos = locresData.GetPosition();

                locresData.Seek(localizedStringOffset);

                int localizedStringCount = locresData.GetIntValue();
                localizedStrings = new List<string>(localizedStringCount);

                for (int i = 0; i < localizedStringCount; i++)
                {
                    localizedStrings.Add(locresData.GetStringUE());
                    if (Version >= LocresVersion.Optimized)
                        locresData.Skip(4); // ref count
                }

                locresData.Seek(currentPos);
            }

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

                        uint externID = Version == LocresVersion.Optimized_CityHash64_ExternID_UTF16 ? locresData.GetUIntValue() : 0;

                        AddString(nsStr, keyStr, value, nsHash, keyHash, valueHash);
                        if (Version == LocresVersion.Optimized_CityHash64_ExternID_UTF16)
                            this[nsStr][keyStr].ExternID = externID;
                    }
                }
            }
        }

        private void ReadTextKey(MemoryList memoryList, LocresVersion locresVersion, out uint StrHash, out string Str)
        {
            StrHash = locresVersion >= LocresVersion.Optimized ? memoryList.GetUIntValue() : 0;
            Str = memoryList.GetStringUE();
        }

        public void AddString(string NameSpace, string key, string value, uint NameSpaceHash = 0, uint keyHash = 0, uint ValueHash = 0, uint ExternID = 0)
        {
            if (!ContainsKey(NameSpace))
                Add(new NameSpaceTable(NameSpace, NameSpaceHash));

            var nsTable = this[NameSpace];

            if (!nsTable.ContainsKey(key))
            {
                var newEntry = new StringTable(key, value, keyHash, ValueHash, ExternID);
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
            if (nsLookup.TryGetValue(NameSpace, out var ns))
            {
                base.Remove(ns);
                nsLookup.Remove(NameSpace);
            }
        }

        public uint Optimized_CityHash64_UTF16Hash(string value)
        {
            var Hash = CityHash.CityHash64(Encoding.Unicode.GetBytes(value));
            return (uint)Hash + ((uint)(Hash >> 32) * 23);
        }

        public uint CalcHash(string Str)
        {
            if (string.IsNullOrEmpty(Str))
                return 0;

            if (Version == LocresVersion.Optimized_CityHash64_UTF16 ||
                Version == LocresVersion.Optimized_CityHash64_ExternID_UTF16)
                return Optimized_CityHash64_UTF16Hash(Str);
            else if (Version >= LocresVersion.Optimized)
                return Str.StrCrc32();
            else
                return 0;
        }

        public uint CalcHashExperimental(string Str)
        {
            if (string.IsNullOrEmpty(Str)) return 0;

            if (Version == LocresVersion.Optimized_CityHash64_UTF16)
                return Optimized_CityHash64_UTF16Hash(Str);
            else
                return Str.StrCrc32();
        }

        public HashTable GetHash(string nameSpace, string key)
        {
            if (!ContainsKey(nameSpace)) return null;
            var ns = this[nameSpace];
            if (!ns.ContainsKey(key)) return null;
            var entry = ns[key];
            return new HashTable(ns.NameHash, entry.KeyHash, entry.ValueHash)
            {
                ExternID = entry.ExternID
            };
        }

        // === IAsset interface methods ===

        public void AddItemsToDataGridView(DataGridView dataGrid)
        {
            dataGrid.DataSource = null;
            dataGrid.Rows.Clear();
            dataGrid.Columns.Clear();

            var dataTable = new System.Data.DataTable();
            dataTable.Columns.Add("ID", typeof(string));
            dataTable.Columns.Add("Text", typeof(string));
            dataTable.Columns.Add("Hash Table", typeof(HashTable));

            foreach (var ns in this)
            {
                foreach (var entry in ns)
                {
                    string name = string.IsNullOrEmpty(ns.Name) ? entry.Key : ns.Name + "::" + entry.Key;
                    dataTable.Rows.Add(
                        name,
                        entry.Value,
                        new HashTable(ns.NameHash, entry.KeyHash, entry.ValueHash) { ExternID = entry.ExternID }
                    );
                }
            }

            dataGrid.DataSource = dataTable;
            dataGrid.Columns["Text"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGrid.Columns["Hash Table"].Visible = false;
        }

        public void LoadFromDataGridView(DataGridView dataGrid)
        {
            Clear();
            nsLookup.Clear();

            foreach (DataGridViewRow row in dataGrid.Rows)
            {
                var idCellValue = row.Cells["ID"].Value?.ToString() ?? "";
                var items = idCellValue.Split(new string[] { "::" }, StringSplitOptions.None);

                var nsStr = items.Length == 2 ? items[0] : "";
                var keyStr = items.Length == 2 ? items[1] : items[0];

                if (row.Cells["Hash Table"].Value is HashTable hashTable)
                {
                    AddString(
                        nsStr,
                        keyStr,
                        row.Cells["Text"].Value?.ToString() ?? "",
                        hashTable.NameHash,
                        hashTable.KeyHash,
                        hashTable.ValueHash,
                        hashTable.ExternID
                    );
                }
            }
        }

        public void SaveFile(string filePath)
        {
            Save();
            locresData.WriteFile(filePath);
        }

        public List<List<string>> ExtractTexts()
        {
            var result = new List<List<string>>();
            foreach (var ns in this)
            {
                foreach (var entry in ns)
                {
                    result.Add(new List<string>() { string.IsNullOrEmpty(ns.Name) ? entry.Key : ns.Name + "::" + entry.Key, entry.Value });
                }
            }
            return result;
        }

        public void ImportTexts(List<List<string>> strings)
        {
            int i = 0;
            foreach (var ns in this)
            {
                foreach (var entry in ns)
                {
                    entry.Value = strings[i++][1];
                }
            }
        }

        // === Saving ===
        private void Save()
        {
            locresData.Clear();

            if (Version == LocresVersion.Legacy)
            {
                BuildLegacyFile();
                return;
            }

            locresData.SetBytes(MagicGUID);
            locresData.SetByteValue((byte)Version);

            long localizedStringOffsetPos = locresData.GetPosition();
            locresData.SetInt64Value(0); // резерв для offset

            if (Version >= LocresVersion.Optimized)
                locresData.SetIntValue(this.Sum(ns => ns.Count));

            locresData.SetIntValue(Count);

            var stringTable = new List<StringEntry>();
            var stringIndexLookup = new Dictionary<string, int>();

            foreach (var ns in this)
            {
                if (Version >= LocresVersion.Optimized)
                    locresData.SetUIntValue(ns.NameHash != 0 ? ns.NameHash : CalcHash(ns.Name));

                locresData.SetStringUE(ns.Name);
                locresData.SetIntValue(ns.Count);

                foreach (var entry in ns)
                {
                    if (Version >= LocresVersion.Optimized)
                        locresData.SetUIntValue(entry.KeyHash != 0 ? entry.KeyHash : CalcHash(entry.Key));

                    locresData.SetStringUE(entry.Key);
                    locresData.SetUIntValue(entry.ValueHash != 0 ? entry.ValueHash : entry.Value.StrCrc32());

                    if (!stringIndexLookup.TryGetValue(entry.Value, out int stringIndex))
                    {
                        stringIndex = stringTable.Count;
                        stringTable.Add(new StringEntry { Text = entry.Value, RefCount = 1 });
                        stringIndexLookup[entry.Value] = stringIndex;
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

            int localizedStringOffset = locresData.GetPosition();
            locresData.SetIntValue(stringTable.Count);

            foreach (var s in stringTable)
            {
                locresData.SetStringUE(s.Text);
                if (Version >= LocresVersion.Optimized)
                    locresData.SetIntValue(s.RefCount);
            }

            locresData.Seek((int)localizedStringOffsetPos);
            locresData.SetInt64Value(localizedStringOffset);
        }

        private void BuildLegacyFile()
        {
            locresData.SetIntValue(Count);

            foreach (var ns in this)
            {
                locresData.SetStringUE(ns.Name, true);
                locresData.SetIntValue(ns.Count);
                foreach (var table in ns)
                {
                    locresData.SetStringUE(table.Key, true);
                    locresData.SetUIntValue(table.ValueHash == 0 ? table.Value.StrCrc32() : table.ValueHash);
                    locresData.SetStringUE(table.Value, true);
                }
            }
        }

        private class StringEntry
        {
            public string Text { get; set; }
            public int RefCount { get; set; }
        }
    }
}
