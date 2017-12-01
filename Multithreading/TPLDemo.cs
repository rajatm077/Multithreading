using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Multithreading {
    class TPLDemo {
        readonly Object obj = new object();

        enum ReturnCode { ResourceNotFound = -1, Ok};
        public static Object ParallelCalculateFileSize(string path, string dest) {
            long totalSize = 0;
            long size = 0;
            int returnCode = 0;            
            
            if (!(Directory.Exists(path) && Directory.Exists(dest))) {
                returnCode = (int)ReturnCode.ResourceNotFound;                
            }
            FileInfo[] files = new DirectoryInfo(path).GetFiles();

            Stopwatch sw = Stopwatch.StartNew();
            Parallel.For(0, files.Length, i => {                
                size = files[i].Length;
                Interlocked.Add(ref totalSize, size);

                String newFileName = dest + "\\" + files[i].Name;
                if (!File.Exists(newFileName)) {
                    files[i].CopyTo(newFileName);
                }
                 
            });
            sw.Stop();

            returnCode = (int)ReturnCode.Ok;

            return new {
                ResultCode = returnCode,
                TimeElapsed = sw.ElapsedTicks/100,
                Size = totalSize
            };
        }

        public static Object CalculateFileSize(string path, string dest) {
            long totalSize = 0;
            int returnCode = 0;
            if (!Directory.Exists(path)) {
                returnCode = (int)ReturnCode.ResourceNotFound;
            }

            FileInfo[] files = new DirectoryInfo(path).GetFiles();
            Stopwatch sw = Stopwatch.StartNew();


            foreach (var file in files) {
                totalSize += file.Length;
                file.CopyTo(dest + "\\" + file.Name);
            }
            sw.Stop();

            returnCode = (int)ReturnCode.Ok;

            return new {
                ResultCode = returnCode,
                TimeElapsed = sw.ElapsedTicks/100,
                Size = totalSize
            };
        }
    }
}
