using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace HY.Frame.Core
{
   public static class WebExtensions
    {
       private static readonly string _appPath;

       public static string AppPath(this UserControl uc)
       {
           var path = uc.Request.ApplicationPath;
           if (path.Length == 1)
           {
               return string.Empty;
           }
           return path;
       }

       public static string AppPath(this Page uc)
       {
           var path = uc.Request.ApplicationPath;
           if (path.Length == 1)
           {
               return string.Empty;
           }
           return path;
       }
    }
}
