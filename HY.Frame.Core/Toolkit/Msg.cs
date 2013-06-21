using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;


namespace HY.Frame.Core.Toolkit
{
    /// <summary>
    /// 读取~/App_Data/Msg.xml 的信息
    /// </summary>
    public class Msg
    {
        private static XElement _root;

        /// <summary>
        /// 
        /// </summary>
        public static XElement Root
        {
            get
            {
                string path = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Msg.xml");
                if (System.Web.HttpContext.Current.IsDebuggingEnabled)
                {
                    return System.Xml.Linq.XDocument.Load(path).FirstNode as XElement;
                }
                if (_root == null)
                {
                    var doc = System.Xml.Linq.XDocument.Load(path);
                    _root = doc.FirstNode as XElement;
                }
                return _root;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static string Get(Type type, int index)
        {
            var root = Root;
            var cls = root.Elements("class").Single(a => a.Attribute("name").Value == type.FullName);
            return cls.Elements("add").ElementAt(index).Value.Trim();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string Get(Type type, string name)
        {
            var root = Root;
            var cls = root.Elements("class").Single(a => a.Attribute("name").Value == type.FullName);
            return cls.Elements("add").Single(a => a.Attribute("name").Value == name).Value.Trim();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cname"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string Get(string cname, string name)
        {
            var root = Root;
            var cls = root.Elements("class").Single(a => a.Attribute("name").Value == cname);
            return cls.Elements("add").Single(a => a.Attribute("name").Value == name).Value.Trim();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cname"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static string Get(string cname, int index)
        {
            var root = Root;
            var cls = root.Elements("class").Single(a => a.Attribute("name").Value == cname);
            return cls.Elements("add").ElementAt(index).Value.Trim();
        }
    }
}
