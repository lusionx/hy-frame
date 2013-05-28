<%@ WebHandler Language="C#" Class="HY.Frame.Web.View.data" %>


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HY.Frame.Core;

namespace HY.Frame.Web.View
{
    /// <summary>
    /// data 的摘要说明
    /// </summary>
    public class data : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            var ls = new System.Collections.ArrayList();
            foreach (var i in Enumerable.Range(1, 10))
            {
                ls.Add(new { a = i, b = i + i });
            }

            var o = new { total = 55, data = ls };
            context.Response.Write(o.ToJson());
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