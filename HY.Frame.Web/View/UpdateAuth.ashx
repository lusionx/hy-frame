<%@ WebHandler Language="C#" Class="HY.Frame.Web.View.UpdateAuth" %>


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HY.Frame.Core;

namespace HY.Frame.Web.View
{
    /// <summary>
    /// UpdateAuth 的摘要说明
    /// </summary>
    public class UpdateAuth : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            new Auth.AuthedUser().UpdateNodes(context.Request["data"]);
            context.Response.Write(1);
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