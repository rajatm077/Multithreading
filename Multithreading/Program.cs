using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multithreading {
    class Program {
        static void Main(string[] args) {
            string path = @"C:\Users\malhotr1\Pictures\New_folder";
            Console.WriteLine(path);
            Object res = TPLDemo.ParallelCalculateFileSize(path);
            Console.WriteLine("ParallelCalulate:{0}", res);

            res = TPLDemo.CalculateFileSize(path);
            Console.WriteLine("Normal Calulate:{0}", res);


        }
    }
}
