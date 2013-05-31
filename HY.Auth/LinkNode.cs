using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HY.Auth
{
    public class LinkNode
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public List<LinkNode> Children { get; set; }
        public bool Enabled { get; set; }
    }
}
