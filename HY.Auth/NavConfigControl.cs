using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace HY.Auth
{
    public class NavConfigControl : WebControl
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
            writer.WriteAttribute("src", cs.GetWebResourceUrl(this.GetType(), "HY.Auth.ClientControl.js"));
            writer.Write(">");
            writer.WriteEndTag("script");
        }

        protected const int MAX_DEEP = 3;

        protected void RenderTable(HtmlTextWriter writer)
        {
            var u = new AuthedUser();
            var linknode = u.InitTree(u.Root.Element("node"));
            writer.WriteBeginTag("table");
            writer.WriteAttribute("class", "table table-condensed table-hover table-bordered table-striped");
            writer.Write(">");

            var lv = MAX_DEEP;
            var roles = u.Root.Element("roles").Elements("add").Select(a => a.Attribute("name").Value).ToList();

            XElement txelm = null;
            //thead
            txelm = new XElement("thead", new XElement("tr"));
            txelm = txelm.FirstNode as XElement;

            txelm.Add(new XElement("th", new XAttribute("colspan", MAX_DEEP)));

            roles.ForEach(a => txelm.Add(new XElement("th", a)));
            writer.Write(txelm.Parent.ToString());

            //tbody
            txelm = new XElement("tbody");

            linknode.Children.ForEach(a =>
            {
                txelm.Add(RenderTbody(0, a, roles));
            });

            writer.Write(txelm.ToString());

            writer.WriteEndTag("table");
        }

        protected List<XElement> RenderTbody(int deep, LinkNode node, List<string> roles)
        {
            if (string.IsNullOrEmpty(node.Title))
            {
                return new List<XElement>();
            }

            var tr = new XElement("tr");

            for (int i = 0; i < MAX_DEEP; i++)
            {
                if (i != deep)
                {
                    tr.Add(new XElement("td"));
                }
                else
                {
                    tr.Add(new XElement("td", new XAttribute("title", node.Url), node.Title));
                }
            }


            roles.ForEach(a =>
            {
                if (node.Children.Count == 0)
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
                }
                else
                {
                    tr.Add(new XElement("td"));
                }
            });

            var trs = new List<XElement>();
            trs.Add(tr);

            node.Children.ForEach(a =>
                {
                    trs.AddRange(RenderTbody(deep + 1, a, roles));
                });

            return trs;
        }

    }
}
