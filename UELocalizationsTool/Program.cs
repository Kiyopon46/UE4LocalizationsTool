using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using UELocalizationsTool.Properties;
namespace UELocalizationsTool
{
    internal static class Program
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool AttachConsole(int dwProcessId);
        private const int ATTACH_PARENT_PROCESS = -1;

        public static string commandlines =
           $"{AppDomain.CurrentDomain.FriendlyName}  export    <(Locres/Uasset/Umap) FilePath>  <Options>\n" +
           $"{AppDomain.CurrentDomain.FriendlyName}  import    <(txt) FilePath>  <Options>\n" +
           $"{AppDomain.CurrentDomain.FriendlyName} -import    <(txt) FilePath>  <Options>\n" +
           $"{AppDomain.CurrentDomain.FriendlyName}  exportall <Folder> <TxtFile> <Options>\n" +
           $"{AppDomain.CurrentDomain.FriendlyName}  importall <Folder> <TxtFile>  <Options>\n" +
           $"{AppDomain.CurrentDomain.FriendlyName} -importall  <Folder> <TxtFile>  <Options>\n\n" +
           "- for import without rename file be careful with this command.\n\n" +

           "Options:\n" +
           "To use last filter you applied before in GUI, add (-f \\ -filter) after command line\n" +
           "filter will apply only in name table " +
           "\n(Remember to apply the same filter when importing)\n\n" +

           "To export file without including the names use (-nn \\ -NoName)" +
           "\n(Remember to use this command when importing)\n\n" +

           "To use method 2 (-m2 \\ -method2)" +
           "\n(Remember to use this command when importing)\n\n" +

           "Examples:\n" +
           $"{AppDomain.CurrentDomain.FriendlyName} export Actions.uasset\n" +
           $"{AppDomain.CurrentDomain.FriendlyName} import Actions.uasset.txt\n" +
           $"{AppDomain.CurrentDomain.FriendlyName} exportall Actions text.txt\n" +
           $"{AppDomain.CurrentDomain.FriendlyName} importall Actions text.txt\n";

        public static Args GetArgs(int Index, string[] args)
        {
            Args args1 = new Args();

            for (int n = Index; n < args.Length; n++)
            {
                switch (args[n].ToLower())
                {
                    case "-f":
                    case "-filter":
                        args1 |= Args.filter;
                        break;
                    case "-nn":
                    case "-noname":
                        args1 |= Args.noname;
                        break;
                    case "-m2":
                    case "-method2":
                        args1 |= Args.method2;
                        break;
                    case "-c":
                    case "-csv":
                        args1 |= Args.CSV;
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Invalid command: " + args[n]);
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                }
            }
            return args1;
        }

        public static void CheckArges(int Index, string[] args)
        {
            for (int n = 0; n < Index; n++)
            {
                switch (args[n].ToLower())
                {
                    case "-f":
                    case "-filter":
                    case "-nn":
                    case "-noname":
                    case "-method2":
                    case "-m2":
                    case "-c":
                    case "-csv":
                        throw new Exception("Invalid number of arguments.\n\n" + commandlines);
                }
            }
        }

        [STAThread]
        static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }

        static async Task MainAsync(string[] args)
        {
            if (args.Length > 0)
            {
                if (args.Length == 1 && (args[0].EndsWith(".uasset") || args[0].EndsWith(".umap") || args[0].EndsWith(".locres")))
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);

                    var frmMain = new FrmMain();

                    frmMain.Shown += async (s, e) =>
                    {
                        frmMain.Shown -= null;
                        try
                        {
                            await frmMain.LoadFile(args[0]);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(Resources.Msg_ErrorForReadFile + ex.Message, Resources.Title_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    };

                    Application.Run(frmMain);
                    return;
                }

                AttachConsole(ATTACH_PARENT_PROCESS);
                Console.WriteLine("");

                if (args.Length < 2)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid number of arguments.\n\n" + commandlines);
                    Console.ForegroundColor = ConsoleColor.White;
                    return;
                }

                try
                {
                    var commands = new Commands();

                    if (args[0].ToLower() == "importall" || args[0].ToLower() == "-importall" || args[0].ToLower() == "exportall")
                    {
                        if (args.Length < 3)
                            throw new Exception("Invalid number of arguments.\n\n" + commandlines);

                        CheckArges(3, args);
                        await commands.RunAsync(args[0], args[1] + "*" + args[2], GetArgs(3, args));
                    }
                    else
                    {
                        CheckArges(2, args);
                        await commands.RunAsync(args[0], args[1], GetArgs(2, args));
                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n" + ex.Message);
                    Console.ForegroundColor = ConsoleColor.White;
                }

                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmMain());
        }
    }
}