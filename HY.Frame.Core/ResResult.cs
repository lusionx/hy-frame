using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HY.Frame.Core
{
    /// <summary>
    /// 返回此类整体json
    /// </summary>
    public class ResResult
    {
        /// <summary>
        /// error = false; msg = "ok";
        /// </summary>
        public ResResult()
        {
            error = false;
            msg = "ok";
        }
        /// <summary>
        /// 
        /// </summary>
        public bool error { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public object data { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string msg { get; set; }
    }

    /// <summary>
    /// 只输出data字段.tojson, 不管其他
    /// </summary>
    public class ResJsonResult : ResResult
    {
         
    }

    /// <summary>
    /// 只输出data字段.ToString, 不管其他
    /// </summary>
    public class ResStringResult : ResResult
    {

    }

}
