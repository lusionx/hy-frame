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

            byte[] inBuffer = new byte[eam.Length];
            eam.Read(inBuffer, 0, inBuffer.Length);

            var outStream = new MemoryStream();
            using (var ds = new DeflateStream(outStream, CompressionMode.Compress, true))
            {
                ds.Write(inBuffer, 0, inBuffer.Length);
                return outStream;
            }
        }

        /// <summary>
        /// 解压
        /// </summary>
        /// <param name="eam"></param>
        /// <returns></returns>
        public static Stream Decompress(Stream eam)
        {
            return Decompress(eam, 2);
        }


        /// <summary>
        /// 解压
        /// </summary>
        /// <param name="eam"></param>
        /// <param name="skip">=2 same time fix bug</param>
        /// <returns></returns>
        public static Stream Decompress(Stream eam, int skip)
        {
            // fix bug http://social.msdn.microsoft.com/Forums/zh-CN/visualcshartzhchs/thread/dbb43f3d-1ffa-44a9-91b5-924d705a74aa
            for (int i = 0; i < skip; i++)
            {
                eam.ReadByte();
            }
            using (var press = new DeflateStream(eam, CompressionMode.Decompress, eam is MemoryStream))
            {
                var outStream = new MemoryStream();
                press.CopyTo(outStream);
                return outStream;
            }
        }

        public static byte[] StreamToBytes(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }

        public static Stream BytesToStream(byte[] bytes)
        {
            Stream stream = new MemoryStream(bytes);
            return stream;
        }
    }
}
