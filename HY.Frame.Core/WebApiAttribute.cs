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
    /// 
    /// </summary>
    public abstract class ActionHandlerAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public abstract void ProcessRequest(HttpContext context);
    }

    /// <summary>
    /// 修饰一个方法, 在调用方法之前,调用ProcessRequest
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public abstract class ActionBeforeHandlerAttribute : ActionHandlerAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public abstract override void ProcessRequest(HttpContext context);
    }

    /// <summary>
    /// 修饰一个方法, 在调用方法之后,调用ProcessRequest
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public abstract class ActionAfterHandlerAttribute : ActionHandlerAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public abstract override void ProcessRequest(HttpContext context);
    }
}
