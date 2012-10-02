using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace Type
{
    class Task
    {
        private string rawText;

        private bool done;
        private bool archive;

        private DateTime start;
        private DateTime end;

        private StringCollection tags;

        private List<Tuple<string, int>> tokens;

        public string RawText
        {
            get
            {
                return rawText;
            }
        }

        public bool Done
        {
            get
            {
                return done;
            }

            set
            {
                done = value;
            }
        }

        public bool Archive
        {
            get
            {
                return archive;
            }

            set
            {
                archive = value;
            }
        }

        public DateTime Start
        {
            get
            {
                return start;
            }
        }

        public DateTime End
        {
            get
            {
                return end;
            }
        }

        public StringCollection Tags
        {
            get
            {
                return tags;
            }
        }

        public List<Tuple<string, int>> Tokens
        {
            get
            {
                return tokens;
            }
        }

        public Task(string rawText)
        {
            // @michael TODO
            
        }
    }
}
