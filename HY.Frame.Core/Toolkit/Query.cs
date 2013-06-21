using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HY.Frame.Core.Toolkit
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class Query
    {
        /// <summary>
        /// 
        /// </summary>
        public abstract string SearchText { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return SearchText;
        }
    }
}
