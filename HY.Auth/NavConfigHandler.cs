using System;
using System.Web;
using HY.Frame.Core.Toolkit;
using HY.Frame.Core;

namespace HY.Auth
{
    public class NavConfigHandler : IHttpHandler
    {
        /// <summary>
        /// 您将需要在网站的 Web.config 文件中配置此处理程序 
        /// 并向 IIS 注册它，然后才能使用它。有关详细信息，
        /// 请参见下面的链接: http://go.microsoft.com/?linkid=8101007
        /// </summary> 
        public bool IsReusable
        {
            // 如果无法为其他请求重用托管处理程序，则返回 false。
            // 如果按请求保留某些状态信息，则通常这将为 false。
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            var u = new AuthedUser();
            var rep = context.Response;
            try
            {
                var bytes = new byte[context.Request.InputStream.Length];
                context.Request.InputStream.Read(bytes, 0, bytes.Length);
                u.UpdateNodes(System.Text.Encoding.UTF8.GetString(bytes));
                var obj = new HY.Frame.Core.ResResult();
                rep.Write(ObjectExtensions.ToJson(obj));
            }
            catch (Exception e)
            {
                HY.Frame.Core.Log.Get(this.GetType()).Error("保存出错", e);
                rep.StatusCode = 500;
                var obj = new HY.Frame.Core.ResResult { error = true, msg = e.Message };
                rep.Write(ObjectExtensions.ToJson(obj));
            }
        }
    }
}
