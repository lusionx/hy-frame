using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HY.Frame.Core
{

    /// <summary>
    /// 配置log4(~/App_Data/log4net.xml)，并提供全局入口
    /// </summary>
    public class Log
    {
        private static bool isInit = false; 

        //private static log4net.ILog _logger;

        public static log4net.ILog Get(Type classtype)
        {
            if (!isInit)
            {
                var context = System.Web.HttpContext.Current;
                var path = context.Server.MapPath("~/App_Data/log4net.xml");
                log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(path));
                isInit = true;
                //var a = new log4net.Appender.RollingFileAppender();
               
            }

            return log4net.LogManager.GetLogger(classtype);
        }
    }
}
