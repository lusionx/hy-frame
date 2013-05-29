using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HY.Frame.Core
{
    public interface IJSON
    {
        string ToJSON(object obj);
    }
}
