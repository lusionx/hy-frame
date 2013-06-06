using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace HY.Frame.CMD
{
    public class DeflateTest : IRuner
    {

        public static void CompressFile(string sourceFile, string destFile)
        {
            FileStream inFs = new FileStream(sourceFile, FileMode.Open, FileAccess.Read);
            byte[] inBuffer = new byte[inFs.Length];
            inFs.Read(inBuffer, 0, inBuffer.Length);


            FileStream outFs = new FileStream(destFile, FileMode.Create);
            DeflateStream compressStream = new DeflateStream(outFs, CompressionMode.Compress);


            compressStream.Write(inBuffer, 0, inBuffer.Length);

            Console.Write(destFile + " Compress " + outFs.Length);

            compressStream.Close();
            outFs.Close();
        }


        /// <summary>
        /// 无效
        /// </summary>
        /// <param name="fi"></param>
        public void Compress(FileInfo fi)
        {
            // Get the stream of the source file.
            using (FileStream inFile = fi.OpenRead())
            {
                // Prevent compressing hidden and already compressed files.
                if ((File.GetAttributes(fi.FullName) & FileAttributes.Hidden)
                    != FileAttributes.Hidden & fi.Extension != ".cmp")
                {
                    // Create the compressed file.
                    using (FileStream outFile =
                            File.Create(fi.FullName + ".cmp"))
                    {
                        using (DeflateStream Compress =
                            new DeflateStream(outFile,
                            CompressionMode.Compress))
                        {
                            // Copy the source file into 
                            // the compression stream.
                            inFile.CopyTo(Compress);

                            Console.WriteLine("Compressed {0} from {1} to {2} bytes.",
                            fi.Name, fi.Length.ToString(), outFile.Length.ToString());
                        }
                    }
                }
            }
        }

        public void Go()
        {
            var path = "d:\\Users\\兴\\Desktop\\5月.txt";
            //Compress(new FileInfo(path));

            //CompressFile(path, path + ".cmp");

            var cmps = HY.Frame.Core.Toolkit.Deflate.Compress(new FileStream(path, FileMode.Open, FileAccess.Read));

            Console.Write(path + " Compress " + cmps.Length);
        }
    }
}
