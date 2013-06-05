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
    /// 通过 $('#' + ModConfig.id).data(ModConfig.key)方法取得json数据, 
    /// 然后把json数据 交给AuthedUser.UpdateMods方法 修改xml
    /// </summary>
    public class ModConfigControl : WebControl
    {
        protected override void Render(HtmlTextWriter writer)
        {
            writer.WriteBeginTag("div");
            writer.WriteAttribute("id", this.ClientID);
            writer.WriteAttribute("class", this.CssClass);
            writer.Write(">");
            RenderTable(writer);
            writer.WriteEndTag("div");
            var cs = Page.ClientScript;
            writer.WriteBeginTag("script");
            writer.WriteAttribute("src", cs.GetWebResourceUrl(this.GetType(), "HY.Auth.ModConfigControl.js"));
            writer.Write(">");
            writer.WriteEndTag("script");

            //配置信息
            writer.WriteBeginTag("script");
            writer.Write(">");
            var cfg = new
            {
                id = this.ClientID,
                key = "modcfg"
            };

            writer.Write("ModConfig=");
            writer.Write(ObjectExtensions.ToJson(cfg));
            writer.Write(";");
            writer.WriteEndTag("script");

        }



        protected void RenderTable(HtmlTextWriter writer)
        {
            var u = new AuthedUser();
            var mods = u.Root.Element("mods").Elements().ToList();
            writer.WriteBeginTag("table");
            writer.WriteAttribute("class", "table table-condensed table-hover table-bordered table-striped");
            writer.Write(">");

            var roles = u.Root.Element("roles").Elements("add").Select(a => a.Attribute("name").Value).ToList();

            XElement txelm = null;
            //thead
            txelm = new XElement("thead", new XElement("tr"));
            txelm = txelm.FirstNode as XElement;

            txelm.Add(new XElement("th", new XAttribute("colspan", "2")));

            roles.ForEach(a => txelm.Add(new XElement("th", a)));
            writer.Write(txelm.Parent.ToString());

            //tbody
            txelm = new XElement("tbody");
            txelm.Add(RenderTbody(u.GetAllMods(), roles));


            writer.Write(txelm.ToString());

            writer.WriteEndTag("table");
        }

        protected List<XElement> RenderTbody(List<ModNode> nodes, List<string> roles)
        {
            var trs = new List<XElement>();

            nodes.ForEach(node =>
            {

                var tr = new XElement("tr");
                tr.Add(new XElement("td", new XAttribute("title", node.Url), node.Title));

                tr.Add(new XElement("td", node.Action));

                roles.ForEach(a =>
                {

                    if (node.Roles.Contains(a))
                    {
                        tr.Add(new XElement("td",
                            new XElement("Label", new XAttribute("title", a),
                                new XElement("input", new XAttribute("type", "checkbox"), new XAttribute("checked", "checked")))));
                    }
                    else
                    {
                        tr.Add(new XElement("td",
                            new XElement("Label", new XAttribute("title", a),
                                new XElement("input", new XAttribute("type", "checkbox")))));
                    }

                });
                trs.Add(tr);
            });



            return trs;
        }
    }
}
