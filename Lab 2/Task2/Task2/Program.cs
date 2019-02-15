
using System;

using System.Collections.Generic;

using System.IO;

using System.Linq;

using System.Text;

using System.Threading.Tasks;



namespace Task2
{

    class Program
    {

        static bool IsPal(string s)
        {

            for (int i = 0; i < s.Length / 2; i++)
            {

                if (s[i] != s[s.Length - 1 - i])
                {

                    return false;

                }

            }

            return true;

        }



        static void Main(string[] args)
        {

            using (StreamReader sr = new StreamReader(@"C:\Users\Faizullayev Rauan\Desktop\PP2\Lab 2\Task2\input.txt"))
            {

                if (IsPal(sr.ReadLine()))
                {

                    Console.WriteLine("Yes");

                }
                else
                {

                    Console.WriteLine("No");

                }

            }

        }

    }

}