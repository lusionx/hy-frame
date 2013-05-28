using System;
using System.Web;

namespace HY.Frame.Core
{
    public class Authorization : IHttpModule
    {        
        /// <summary>
        /// 认证失败,则不会有后续的请求
        /// </summary>
        /// <returns></returns>
        protected virtual bool IsAuthorization()
        {
            return true;
        }

        public void Dispose() { }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += context_BeginRequest;
        }

        void context_BeginRequest(object sender, EventArgs e)
        {
            HttpApplication application = sender as HttpApplication;

            if (!IsAuthorization())
            {
                application.CompleteRequest();
                application.Context.Response.Write("请求被终止");
            }
        }
    }
}
