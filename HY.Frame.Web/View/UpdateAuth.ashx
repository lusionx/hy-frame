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
            var u = new Auth.AuthedUser();
            if (!string.IsNullOrEmpty(context.Request["nav"]))
            {
                u.UpdateNodes(context.Request["nav"]);
            }
            if (!string.IsNullOrEmpty(context.Request["mod"]))
            {
                u.UpdateMods(context.Request["mod"]);
            }
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