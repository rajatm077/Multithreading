using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multithreading {
    class Program {
        static void Main(string[] args) {
            string source = @"C:\Users\malhotr1\Pictures\New_folder";
            string destination = @"C:\Users\malhotr1\Pictures\New_folder2";

            foreach (var file in Directory.GetFiles(destination)) {
                File.Delete(file);
            }
        
            Object res = TPLDemo.CalculateFileSize(source, destination);
            Console.WriteLine("Normal Calulate:{0}", res);

            foreach (var file in Directory.GetFiles(destination)) {
                File.Delete(file);
            }
            res = TPLDemo.ParallelCalculateFileSize(source, destination);
            Console.WriteLine("ParallelCalulate:{0}", res);           

        }
    }
}
