//// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using System;
using System.Xml;
using System.IO;

class Program
{
    static void Main()
    {
        string csvPath = "translations_en.csv"; // スプレッドシートをCSVで保存
        string resxPath = "Resources.en.resx";

        using var reader = new StreamReader(csvPath);
        using var writer = XmlWriter.Create(resxPath, new XmlWriterSettings { Indent = true });

        writer.WriteStartElement("root");
        writer.WriteStartElement("resheader");
        writer.WriteAttributeString("name", "resmimetype");
        writer.WriteElementString("value", "text/microsoft-resx");
        writer.WriteEndElement();

        writer.WriteStartElement("resheader");
        writer.WriteAttributeString("name", "version");
        writer.WriteElementString("value", "2.0");
        writer.WriteEndElement();

        writer.WriteStartElement("resheader");
        writer.WriteAttributeString("name", "reader");
        writer.WriteElementString("value", "System.Resources.ResXResourceReader, System.Windows.Forms");
        writer.WriteEndElement();

        writer.WriteStartElement("resheader");
        writer.WriteAttributeString("name", "writer");
        writer.WriteElementString("value", "System.Resources.ResXResourceWriter, System.Windows.Forms");
        writer.WriteEndElement();

        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();
            if (string.IsNullOrWhiteSpace(line)) continue;
            var parts = line.Split(',', 2); // カンマ区切りでName,Value

            writer.WriteStartElement("data");
            writer.WriteAttributeString("name", parts[0]);
            //writer.WriteAttributeString("xml:space", "preserve");
            writer.WriteAttributeString("xml", "space", null, "preserve"); // ← 修正ポイント！
            writer.WriteElementString("value", parts[1]);
            writer.WriteEndElement();
        }

        writer.WriteEndElement(); // root
        writer.Close();

        Console.WriteLine("Resources.ja.resx を生成しました！");
    }
}
