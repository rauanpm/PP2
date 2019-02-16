using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    class Program
    {
        static int[]  Dmassive(int[] arr)
        {
            int cnt = 0;
            int[] arrx = new int[arr.Length * 2];
            for (int i = 0; i < arr.Length; i++)
            {
                arrx[cnt++] = arr[i];
                arrx[cnt++] = arr[i];

            }
            return arrx;

        }
        static void Main(string[] args)
        {
            int n = Convert.ToInt32(Console.ReadLine());
            int[] arr2 = Array.ConvertAll<string, int>(Console.ReadLine().Split(), int.Parse); 
            int[] show = Dmassive(arr2);
            for (int i = 0; i < show.Length; ++i)
            {
                Console.Write(show[i] + " ");
            }
            Console.ReadKey();


        }
    }
}
