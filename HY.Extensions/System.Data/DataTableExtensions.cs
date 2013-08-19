using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace System.Data
{
    /// <summary>
    /// 
    /// </summary>
    public static class DataTableExtensionsHY
    {
        /// <summary>
        /// 使用Newtonsoft.Json.Converters.DataTableConverte进行转换
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToJson(this DataTable dt)
        {
            System.IO.StringWriter sw = new System.IO.StringWriter();

            JsonTextWriter jw = new JsonTextWriter(sw);

            Newtonsoft.Json.Converters.DataTableConverter cv = new Newtonsoft.Json.Converters.DataTableConverter();

            cv.WriteJson(jw, dt, new JsonSerializer());

            return sw.ToString();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToJson2(this DataTable dt)
        {
            System.IO.StringWriter sw = new System.IO.StringWriter();
            JsonTextWriter writer = new JsonTextWriter(sw);
            var dt_star = new DateTime(1970, 1, 1);

            Newtonsoft.Json.Converters.JavaScriptDateTimeConverter dtc = new Newtonsoft.Json.Converters.JavaScriptDateTimeConverter();

            writer.WriteStartArray();
            var vtype = new Type[] { typeof(int), typeof(float), typeof(decimal) };
            foreach (var dr in dt.AsEnumerable())
            {
                writer.WriteStartObject();
                foreach (DataColumn col in dt.Columns)
                {
                    writer.WritePropertyName(col.ColumnName);
                    var v = dr[col.ColumnName];
                    if (vtype.Contains(v.GetType()))
                    {
                        writer.WriteRawValue(v.ToString());
                    }
                    else if (v is DBNull)
                    {
                        writer.WriteNull();
                    }
                    else if (v is DateTime)
                    {
                        var ms = Convert.ToInt64(((DateTime)v - dt_star).TotalMilliseconds);
                        dtc.WriteJson(writer, v, new JsonSerializer());
                    }
                    else
                    {
                        writer.WriteRawValue(v.ToString());
                    }
                    writer.WriteEndObject();
                }

            }
            writer.WriteEndArray();
            return sw.ToString();
        }

        /// <summary>
        /// 根据条件, 过滤掉部分数据
        /// </summary>
        /// <param name="dts"></param>
        /// <param name="ff"></param>
        /// <returns></returns>
        public static DataTable Where(this DataTable dts, Func<DataRow, bool> ff)
        {
            var dt = dts.Clone();
            foreach (var item in dts.AsEnumerable())
            {
                if (ff(item))
                {
                    dt.Rows.Add(item.ItemArray);
                }
            }
            return dt;
        }

        /// <summary>
        /// 把 byte[] 列类型数据 转换成 guid
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DataTable ConverColBytesToGuid(this DataTable dt)
        {
            var addCols = new List<DataColumn>();
            var cols = new Dictionary<string, string>();
            foreach (DataColumn col in dt.Columns)//增加列
            {
                if (col.DataType == typeof(byte[]))
                {
                    cols.Add(col.ColumnName, col.ColumnName + "_guid");
                    addCols.Add(new DataColumn(col.ColumnName + "_guid", typeof(Guid)));
                }
            }

            if (addCols.Count == 0)
            {
                return dt;
            }

            dt.Columns.AddRange(addCols.ToArray());

            foreach (var row in dt.AsEnumerable())//赋值
            {
                foreach (var item in cols)
                {
                    row.SetField<Guid>(item.Value, new Guid(row.Field<byte[]>(item.Key)));
                }
            }

            foreach (var item in cols)//移除原列,用 guid 列 顶替
            {
                dt.Columns.Remove(item.Key);
                dt.Columns[item.Value].ColumnName = item.Key;
            }
            return dt;
        }

        /// <summary>
        /// 把 datetime 的列 转成 字符 
        /// </summary>
        /// <param name="dt"></param> 
        /// <returns></returns>
        public static DataTable ConverColDateToString(this DataTable dt)
        {
            return DataTableExtensionsHY.ConverColDateToString(dt, "yyyy-MM-dd");
        }

        /// <summary>
        /// 把 datetime 的列 转成 字符
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="format">时间格式</param>
        /// <returns></returns>
        public static DataTable ConverColDateToString(this DataTable dt, string format)
        {
            var addCols = new List<DataColumn>();
            var cols = new Dictionary<string, string>();
            foreach (DataColumn col in dt.Columns)//增加列
            {
                if (col.DataType == typeof(DateTime))
                {
                    cols.Add(col.ColumnName, col.ColumnName + "_DateTime");
                    addCols.Add(new DataColumn(col.ColumnName + "_DateTime", typeof(string)));
                }
            }

            if (addCols.Count == 0)
            {
                return dt;
            }

            dt.Columns.AddRange(addCols.ToArray());

            foreach (var row in dt.AsEnumerable())//赋值
            {
                foreach (var item in cols)
                {
                    if (row.IsNull(item.Key))
                    {
                        row.SetField<string>(item.Value, null);
                    }
                    else
                    {
                        row.SetField<string>(item.Value, row.Field<DateTime>(item.Key).ToString(format));
                    }
                }
            }

            foreach (var item in cols)//移除原列,用 guid 列 顶替
            {
                dt.Columns.Remove(item.Key);
                dt.Columns[item.Value].ColumnName = item.Key;
            }
            return dt;
        }
    }
}
