﻿using AssetParser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UELocalizationsTool.Core.locres;
using UELocalizationsTool.Helper;

namespace UELocalizationsTool
{
    [Flags]
    public enum Args
    {
        filter = 1 << 0,
        noname = 1 << 1,
        method2 = 1 << 2,
        CSV = 1 << 3,
    }

    public class Commands
    {
        private List<List<string>> Strings;
        private string TextFileExtension = ".txt";
        public Args Flags;

        private bool UseMatching = false;
        private bool RegularExpression = false;
        private bool ReverseMode = false;
        private List<string> ArrayValues;

        public Commands()
        {
            Strings = new List<List<string>>();
        }

        public async Task RunAsync(string Options, string SourcePath, Args args)
        {
            Flags = args;
            if (Flags.HasFlag(Args.filter))
            {
                GetFilterValues();
            }

            if (Flags.HasFlag(Args.CSV))
            {
                TextFileExtension = ".csv";
            }
            string[] Paths;
            string ConsoleText;
            string[] rows;
            switch (Options.ToLower())
            {
                case "export":
                    Console.ForegroundColor = ConsoleColor.Blue;
                    ConsoleText = $"Exporting... '{Path.GetFileName(SourcePath)}' ";
                    Console.Write(ConsoleText);
                    Console.ForegroundColor = ConsoleColor.White;

                    Strings = Export(SourcePath);

                    if (Flags.HasFlag(Args.filter))
                    {
                        Strings = ApplyFilter(Strings);
                    }

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Done\n");
                    Console.ForegroundColor = ConsoleColor.White;

                    await SaveTextFile(SourcePath + TextFileExtension);
                    break;

                case "exportall":
                    Paths = SourcePath.Split(new char[] { '*' }, 2);
                    await ExportFolder(Paths[0]);
                    await SaveTextFile(Paths[1]);
                    break;

                case "import":
                case "-import":
                    Console.ForegroundColor = ConsoleColor.Blue;
                    ConsoleText = $"Importing... '{Path.GetFileName(SourcePath)}' ";
                    Console.Write(ConsoleText);
                    Console.ForegroundColor = ConsoleColor.White;

                    if (!SourcePath.EndsWith(TextFileExtension, StringComparison.OrdinalIgnoreCase))
                    {
                        throw new Exception("Invalid text file type: " + Path.GetFileName(SourcePath));
                    }

                    if (Flags.HasFlag(Args.CSV))
                    {
                        rows = await CSVFile.Instance.Load(SourcePath, Flags.HasFlag(Args.noname));
                    }
                    else
                    {
                        rows = File.ReadAllLines(SourcePath);
                    }

                    Import(Path.ChangeExtension(SourcePath, null), rows, Options.ToLower());
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Done\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    break;

                case "importall":
                case "-importall":
                    Paths = SourcePath.Split(new char[] { '*' }, 2);

                    if (!Paths[1].EndsWith(TextFileExtension, StringComparison.OrdinalIgnoreCase))
                    {
                        throw new Exception("Invalid text file type: " + Path.GetFileName(SourcePath));
                    }

                    if (Flags.HasFlag(Args.CSV))
                    {
                        rows = await CSVFile.Instance.Load(Paths[1], Flags.HasFlag(Args.noname));
                    }
                    else
                    {
                        rows = File.ReadAllLines(Paths[1]);
                    }

                    await ImportFolder(Paths[0], rows, Options.ToLower());
                    break;
                default:
                    throw new Exception("Invalid number of arguments.\n" + Program.commandlines);
            }
        }

        private async Task SaveTextFile(string FilePath)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            string ConsoleText = "Saving text file... ";
            Console.Write(ConsoleText);
            Console.ForegroundColor = ConsoleColor.White;


            if (Flags.HasFlag(Args.CSV))
            {
                await CSVFile.Instance.Save(Strings, FilePath, Flags.HasFlag(Args.noname));
                goto End;
            }

            string[] stringsArray = new string[Strings.Count];
            int i = 0;

            foreach (var item in Strings)
            {
                if (item[0] == "[~PATHFile~]")
                {
                    stringsArray[i] = item[1];
                }
                else
                {
                    if (!Flags.HasFlag(Args.noname))
                    {
                        stringsArray[i] = item[0] + "=" + item[1];
                    }
                    else
                    {
                        stringsArray[i] = item[1];
                    }
                }
                i++;
            }
            File.WriteAllLines(FilePath, stringsArray);
        End:
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Done\n");
            Console.ForegroundColor = ConsoleColor.White;
        }


        private List<List<string>> Export(string FilePath)
        {
            if (!File.Exists(FilePath))
            {
                throw new Exception("File not existed: " + FilePath);
            }


            if (FilePath.EndsWith(".locres", StringComparison.OrdinalIgnoreCase))
            {
                IAsset locres = new LocresFile(FilePath);
                return locres.ExtractTexts();
            }
            else if (FilePath.EndsWith(".uasset", StringComparison.OrdinalIgnoreCase) || FilePath.EndsWith(".umap", StringComparison.OrdinalIgnoreCase))
            {
                IUasset Uasset = Uexp.GetUasset(FilePath);

                if (Flags.HasFlag(Args.method2))
                {
                    Uasset.UseMethod2 = true;
                }

                Uexp uexp = new Uexp(Uasset);
                return uexp.Strings;
            }
            else
            {
                throw new Exception("Invalid language file type: " + Path.GetFileName(FilePath));
            }
        }


        private async Task ExportFolder(string FolderPath)
        {
            if (!Directory.Exists(FolderPath))
            {
                throw new Exception("Directory not existed: " + FolderPath);
            }
            Console.ForegroundColor = ConsoleColor.Blue;
            string ConsoleText = "Scaning for files...";
            Console.Write(ConsoleText);
            Console.ForegroundColor = ConsoleColor.White;

            string[] LanguageFiles = await Task.Run(() =>
            {
                return Directory.GetFiles(FolderPath, "*.*", SearchOption.AllDirectories)
                                .Where(x => x.EndsWith(".locres", StringComparison.OrdinalIgnoreCase) ||
                                            x.EndsWith(".uasset", StringComparison.OrdinalIgnoreCase) ||
                                            x.EndsWith(".umap", StringComparison.OrdinalIgnoreCase))
                                .ToArray();
            });

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Done\n");
            Console.ForegroundColor = ConsoleColor.White;

            if (LanguageFiles.Length == 0)
            {
                throw new Exception($"This directory '{FolderPath}' not contine any language files.");
            }

            await Task.Run(() =>
            {
                for (int i = 0; i < LanguageFiles.Length; i++)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    string currentConsoleText = $"[{i + 1}:{LanguageFiles.Length}] Exporting... '{Path.GetFileName(LanguageFiles[i])}' ";
                    Console.Write(currentConsoleText);
                    Console.ForegroundColor = ConsoleColor.White;

                    try
                    {
                        List<List<string>> Souce = Export(LanguageFiles[i]);

                        if (Flags.HasFlag(Args.filter))
                        {
                            Souce = ApplyFilter(Souce);
                        }

                        Strings.Add(new List<string>() { "[~PATHFile~]", "[PATH]" + Souce.Count + "*" + Path.GetFullPath(LanguageFiles[i]).Replace(Path.GetFullPath(FolderPath), "") + "[PATH]", "[~PATHFile~]" });
                        Strings.AddRange(Souce);
                    }
                    catch (Exception EX)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("Fail\n");
                        Console.ForegroundColor = ConsoleColor.White;

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Can't parse it, the tool will skip this file.\n" + EX.Message);
                        Console.ForegroundColor = ConsoleColor.White;
                        continue;
                    }
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Done\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            });
        }


        void EditList(List<List<string>> Strings, string[] StringValues)
        {

            if (Flags.HasFlag(Args.filter))
            {
                ApplyFilter(Strings, StringValues);
                return;
            }

            if (StringValues.Length < Strings.Count)
            {
                throw new Exception("Text file is too short");
            }
            for (int i = 0; i < StringValues.Length; i++)
            {
                try
                {
                    if (!Flags.HasFlag(Args.noname))
                    {
                        Strings[i][1] = StringValues[i].Split(new char[] { '=' }, 2)[1];
                    }
                    else
                    {
                        Strings[i][1] = StringValues[i];
                    }
                }
                catch
                {
                    throw new Exception("Can't parse this line from text file: " + StringValues[i]);
                }
            }
        }



        private void Import(string FilePath, string[] Values, string Option)
        {

            if (!File.Exists(FilePath))
            {
                throw new Exception("File not existed: " + FilePath);
            }

            if (FilePath.EndsWith(".locres", StringComparison.OrdinalIgnoreCase))
            {
                var locres = new LocresFile(FilePath);
                var strings = locres.ExtractTexts();
                EditList(strings, Values);
                locres.ImportTexts(strings);

                if (Option == "-import")
                {
                    locres.SaveFile(FilePath);
                    return;
                }

                FilePath = Path.ChangeExtension(FilePath, null) + "_NEW.locres";
                locres.SaveFile(FilePath);
            }
            else if (FilePath.EndsWith(".uasset", StringComparison.OrdinalIgnoreCase) || FilePath.EndsWith(".umap", StringComparison.OrdinalIgnoreCase))
            {
                IUasset Uasset = Uexp.GetUasset(FilePath);
                if (Flags.HasFlag(Args.method2))
                {
                    Uasset.UseMethod2 = true;
                }

                Uexp uexp = new Uexp(Uasset);
                EditList(uexp.Strings, Values);

                if (Option == "-import")
                {
                    uexp.SaveFile(FilePath);
                    return;
                }

                if (FilePath.EndsWith(".uasset", StringComparison.OrdinalIgnoreCase))
                {
                    FilePath = Path.ChangeExtension(FilePath, null) + "_NEW.uasset";
                }
                else if (FilePath.EndsWith(".umap", StringComparison.OrdinalIgnoreCase))
                {
                    FilePath = Path.ChangeExtension(FilePath, null) + "_NEW.umap";
                }

                uexp.SaveFile(FilePath);
            }
            else
            {
                throw new Exception("Invalid language file type: " + Path.GetFileName(FilePath));
            }
        }

        private async Task ImportFolder(string FolderPath, string[] Values, string Option)
        {
            await Task.Run(() =>
            {
                if (!Directory.Exists(FolderPath))
                {
                    throw new Exception("Directory not existed: " + FolderPath);
                }

                int[] Indexs = Values.Select((Value, Index) => (Value.Trim().StartsWith("[PATH]") && Value.Trim().EndsWith("[PATH]")) ? Index : -1).Where(index => index != -1).ToArray();

                if (Indexs.Length == 0)
                {
                    throw new Exception("Source text file is corrupted or not contain text or you modified language files path ([PATH]....[PATH]).");
                }

                for (int PathIndex = 0; PathIndex < Indexs.Length; PathIndex++)
                {
                    string[] RecordInfo = Values[Indexs[PathIndex]].Replace("[PATH]", "").Trim().Split(new char[] { '*' }, 2);
                    int ArraySize = int.Parse(RecordInfo[0]);
                    string FilePath = RecordInfo[1];

                    if (string.IsNullOrEmpty(FilePath))
                    {
                        Console.WriteLine("Can't get path from" + Indexs[PathIndex] + "line");
                        continue;
                    }
                    FilePath = FolderPath + @"\" + FilePath;
                    FilePath = FilePath.Replace(@"\\", @"\");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    string ConsoleText = $"[{PathIndex + 1}:{Indexs.Length}] Importing... '{Path.GetFileName(FilePath)}' ";
                    Console.Write(ConsoleText);
                    Console.ForegroundColor = ConsoleColor.White;
                    string[] StringArrayValues = new string[ArraySize];
                    Array.Copy(Values, Indexs[PathIndex] + 1, StringArrayValues, 0, ArraySize);

                    try
                    {
                        if (Option == "importall")
                        {
                            Import(FilePath, StringArrayValues, "import");
                        }
                        else
                        {
                            Import(FilePath, StringArrayValues, "-import");
                        }
                    }
                    catch (Exception EX)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("Fail\n");
                        Console.ForegroundColor = ConsoleColor.White;

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Can't parse it, the tool will skip this file.\n" + EX.Message);
                        Console.ForegroundColor = ConsoleColor.White;
                        continue;
                    }
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Done\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            });
        }

        private void GetFilterValues()
        {
            if (!File.Exists("FilterValues.txt"))
            {
                throw new Exception("Can't find 'FilterValues.txt' file, open the GUI and create new one from (Tool>Filter)");
            }

            try
            {
                List<string> FV = new List<string>();
                FV.AddRange(File.ReadAllLines("FilterValues.txt"));
                string[] Controls = FV[0].Split(new char[] { '|' });
                bool.TryParse(Controls[0], out UseMatching);
                bool.TryParse(Controls[1], out RegularExpression);
                bool.TryParse(Controls[2], out ReverseMode);
                FV.RemoveAt(0);
                ArrayValues = FV;
            }
            catch (Exception ex)
            {
                throw new Exception("Can't parse 'FilterValues.txt', open the GUI and create new one from (Tool>Filter)\n" + ex.Message);
            }
        }


        private List<List<string>> ApplyFilter(List<List<string>> Strings)
        {
            List<List<string>> FV = new List<List<string>>();
            for (int x = 0; x < Strings.Count; x++)
            {
                bool CanAdd = false;

                ArrayValues.ForEach(Value =>
                {
                    if (UseMatching)
                    {
                        if (RegularExpression)
                        {
                            try
                            {
                                if (Regex.IsMatch(Strings[x][0], Value))
                                {
                                    CanAdd = true;
                                }
                            }
                            catch { }
                        }
                        else
                        {
                            if (Strings[x][0] == Value)
                            {
                                CanAdd = true;
                            }
                        }
                    }
                    else
                    {
                        if (RegularExpression)
                        {
                            try
                            {
                                if (Regex.IsMatch(Strings[x][0], Value, RegexOptions.IgnoreCase))
                                {
                                    CanAdd = true;
                                }
                            }
                            catch { }
                        }
                        else
                        {
                            if (Strings[x][0].IndexOf(Value, StringComparison.OrdinalIgnoreCase) >= 0)
                            {
                                CanAdd = true;
                            }
                        }
                    }
                });

                if (CanAdd)
                {
                    if (!ReverseMode)
                        FV.Add(Strings[x]);
                }
                else if (ReverseMode)
                {
                    FV.Add(Strings[x]);
                }
            }
            return FV;
        }


        private void ApplyFilter(List<List<string>> Strings, string[] Array)
        {
            int i = 0;
            for (int x = 0; x < Strings.Count; x++)
            {
                bool CanAdd = false;


                ArrayValues.ForEach(Value =>
                {

                    if (UseMatching)
                    {
                        if (RegularExpression)
                        {
                            try
                            {
                                if (Regex.IsMatch(Strings[x][0], Value))
                                {
                                    CanAdd = true;
                                }
                            }
                            catch { }
                        }
                        else
                        {
                            if (Strings[x][0] == Value)
                            {
                                CanAdd = true;
                            }
                        }
                    }
                    else
                    {
                        if (RegularExpression)
                        {
                            try
                            {
                                if (Regex.IsMatch(Strings[x][0], Value, RegexOptions.IgnoreCase))
                                {
                                    CanAdd = true;
                                }
                            }
                            catch { }
                        }
                        else
                        {
                            if (Strings[x][0].IndexOf(Value, StringComparison.OrdinalIgnoreCase) >= 0)
                            {
                                CanAdd = true;
                            }
                        }
                    }
                });

                if (CanAdd)
                {
                    if (!ReverseMode)
                    {
                        try
                        {
                            if (!Flags.HasFlag(Args.noname))
                            {
                                Strings[x][1] = Array[i].Split(new char[] { '=' }, 2)[1];
                                i++;
                            }
                            else
                            {
                                Strings[x][1] = Array[i];
                                i++;
                            }
                        }
                        catch
                        {
                            throw new Exception("Can't parse this line from text file: " + Array[i]);
                        }
                    }
                }
                else if (ReverseMode)
                {
                    try
                    {
                        if (!Flags.HasFlag(Args.noname))
                        {
                            Strings[x][1] = Array[i].Split(new char[] { '=' }, 2)[1];
                            i++;
                        }
                        else
                        {
                            Strings[x][1] = Array[i];
                            i++;
                        }
                    }
                    catch
                    {
                        throw new Exception("Can't parse this line from text file: " + Array[i]);
                    }
                }
            }
        }
    }
}