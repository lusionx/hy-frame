using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using HY.Frame.Core;

namespace HY.Auth
{
    /// <summary>
    /// 通过 $('#' + NavConfig.id).data(NavConfig.key)方法取得json数据, 
    /// 然后把json数据 交给AuthedUser.UpdateNodes方法 修改xml
    /// </summary>
    public class NavConfigControl : WebControl
    {
        protected override void Render(HtmlTextWriter writer)
        {
            writer.WriteBeginTag("div");
            writer.WriteAttribute("id", this.ClientID);
            writer.WriteAttribute("class", this.CssClass);
            writer.Write(">");
            writer.WriteEndTag("div");
            //RenderTable(writer);

            writer.WriteBeginTag("form");
            writer.WriteAttribute("id", this.ClientID + "_form");
            writer.WriteAttribute("method", "post");
            writer.WriteAttribute("action", this.ClientID + ".NavConfig");
            writer.Write(">");

            var cs = Page.ClientScript;
            writer.WriteBeginTag("script");
            writer.WriteAttribute("src", cs.GetWebResourceUrl(this.GetType(), "HY.Auth.NavConfigControl.js"));
            writer.Write(">");
            writer.WriteEndTag("script");

            //配置信息
            writer.WriteBeginTag("script");
            writer.Write(">");
            var u = new AuthedUser();
            var cfg = new
            {
                id = this.ClientID,
                key = "authcfg",
                tree = u.InitTree(u.Root.Element("node")),
                roles = u.Root.Element("roles").Elements("add").Select(a => a.Attribute("name").Value).ToList()
            };

            writer.Write("NavConfig=");
            writer.Write(ObjectExtensions.ToJson(cfg));
            writer.Write(";");


            writer.WriteEndTag("script");

            writer.Write("<input type=\"hidden\" id=\"{0}\" name=\"{1}\" />", cfg.id + "_hd", cfg.key);

            writer.WriteEndTag("form");

        }         
    }
}
