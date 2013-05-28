using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HY.Frame.Core.Toolkit
{
    public abstract class Query
    {
        public abstract string SearchText { get; set; }

        public override string ToString()
        {
            return SearchText;
        }
    }
}
