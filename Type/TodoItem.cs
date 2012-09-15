using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace Type
{
    class TodoItem
    {
        public string text { get; set; }
        public StringCollection tags { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
    }
}
