using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Collections.Generic
{
    /// <summary>
    /// 数组,列表扩展
    /// </summary>
    public static class ArrayExtensions
    {
        /// <summary>
        /// 检查是否存在
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static bool In<T>(this T obj, IEnumerable<T> arr) where T : class
        {
            return arr.AsQueryable().Contains(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="scource"></param>
        /// <param name="predicate"></param>
        public static void ForEach<T>(this IEnumerable<T> scource, Action<T> predicate)
        {
            foreach (var t in scource)
            {
                predicate(t);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="scource"></param>
        /// <param name="predicate"></param>
        public static void ForEach<T>(this IEnumerable<T> scource, Action<int, T> predicate)
        {
            var i = 0;
            foreach (var t in scource)
            {
                predicate(i++, t);
            }
        }

        /// <summary>
        /// 多维数组 降1维
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lst"></param>
        /// <returns></returns>
        public static IEnumerable<T> MergeList<T>(this IEnumerable<List<T>> lst) where T : class,new()
        {
            foreach (var a in lst)
            {
                foreach (var b in a)
                {
                    yield return b;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lst"></param>
        /// <returns></returns>
        public static IEnumerable<T> MergeList<T>(this IEnumerable<T[]> lst) where T : class,new()
        {
            foreach (var a in lst)
            {
                foreach (var b in a)
                {
                    yield return b;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strs"></param>
        /// <param name="sp"></param>
        /// <returns></returns>
        public static string Join(this IEnumerable<string> strs, string sp)
        {
            return string.Join(sp, strs.ToArray());
        }
    }
}
