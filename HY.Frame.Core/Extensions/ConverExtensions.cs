using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace HY.Frame.Core
{
    public static class ConverExtensions
    {
        public static int ToInt32(this string str, int def)
        {
            int v;
            if (Int32.TryParse(str, out v))
            {
                return v;
            }
            else
            {
                return def;
            }
        }

        public static int ToInt32(this string str)
        {
            return ConverExtensions.ToInt32(str, 0);
        }

        public static DateTime ToDateTime(this string str, DateTime def)
        {
            DateTime dt;
            if (DateTime.TryParse(str, out dt))
            {
                return dt;
            }
            else
            {
                return def;
            }
        }

        public static DateTime ToDateTime(this string str)
        {
            return ToDateTime(str, DateTime.Now);
        }

        public static decimal ToDecimal(this string str, decimal def)
        {
            decimal s;
            if (decimal.TryParse(str, out s))
            {
                return s;
            }
            else
            {
                return def;
            }
        }

        public static decimal ToDecimal(this string str)
        {
            return ToDecimal(str, 0);
        }

        public static Guid ToGuid(this string str)
        {
            return new Guid(str);
        }


        /// <summary>
        /// 生成给定字节数组的MD5串。
        /// </summary>
        public static string MD5(this string src)
        {
            byte[] b = Encoding.Default.GetBytes(src);
            b = new MD5CryptoServiceProvider().ComputeHash(b);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < b.Length; i++)
            {
                sb.Append(b[i].ToString("x").PadLeft(2, '0'));
            }
            return sb.ToString();
        }


    }
}
