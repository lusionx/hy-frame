using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HY.Frame.Core
{
    /// <summary>
    /// 修饰一个方法，此方法可以被js访问
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class WebApiAttribute : Attribute
    {

    }
}
