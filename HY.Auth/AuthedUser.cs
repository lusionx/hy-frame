using HY.Frame.Core.Toolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using HY.Frame.Core;

namespace HY.Auth
{
    public class AuthedUser : ConfigBase
    {
        protected override string ConfigFilePath
        {
            get
            {
                return System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Auth.xml");
            }
        }

        public IList<string> Roles { get; set; }

        public AuthedUser(IEnumerable<string> roles)
        {
            Roles = roles.Select(a => a.Trim()).ToList();
        }

        private List<string> FixRoleString(string role)
        {

            return role.Split(';', ',', ' ').Where(a => !string.IsNullOrEmpty(a)).Select(a => a.Trim()).ToList();
        }

        public AuthedUser(string role)
        {
            Roles = FixRoleString(role);
        }

        public AuthedUser()
        {
            Roles = new List<string>();
        }


        public List<LinkNode> GetNodes()
        {
            var node = InitTree(Root.Element("node"));
            SetNodeEnable(node);

            return node.Children;
        }

        protected void RemoveNode(LinkNode ln)
        {
            ln.Children = ln.Children.Where(a => a.Enabled).ToList();
            ln.Children.ForEach(a =>
            {
                RemoveNode(a);
            });
        }


        protected void SetNodeEnable(LinkNode ln)
        {
            if (ln.Children != null && ln.Children.Count > 0)
            {
                ln.Children.ForEach(a =>//先计算 子节点
                {
                    SetNodeEnable(a);
                });
                //通过子节点计算自己
                ln.Enabled = ln.Children.Any(a => a.Enabled);
            }
            else// 我是最末节点
            {
                ln.Enabled = ln.Roles.Count == 0 || ln.Roles.Any(a => Roles.Contains(a));
            }
        }


        /// <summary>
        ///吧xml.node转换成对象
        /// </summary>
        /// <param name="rootNode"></param>
        /// <returns></returns>
        internal LinkNode InitTree(XElement rootNode)
        {
            var node = new LinkNode
            {
                Url = rootNode.Attribute("url").Value,
                Enabled = true,
                Title = rootNode.Attribute("title").Value,
                Desc = rootNode.Attribute("desc").Value,
                Children = new List<LinkNode>(),
                Roles = FixRoleString(rootNode.Attribute("roles").Value)
            };

            rootNode.Elements("node").ToList().ForEach(a =>
                {
                    node.Children.Add(InitTree(a));
                });

            return node;
        }


        /// <summary>
        /// 更新xml
        /// </summary>
        /// <param name="json">[LinkNode]的json格式</param>
        public void Update(string json)
        {
            var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            var ls = new List<LinkNode>();
            try
            {
                ls = jss.Deserialize(json, ls.GetType()) as List<LinkNode>;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            var node = Root.Element("node");

            foreach (var item in node.Descendants("node"))
            {
                var match = ls.FirstOrDefault(a => a.Title == item.Attribute("title").Value && a.Url == item.Attribute("url").Value);
                if (match != null)
                {
                    item.Attribute("roles").Value = match.Roles.Join(",");
                }
            }

            node.Document.Save(ConfigFilePath, SaveOptions.None);
        }
    }
}
