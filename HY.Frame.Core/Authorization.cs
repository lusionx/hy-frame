using System;
using System.Web;

namespace HY.Frame.Core
{
    /// <summary>
    /// 实现自己的认证Module
    /// </summary>
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

        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose() { }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="context"></param>
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
