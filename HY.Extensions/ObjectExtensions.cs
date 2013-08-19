using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    /// <summary>
    /// Object扩展
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// 使用Newtonsoft.Json进行JSON
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson(object obj)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }
        /// <summary>
        /// 使用Newtonsoft.Json进行反JSON
        /// </summary>
        /// <param name="json"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object FromJson(string json, Type type)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject(json, type);
        }

        /// <summary>
        /// 全局log 入口, 需要在 main, global 中填写配置
        /// log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(path));
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static log4net.ILog Log4(this object obj)
        {
            return log4net.LogManager.GetLogger(obj.GetType());
        }
    }
}
