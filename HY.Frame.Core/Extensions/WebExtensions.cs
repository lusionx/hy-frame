using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace HY.Frame.Core
{
    /// <summary>
    /// 
    /// </summary>
   public static class WebExtensions
    {
       /// <summary>
       /// 
       /// </summary>
       /// <param name="uc"></param>
       /// <returns></returns>
       public static string AppPath(this UserControl uc)
       {
           var path = uc.Request.ApplicationPath;
           if (path.Length == 1)
           {
               return string.Empty;
           }
           return path;
       }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="uc"></param>
       /// <returns></returns>
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
