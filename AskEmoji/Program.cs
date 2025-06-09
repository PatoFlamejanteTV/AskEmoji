using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;
using AskEmoji.Forms;

namespace AskEmoji
{
    static class Program
    {
        private const string ContextMenuKeyPath = @"*\shell\Ask Emoji";
        private const string CommandKeyPath = @"*\shell\Ask Emoji\command";

        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (args.Length == 0)
            {
                if (!IsContextMenuInstalled())
                {
                    try
                    {
                        InstallContextMenu();
                        MessageBox.Show("Context menu thing installed!", "Ask Emoji", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Err while installing context menu:\n" + ex.Message, "Ask Emoji", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Already installed ;)", "Ask Emoji", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                return;
            }

            string filePath = args[0];
            string fileName = Path.GetFileName(filePath).ToLowerInvariant();

            if (fileName.Contains("good")) //if (fileName.Contains("good_app"))
            {
                Application.Run(new Good());
            }
            else if (fileName.Contains("bad")) //else if (fileName.Contains("bad_app"))
            {
                Application.Run(new Bad());
            }
            else
            {
                MessageBox.Show("i dont actually have an opinion on this", "Ask Emoji", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private static bool IsContextMenuInstalled()
        {
            using (var key = Registry.ClassesRoot.OpenSubKey(ContextMenuKeyPath))
            {
                return key != null;
            }
        }

        private static void InstallContextMenu()
        {
            string exePath = System.Reflection.Assembly.GetExecutingAssembly().Location;

            using (var key = Registry.ClassesRoot.CreateSubKey(ContextMenuKeyPath))
            {
                if (key == null)
                    throw new Exception("Error while creating context menu key.");
                key.SetValue(null, "Ask Emoji");
            }

            using (var commandKey = Registry.ClassesRoot.CreateSubKey(CommandKeyPath))
            {
                if (commandKey == null)
                    throw new Exception("Impossible to create context menu key.");
                commandKey.SetValue(null, $"\"{exePath}\" \"%1\"");
            }
        }
    }
}
