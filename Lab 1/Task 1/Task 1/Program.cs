using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            int q = Convert.ToInt32(Console.ReadLine());                                         // Get the number of numbers
            int[] arr = Array.ConvertAll<string, int>(Console.ReadLine().Split(), int.Parse);   // Create an array of read numbers
            int cnt = 0;                                                                       // Create counter to prime numbers
            List<int> p = new List<int>();                                                    // Create list for prime numbers
            for (int i = 0; i < arr.Length; ++i)                                             // Check each element of the array
            {
                if (isPrime(arr[i]))                                                        // Call the method IsPrime, checking whether a number is prime
                {
                    cnt++;
                    p.Add(arr[i]);
                }
            }
            Console.WriteLine(cnt);
            for (int i = 0; i < p.Count; ++i)
            {
                Console.Write(p[i] + " ");
            }

            Console.ReadKey();


        }
        static bool isPrime(int a)
        {                                                                         // Create a function to check if a number is prime
            if (a == 1)
            {
                return false;
            }
            for (int i = 2; i <= Math.Sqrt(a); i++)
            {
                if (a % i == 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
