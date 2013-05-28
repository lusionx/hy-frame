<%@ WebHandler Language="C#" Class="HY.Frame.Web.Api.Log" %>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace HY.Frame.Web.Api
{
    /// <summary>
    /// Log 的摘要说明
    /// </summary>
    public class Log : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            var req = context.Request;
            var pathinf = req.Url.Query.Substring(1);
            var path = context.Server.MapPath(pathinf);
            var root = context.Server.MapPath("~/");
            if (System.IO.Directory.Exists(path))
            {
                context.Response.ContentType = "text/html; charset=utf-8";
                foreach (var item in System.IO.Directory.GetFiles(path))
                {
                    var file = item.Substring(root.Length - 1).Replace("\\", "/");
                    context.Response.Write(string.Format("<p><a href='log.ashx?{0}'>{0}</a></p><br/>", file, req.Url.AbsoluteUri));
                }
            }

            var pathf = context.Server.MapPath(pathinf);
            if (System.IO.File.Exists(pathf))
            {
                context.Response.ContentType = "text/pain; charset=utf-8";
                System.IO.FileStream fs = new System.IO.FileStream(pathf, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                StreamReader objReader = new StreamReader(fs);

                context.Response.Write(objReader.ReadToEnd());
            }
            context.Response.Write("");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
