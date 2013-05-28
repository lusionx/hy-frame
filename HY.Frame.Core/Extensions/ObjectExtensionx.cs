using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HY.Frame.Core
{
    public static class ObjectExtensionx
    {
        public static string ToJson(this object obj)
        {
            System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            return jss.Serialize(obj);
        }
    }
}
