using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Advanced.Testing
{
    class Program
    {
        /*
        //
        ================================ Case - 15

        //1 2 3 4
        //5 6

        1 2 3 4 5 6
        1 2 3 5 4 6
        1 2 3 5 6 4

        1 2 5 3 4 6
        1 2 5 6 3 4

        1 5 2 3 4 6
        1 5 6 2 3 4

        1 2 5 3 6 4

        1 5 2 6 3 4
        1 5 2 3 6 4

        5 6 1 2 3 4
        5 1 6 2 3 4
        5 1 2 6 3 4
        5 1 2 3 6 4
        5 1 2 3 4 6

        ================================ CASE - 10

        // 1 2 3
        // 4 5
        
        // 1 2 3 4 5
        // 1 2 4 3 5
        // 1 2 4 5 3

        // 1 4 2 3 5
        // 1 4 5 2 3 
        // 1 4 2 5 3
        
        // 4 5 1 2 3
        // 4 1 5 2 3
        // 4 1 2 5 3
        // 4 1 2 3 5
        
         

        //================================== Case - 6

        //1 2 3 4 | 0 1 100 101
        //1 3 2 4 | 0 100 1 101
        //1 3 4 2 | 0 100 101 1
        //3 4 1 2 | 100 101 0 1
        //3 1 4 2 | 100 0 101 1
        //3 1 2 4 | 100 0 1 101
        */
        // ============================================ CASE - 20
        /* 
         * 
         * 1 2 3 4 5 6
         * 1 2 4 3 5 6
         * 1 2 4 5 3 6
         * 1 2 4 5 6 3
         * 
         * 1 4 2 3 5 6
         * 1 4 5 2 3 6
         * 1 4 5 6 2 3
         * 1 4 5 2 6 3
         * 
         * 1 4 2 5 3 6
         * 1 4 2 5 6 3
         * 
         * 4 1 2 3 5 6
         * 4 5 1 2 3 6
         * 4 5 6 1 2 3
         * 
         * 4 1 2 5 3 6
         * 4 1 2 5 6 3
         * 
         * 4 1 5 2 3 6
         * 4 1 5 2 6 3
         * 4 1 5 6 2 3
         * 
         * 4 5 1 6 2 3
         * 4 5 1 2 6 3
         * 
         */


        static List<string> output = new List<string>();

        static void Generate(int index,string[] str, bool[] used, List<string> currentCombo)
        {
            if(index >= str.Length)
            {
                output.Add(string.Join(" ", currentCombo));
            }
            else
            {
                for (int i = index; i < str.Length; i++)
                {
                    if (!used[i])
                    {
                        used[i] = true;
                        currentCombo.Add(str[i]);
                        Generate(index + 1, str, used, currentCombo);
                        currentCombo.RemoveAt(currentCombo.Count - 1);
                        used[i] = false;
                    }
                }
            }
        }

        static List<string> solve(string s)
        {
            output = new List<string>();

            string[] arr = s.Split().ToArray();

            Generate(0, arr, new bool[arr.Length], new List<string>());

            return output;
        }

        static void Main(string[] args)
        {
            int n = 100;
            int m = 100;

            BigInteger bigIntegerN = BigInteger.One;
            BigInteger bigIntegerM = BigInteger.One;


            for (int i = n; i >= 1; i--)
            {
                bigIntegerN *= i;
            }

            for (int i = m; i >= 1; i--)
            {
                bigIntegerM *= i;
            }


        }
    }
}
