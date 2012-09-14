using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace Type
{
    class Parser
    {
        private StringCollection commands;
        private StringCollection hashTags;
        private StringCollection tasks;
        private string escapeToken;

        public Parser(string escapeToken, string[] acceptedCommands)
        {
            this.escapeToken = escapeToken;
            commands = new StringCollection();

            InitCommandsCollection(acceptedCommands);
        }

        private void InitCommandsCollection(string[] acceptedCommands)
        {
            commands.AddRange(acceptedCommands);
        }

        public bool IsCommand(string text)
        {
            string token = ExtractToken(text);
            return (token.StartsWith(escapeToken));
        }

        private static string ExtractToken(string text)
        {
            int firstSpace = text.IndexOf(' ');
            string token;
            if (firstSpace < 0)
            {
                token = text;
            }
            else
            {
                token = text.Substring(0, firstSpace);
            }
            return token;
        }
    }
}
