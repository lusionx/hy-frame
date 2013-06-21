using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HY.Frame.Core
{
    /// <summary>
    /// 数据缓存入口
    /// </summary>
    public class CacheData
    {
        /// <summary>
        /// 
        /// </summary>         
        /// <param name="key">请自行保证,key不会重复，推荐使用当前类的fullname为前缀</param>
        /// <param name="obj"></param>
        /// <param name="cacheHour"></param>
        public static void Add(string key, object obj, int cacheHour)
        {
            var cache = System.Web.HttpContext.Current.Cache;
            cache.Insert(key, obj, null,
                DateTime.Now.AddHours(Convert.ToDouble(cacheHour)),
                System.Web.Caching.Cache.NoSlidingExpiration);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="span"></param>
        public static void Add(string key, object obj, TimeSpan span)
        {
            var cache = System.Web.HttpContext.Current.Cache;
            cache.Insert(key, obj, null, System.Web.Caching.Cache.NoAbsoluteExpiration, span);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key">请自行保证,key不会</param>
        /// <param name="obj"></param>
        public static void Add(string key, object obj)
        {
            Add(key, obj, DefaultCacheHours);
        }

        /// <summary>
        /// 取得缓存
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static TResult Get<TResult>(string key) where TResult : class
        {
            var cache = System.Web.HttpContext.Current.Cache;
            var obj = cache.Get(key);
            if (obj == null)
            {
                throw new NullReferenceException("缓存不存在");
            }
            return obj as TResult;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object Get(string key)
        {
            var cache = System.Web.HttpContext.Current.Cache;
            var obj = cache.Get(key);
            if (obj == null)
            {
                throw new NullReferenceException("缓存不存在");
            }
            return obj;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool Exist(string key)
        {
            var cache = System.Web.HttpContext.Current.Cache;
            var obj = cache.Get(key);
            return obj != null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        public static void Remove(string key)
        {
            var cache = System.Web.HttpContext.Current.Cache;
            if (Exist(key))
            {
                cache.Remove(key);
            }
        }

        private const int DefaultCacheHours = 1;
    }
}
