using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = Convert.ToInt32(Console.ReadLine());
            int[] arr = Array.ConvertAll<string, int>(Console.ReadLine().Split(), int.Parse);
            int[] arr2 = new int[n * 2];
            int cnt = 0;
            for(int i = 0; i < arr.Length; ++i)
            {
                arr2[cnt++] = arr[i];
                arr2[cnt++] = arr[i];
            }
            for(int i = 0; i < arr2.Length; ++i)
            {
                Console.WriteLine(arr2[i]);
                
            }
            Console.ReadKey();


        }
    }
}
