using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Type
{
    class Parser
    {
        private const string COMMAND_ESCAPE = ":";

        public static Boolean IsCommand(string input)
        {
            return input.StartsWith(COMMAND_ESCAPE);
        }
    }
}
