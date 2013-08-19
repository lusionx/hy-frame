using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace HY.Frame.Web
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            //var app = sender as 
            //log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(""));
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

            var app = sender as System.Web.HttpApplication;
            var key = "Global.log4net.Configurator";
            if (app.Context.Cache[key] == null)
            {
                var path = app.Server.MapPath("~/App_Data/Log4net.xml");
                log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(path));
                app.Context.Cache.Insert(key, new object(), null,
                    System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(1, 0, 0));
            }
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}