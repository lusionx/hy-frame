using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace HY.Frame.Core
{
    public class Registration
    {
        public static void ToJSON(Type ty, IJSON i)
        {
            if (_jsonMap.ContainsKey(ty))
            {
                throw new ArgumentException(string.Format("已经为{0}注册了{1}",
                    ty.FullName, _jsonMap[ty].GetType().FullName));
            }
            _jsonMap.Add(ty, i);
        }

        static Registration()
        {
            if (_jsonMap == null)
            {
                _jsonMap = new Dictionary<Type, IJSON>();
            }

            _jsonMap.Add(typeof(DataTable), new DataTableToJSON());

        }

        private static Dictionary<Type, IJSON> _jsonMap;

        /// <summary>
        /// 查看现有的json方法映射
        /// </summary>
        /// <returns></returns>
        public static Dictionary<Type, IJSON> JsonMapView()
        {
            var dic = new Dictionary<Type, IJSON>();
            foreach (var key in _jsonMap.Keys)
            {
                dic.Add(key, _jsonMap[key]);
            }
            return dic;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        internal static IJSON FindIJSON(object obj)
        {
            var t = obj.GetType();
            if (_jsonMap.ContainsKey(t))
            {
                return _jsonMap[t];
            }
            return new ObjectToJSON();
        }

        internal class ObjectToJSON : IJSON
        {
            public string ToJSON(object obj)
            {
                return ObjectExtensions.ToJson(obj);
            }
        }

        internal class DataTableToJSON : IJSON
        {
            public string ToJSON(object obj)
            {
                if (obj == null)
                {
                    throw new ArgumentNullException();
                }
                if (!(obj is DataTable))
                {
                    throw new ArgumentException("参数不是DataTable类型");
                }

                return (obj as DataTable).ToJson();
            }
        }
    }
}
