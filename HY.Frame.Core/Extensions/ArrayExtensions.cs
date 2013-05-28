using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HY.Frame.Core
{
    public static class ArrayExtensions
    {
        public static bool In<T>(this T obj, IEnumerable<T> arr) where T : class
        {
            return arr.AsQueryable().Contains(obj);
        }

        public static void Action<T>(this IEnumerable<T> scource, Action<T> predicate)
        {
            foreach (var t in scource)
            {
                predicate(t);
            }
        }

        public static void Action<T>(this IEnumerable<T> scource, Action<int, T> predicate)
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

        public static string Join(this IEnumerable<string> strs, string sp)
        {
            return string.Join(sp, strs.ToArray());
        }
    }
}
