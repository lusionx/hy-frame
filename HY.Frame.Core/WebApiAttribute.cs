using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace HY.Frame.Core
{
    /// <summary>
    /// 修饰一个方法，此方法可以被js访问
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class WebApiAttribute : Attribute
    {

    }


    /// <summary>
    /// 修饰一个方法, 在调用方法之前,调用handler, 需要实现 IHttpHandler
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class ActionBeforeHandlerAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="handler"></param>
        public ActionBeforeHandlerAttribute(Type handler)
        {
            Handler = handler;
        }
        /// <summary>
        /// 
        /// </summary>
        public Type Handler { get; set; }
    }

    /// <summary>
    /// 修饰一个方法, 在调用方法之后,调用handler, 需要实现 IHttpHandler
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class ActionAfterHandlerAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="handler"></param>
        public ActionAfterHandlerAttribute(Type handler)
        {
            Handler = handler;
        }
        /// <summary>
        /// 
        /// </summary>
        public Type Handler { get; set; }
    }
}
