using System;

namespace Algorithms.Binom
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = 5;
            int k = 2;
            Console.WriteLine(Binom(n, k));
        }

        private static int Binom(int n, int k)
        {
            if(k > n)
            {
                return 0;
            } 
            else if(k == 0 || k == n)
            {
                return 1;
            }
            else
            {
                return Binom(n - 1, k - 1) + Binom(n - 1, k);
            }
        }
    }
}
