using System;
using System.Web;
using System.Collections.Generic;
using System.Collections;
using HY.Frame.Core.Toolkit;
using HY.Frame.Core;
using System.Xml.Linq;
using System.Linq;
using Newtonsoft.Json;
using System.IO;

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.HttpMethod.ToUpper() == "GET")
            {
                Get(context);
            }
            if (context.Request.HttpMethod.ToUpper() == "POST")
            {
                Post(context);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        protected void Get(HttpContext context)
        {
            var u = new AuthedUser();
            var root = u.InitTree(u.Root.Element("node"));
            var roles = u.Root.Element("roles").Elements("add").Select(a => a.Attribute("name").Value).ToList();


            var jw = new JsonTextWriter(context.Response.Output);

            jw.WriteStartArray();
            JWRPT(root, roles, jw);
            jw.WriteEndArray();


            jw.Close();

        }

        private void JWRPT(LinkNode root, List<string> roles, JsonWriter jw)
        {
            foreach (var n in root.Children)
            {
                if (!n.Enabled || (n.Title == n.Url && n.Url == string.Empty))
                {
                    continue;
                }
                jw.WriteStartObject();
                jw.WritePropertyName("Title");
                jw.WriteValue(n.Title);
                jw.WritePropertyName("Url");
                jw.WriteValue(n.Url);
                jw.WritePropertyName("leaf");
                jw.WriteValue(n.Children.Count == 0);


                foreach (var r in roles)
                {
                    jw.WritePropertyName(r);
                    jw.WriteValue(n.Roles.Contains(r));
                }

                jw.WritePropertyName("children");
                jw.WriteStartArray();

                if (n.Children.Count > 0)
                {
                    JWRPT(n, roles, jw);
                }

                jw.WriteEndArray();
                jw.WriteEndObject();

            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        protected void Post(HttpContext context)
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
                this.Log4().Error("保存出错", e);
                rep.StatusCode = 500;
                var obj = new HY.Frame.Core.ResResult { error = true, msg = e.Message };
                rep.Write(ObjectExtensions.ToJson(obj));
            }
        }
    }
}
