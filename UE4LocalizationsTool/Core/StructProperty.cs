using Helper.MemoryList;
using System;

namespace AssetParser
{
    public class StructProperty
    {
        private readonly Uexp _uexp;
        private readonly MemoryList _memoryList;

        public StructProperty(MemoryList memoryList, Uexp uexp, bool fromStruct = true, bool fromProperty = false, bool modify = false)
        {
            _memoryList = memoryList;
            _uexp = uexp;

            ParseProperties(fromStruct, fromProperty, modify);
        }

        private void ParseProperties(bool fromStruct, bool fromProperty, bool modify)
        {
            while (_memoryList.GetPosition() < _memoryList.GetSize())
            {
                ulong propertyNameIndex;
                if (fromProperty)
                {
                    propertyNameIndex = _memoryList.GetUInt64Value();

                    if (propertyNameIndex > (uint)_uexp.UassetData.Number_of_Names || propertyNameIndex == 0)
                    {
                        _memoryList.Skip(-4);
                        continue;
                    }
                }
                else
                {
                    propertyNameIndex = (uint)_memoryList.GetIntValue();
                    _memoryList.Skip(4);
                }

                string propertyName = _uexp.UassetData.GetPropertyName((int)propertyNameIndex);
                if (propertyName == "None")
                {
                    break;
                }

                int propertyTypeIndex = _memoryList.GetIntValue();
                string propertyType = _uexp.UassetData.GetPropertyName(propertyTypeIndex);
                _memoryList.Skip(4);

                int thisPosition = _memoryList.GetPosition();
                int propertyLength = _memoryList.GetIntValue();
                _memoryList.Skip(4);

                if (fromStruct)
                {
                    _memoryList.Skip(1);
                }

                try
                {
                    HandleProperty(propertyName, propertyType, propertyLength, modify, thisPosition);
                }
                catch (Exception ex)
                {
                    _uexp.IsGood = false;
                    ConsoleMode.Print($"Bug here: {ex.Message}", ConsoleColor.Red, ConsoleMode.ConsoleModeType.Error);
                    break;
                }
            }
        }

        private void HandleProperty(string propertyName, string propertyType, int propertyLength, bool modify, int thisPosition)
        {
            int startPosition = _memoryList.GetPosition();

            switch (propertyType)
            {
                case "MapProperty":
                    string mapKey = _uexp.UassetData.GetPropertyName(_memoryList.GetIntValue());
                    _memoryList.Skip(4);
                    string mapValue = _uexp.UassetData.GetPropertyName(_memoryList.GetIntValue());
                    _memoryList.Skip(4);
                    _memoryList.Skip(4);

                    int mapCount = _memoryList.GetIntValue();

                    if (mapCount > 0)
                    {
                        MemoryList mapData = new MemoryList(_memoryList.GetBytes(propertyLength));
                        for (int i = 0; i < mapCount; i++)
                        {
                            ParseSimpleProperty(mapData, mapKey, propertyName, modify);
                            ParseSimpleProperty(mapData, mapValue, propertyName, modify);
                        }

                        if (modify)
                        {
                            _memoryList.ReplaceBytes(propertyLength, mapData.ToArray(), false, startPosition);
                            _memoryList.SetIntValue(mapData.GetSize(), false, thisPosition);
                        }
                    }
                    _memoryList.Seek(startPosition + propertyLength);
                    break;
                case "ArrayProperty":
                    string arrayType = _uexp.UassetData.GetPropertyName(_memoryList.GetIntValue());
                    _memoryList.Skip(4);
                    int arrayCount = _memoryList.GetIntValue();

                    if (arrayCount > 0)
                    {
                        MemoryList arrayData = new MemoryList(_memoryList.GetBytes(propertyLength));

                        if (arrayType == "StructProperty")
                        {
                            string arrayStructName = _uexp.UassetData.GetPropertyName(arrayData.GetIntValue());
                            arrayData.Skip(12);
                            int structLength = arrayData.GetIntValue();
                            arrayData.Skip(4);
                            string arrayStructType = _uexp.UassetData.GetPropertyName(arrayData.GetIntValue());
                            arrayData.Skip(20);

                            for (int i = 0; i < arrayCount; i++)
                            {
                                new StructProperty(arrayData, _uexp, true, false, modify);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < arrayCount; i++)
                            {
                                ParseSimpleProperty(arrayData, arrayType, propertyName, modify);
                            }
                        }

                        if (modify)
                        {
                            _memoryList.ReplaceBytes(propertyLength, arrayData.ToArray(), false, startPosition);
                            _memoryList.SetIntValue(arrayData.GetSize(), false, thisPosition);
                        }
                    }
                    _memoryList.Seek(startPosition + propertyLength);
                    break;
                case "StructProperty":
                    string structType = _uexp.UassetData.GetPropertyName(_memoryList.GetIntValue());
                    _memoryList.Skip(4);
                    _memoryList.Skip(16);

                    if (propertyLength > 4)
                    {
                        MemoryList structData = new MemoryList(_memoryList.GetBytes(propertyLength));
                        new StructProperty(structData, _uexp, true, false, modify);

                        if (modify)
                        {
                            _memoryList.ReplaceBytes(propertyLength, structData.ToArray(), false, startPosition);
                            _memoryList.SetIntValue(structData.GetSize(), false, thisPosition);
                        }
                    }
                    _memoryList.Seek(startPosition + propertyLength);
                    break;
                case "BoolProperty":
                    _memoryList.Skip(1);
                    _memoryList.Skip(propertyLength);
                    break;
                case "EnumProperty":
                    _memoryList.Skip(8);
                    _memoryList.Skip(propertyLength);
                    break;
                case "TextProperty":
                    MemoryList textData = new MemoryList(_memoryList.GetBytes(propertyLength));

                    if (_uexp.UassetData.EngineVersion < UEVersions.VER_UE4_FTEXT_HISTORY)
                    {
                        new ReadStringProperty(textData, _uexp, propertyName + "_0", modify);
                        if (_uexp.UassetData.EngineVersion >= UEVersions.VER_UE4_ADDED_NAMESPACE_AND_KEY_DATA_TO_FTEXT)
                        {
                            new ReadStringProperty(textData, _uexp, propertyName + "_1", modify);
                            new ReadStringProperty(textData, _uexp, propertyName + "_2", modify);
                        }
                        else
                        {
                            new ReadStringProperty(textData, _uexp, propertyName + "_2", modify);
                        }
                    }
                    else
                    {
                        new TextHistory(textData, _uexp, propertyName, modify);
                    }

                    if (modify)
                    {
                        _memoryList.ReplaceBytes(propertyLength, textData.ToArray(), false, startPosition);
                        _memoryList.SetIntValue(textData.GetSize(), false, thisPosition);
                    }
                    _memoryList.Seek(startPosition + propertyLength);
                    break;
                case "ByteProperty":
                    _memoryList.Skip(propertyLength);
                    break;
                case "SetProperty":
                    string setKey = _uexp.UassetData.GetPropertyName(_memoryList.GetIntValue());
                    _memoryList.Skip(4);
                    int setCount = _memoryList.GetIntValue();

                    if (setCount > 0)
                    {
                        MemoryList setData = new MemoryList(_memoryList.GetBytes(propertyLength));
                        for (int i = 0; i < setCount; i++)
                        {
                            ParseSimpleProperty(setData, setKey, propertyName, modify);
                        }

                        if (modify)
                        {
                            _memoryList.ReplaceBytes(propertyLength, setData.ToArray(), false, startPosition);
                            _memoryList.SetIntValue(setData.GetSize(), false, thisPosition);
                        }
                    }
                    _memoryList.Seek(startPosition + propertyLength);
                    break;
                default:
                    _memoryList.Skip(propertyLength);
                    break;
            }
        }

        private void ParseSimpleProperty(MemoryList memoryList, string propertyType, string propertyName, bool modify)
        {
            switch (propertyType)
            {
                case "Int8Property": memoryList.Skip(1); break;
                case "Int16Property": memoryList.Skip(2); break;
                case "IntProperty": memoryList.Skip(4); break;
                case "Int64Property": memoryList.Skip(8); break;
                case "UInt8Property": memoryList.Skip(1); break;
                case "UInt16Property": memoryList.Skip(2); break;
                case "UInt32Property": memoryList.Skip(4); break;
                case "UInt64Property": memoryList.Skip(8); break;
                case "FloatProperty": memoryList.Skip(4); break;
                case "DoubleProperty": memoryList.Skip(8); break;
                case "ObjectProperty": memoryList.Skip(4); break;
                case "SoftObjectProperty": memoryList.Skip(12); break;
                case "NameProperty": new FName(memoryList, _uexp, propertyName, modify); break;
                case "MulticastSparseDelegateProperty": memoryList.Skip(16); break;
                case "MulticastDelegateProperty": memoryList.Skip(16); break;
                case "LazyObjectProperty": memoryList.Skip(16); break;
                case "InterfaceProperty": memoryList.Skip(4); break;
                case "EnumProperty": memoryList.Skip(8); break;
                case "AssetObjectProperty": new ReadStringProperty(memoryList, _uexp, propertyName, modify); break;
                case "BoolProperty": memoryList.Skip(1); break;
                case "ByteProperty": memoryList.Skip(1); break;
                case "StrProperty": new ReadStringProperty(memoryList, _uexp, propertyName, modify); break;
                case "TextProperty": new TextHistory(memoryList, _uexp, propertyName, modify); break;
                default:
                    throw new NotSupportedException($"Unknown simple property type: {propertyType}");
            }
        }
    }
}