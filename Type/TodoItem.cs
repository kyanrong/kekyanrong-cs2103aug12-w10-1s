using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace Type
{
    class TodoItem
    {
        private int type { get; set; }
        private string text { get; set; }
        private StringCollection tags { get; set; }
        private DateTime start { get; set; }
        private DateTime end { get; set; }
    }
}
