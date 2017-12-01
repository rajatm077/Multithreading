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
        enum ReturnCode { ResourceNotFound = -1, Ok};
        public static Object ParallelCalculateFileSize(string path) {
            long totalSize = 0;
            int returnCode = 0;
            if (!Directory.Exists(path)) {
                returnCode = (int)ReturnCode.ResourceNotFound;                
            }

            Stopwatch sw = Stopwatch.StartNew();
            FileInfo[] files = new DirectoryInfo(path).GetFiles();
            Parallel.For(0, files.Length, i => {
                long size = files[i].Length;
                Interlocked.Add(ref totalSize, size);
            });
            sw.Stop();

            returnCode = (int)ReturnCode.Ok;

            return new {
                ResultCode = returnCode,
                TimeElapsed = sw.ElapsedTicks,
                Size = totalSize
            };
        }

        public static Object CalculateFileSize(string path) {
            long totalSize = 0;
            int returnCode = 0;
            if (!Directory.Exists(path)) {
                returnCode = (int)ReturnCode.ResourceNotFound;
            }

            Stopwatch sw = Stopwatch.StartNew();
            FileInfo[] files = new DirectoryInfo(path).GetFiles();
            foreach (var file in files) {
                totalSize += file.Length;
            }
            sw.Stop();

            returnCode = (int)ReturnCode.Ok;

            return new {
                ResultCode = returnCode,
                TimeElapsed = sw.ElapsedTicks,
                Size = totalSize
            };
        }
    }
}
