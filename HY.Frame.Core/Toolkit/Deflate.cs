using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;

namespace HY.Frame.Core.Toolkit
{
    /// <summary>
    /// 使用DeflateStream进行 处理
    /// </summary>
    public class Deflate
    {
        /// <summary>
        /// 压缩
        /// </summary>
        /// <param name="eam"></param>
        /// <returns></returns>
        public static Stream Compress(Stream eam)
        {
            var outStream = new MemoryStream();
            var press = new System.IO.Compression.DeflateStream(outStream, CompressionMode.Compress);
            eam.CopyTo(press);
            return outStream;
        }

        /// <summary>
        /// 解压
        /// </summary>
        /// <param name="eam"></param>
        /// <returns></returns>
        public static Stream Decompress(Stream eam)
        {
            var press = new System.IO.Compression.DeflateStream(eam, CompressionMode.Decompress);
            var outStream = new MemoryStream();
            press.CopyTo(outStream);
            return outStream;
        }
    }
}
