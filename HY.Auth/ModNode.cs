using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HY.Auth
{
    public class ModNode
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string Action { get; set; } 
        public bool Enabled { get; set; }
        public List<string> Roles { get; set; }
        public string Desc { get; set; }
    }
}
