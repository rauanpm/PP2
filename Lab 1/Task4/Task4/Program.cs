using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine()); //Задаём высоту нашей треугольника
            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= i; j++)// цикл для вывода треугольника
                {
                    Console.Write("[*]");
                }
                Console.WriteLine();//Делит по блокам


            }
            Console.ReadKey();
       
        }
         
    }
}
