using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    /// <summary>
    /// Boolean扩展
    /// </summary>
    public static class BooleanExtensions
    {
        /// <summary>
        /// 表达式true
        /// </summary>
        /// <param name="bo"></param>
        /// <param name="pr"></param>
        public static void True(this bool bo, Action pr)
        {
            if (bo)
            {
                pr();
            }
            else
            {
                throw new ArgumentException("bool应为 true");
            }
        }

        /// <summary>
        /// 表达式false
        /// </summary>
        /// <param name="bo"></param>
        /// <param name="pr"></param>
        public static void False(this bool bo, Action pr)
        {
            if (bo)
            {
                throw new ArgumentException("bool应为 false");
            }
            else
            {
                pr();
            }
        }
    }
}
