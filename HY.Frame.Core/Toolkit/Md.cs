using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace HY.Frame.Core.Toolkit
{
    public class MD5
    {

        public string Compute(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                throw new ArgumentNullException("str");
            }

            return Compute(System.Text.Encoding.UTF8.GetBytes(str));
        }

        /// <summary>
        /// 生成给定字节数组的MD5串。
        /// </summary>
        public string Compute(byte[] src)
        {
            if (src == null) throw new ArgumentNullException("src");
            if (src.Length == 0) throw new ArgumentNullException("src");

            byte[] b = new MD5CryptoServiceProvider().ComputeHash(src);

            return GetHexStr(b);
        }

        /// <summary>
        /// 获取指定字节数组的十六进制表示。
        /// </summary>
        public string GetHexStr(byte[] b)
        {
            if (b == null) throw new ArgumentNullException("src");
            if (b.Length == 0) throw new ArgumentNullException("src");

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < b.Length; i++)
            {
                sb.Append(b[i].ToString("x").PadLeft(2, '0'));
            }

            return sb.ToString();
        }
    }
}

