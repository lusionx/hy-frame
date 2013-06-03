using HY.Frame.Core.Toolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

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
                ln.Enabled = ln.Roles.Any(a => Roles.Contains(a));
            }
        }


        /// <summary>
        ///吧xml.node转换成对象
        /// </summary>
        /// <param name="rootNode"></param>
        /// <returns></returns>
        protected LinkNode InitTree(XElement rootNode)
        {
            var node = new LinkNode
            {
                Url = rootNode.Attribute("url").Value,
                Enabled = true,
                Title = rootNode.Attribute("url").Value,
                Children = new List<LinkNode>(),
                Roles = FixRoleString(rootNode.Attribute("roles").Value)
            };

            rootNode.Elements("node").ToList().ForEach(a =>
                {
                    node.Children.Add(InitTree(a));
                });

            return node;
        }
    }
}
