using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace HY.Frame.Core
{
    public static class DataTableExtensions
    {
        public static string ToJson(this DataTable dt)
        {
            StringBuilder sb = new StringBuilder();
            var dt_star = new DateTime(1970, 1, 1);
            string[] cm = { "[", "]" };
            string[] cl = { "{", "}" };
            string[] sp = { ",", ":", "\"{0}\"", "new Date({0})" };

            sb.Append(cm[0]);
            var vtype = new Type[] { typeof(int), typeof(float), typeof(decimal) };
            foreach (var dr in dt.AsEnumerable())
            {
                sb.Append(cl[0]);
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    sb.AppendFormat(sp[2], dt.Columns[i].ColumnName);
                    sb.Append(sp[1]);
                    var v = dr[i];
                    if (vtype.Contains(v.GetType()))
                    {
                        sb.Append(v);
                    }
                    else if (v is DBNull)
                    {
                        sb.Append("null");
                    }
                    else if (v is DateTime)
                    {
                        sb.AppendFormat(sp[3], Convert.ToInt64(((DateTime)v - dt_star).TotalMilliseconds));
                    }
                    else
                    {
                        sb.AppendFormat(sp[2], v);
                    }
                    sb.Append(sp[0]);
                }
                sb.Remove(sb.Length - 1, 1);
                sb.Append(cl[1]);
                sb.Append(sp[0]);
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append(cm[1]);
            return sb.ToString();
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
            return DataTableExtensions.ConverColDateToString(dt, "yyyy-MM-dd");
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
