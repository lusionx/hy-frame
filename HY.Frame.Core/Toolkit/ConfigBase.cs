using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace HY.Frame.Core.Toolkit
{
    /// <summary>
    /// 读取xml并缓存
    /// </summary>
    public class ConfigBase
    {
        /// <summary>
        /// 配置文件的物理地址
        /// </summary>
        protected virtual string ConfigFilePath
        {
            get
            {
                return System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Config.xml");
            }
        }

        private const string CACHE_PREFIX = "HY.Frame.Core.Toolkit.ConfigBase.CACHE_PREFIX.";

        public XElement Root
        {
            get
            {
                string path = ConfigFilePath;
                if (System.Web.HttpContext.Current.IsDebuggingEnabled)
                {
                    return System.Xml.Linq.XDocument.Load(path).FirstNode as XElement;
                }
                var key = CACHE_PREFIX + path;
                if (!CacheData.Exist(key))
                {
                    var doc = System.Xml.Linq.XDocument.Load(path);
                    var root = doc.FirstNode as XElement;
                    CacheData.Add(key, root);
                }
                return CacheData.Get(key) as XElement;
            }
        }
    }
}
