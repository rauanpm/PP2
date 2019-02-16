using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TestCopyCreateDelete
{
    class Program
    {
        static void Main(string[] args)
        {
            string FileName = "Rauan.txt";
            string sourcePath = @"C:\Users\Faizullayev Rauan\Desktop\PP2\Lab 2\Task4\1";
            string targetPath = @"C:\Users\Faizullayev Rauan\Desktop\PP2\Lab 2\Task4\2";

            string sourceFile = System.IO.Path.Combine(sourcePath, FileName);
            string destFile = System.IO.Path.Combine(targetPath, FileName);

            if (System.IO.Directory.Exists(sourcePath))
            {
                string[] files = System.IO.Directory.GetFiles(sourcePath);

                foreach (string s in files)
                {
                    FileName = System.IO.Path.GetFileName(s);
                    destFile = System.IO.Path.Combine(targetPath, FileName);
                    System.IO.File.Copy(s, destFile, true);
                }
            }
            if (System.IO.File.Exists(@"C:\Users\Faizullayev Rauan\Desktop\PP2\Lab 2\Task4\1\Rauan.txt"))
            {
                try
                {
                    System.IO.File.Delete(@"C:\Users\Faizullayev Rauan\Desktop\PP2\Lab 2\Task4\1\Rauan.txt");
                }
                catch
                {
                    Console.WriteLine("Error file does not exist");
                    return;
                }
            }
            else
            {
                Console.WriteLine("Source path does not exist");
            }
            Console.ReadKey();
        }
    }
}