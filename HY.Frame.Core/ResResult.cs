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
        public bool error { get; set; }
        public object data { get; set; }
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
