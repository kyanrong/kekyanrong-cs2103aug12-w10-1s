using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace Type
{
    class Parser
    {
        private string commandEscapeToken;
        private string tagEscapeToken;

        public Parser(string commandEscapeToken, string tagEscapeToken)
        {
            this.commandEscapeToken = commandEscapeToken;
            this.tagEscapeToken = tagEscapeToken;
        }

        public bool IsCommand(string text)
        {
            string token = ExtractToken(text);
            return (token.StartsWith(commandEscapeToken));
        }

        public bool IsTag(string text)
        {
            string token = ExtractToken(text);
            return (token.StartsWith(tagEscapeToken));
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

        private static string RemoveToken(string text)
        {
            int firstSpace = text.IndexOf(' ');
            string remaining;
            if (firstSpace < 0)
            {
                remaining = "";
            }
            else
            {
                remaining = text.Substring(firstSpace);
            }
            return remaining;
        }
    }
}
