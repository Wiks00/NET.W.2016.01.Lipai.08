using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task1
{
    public static class FibonacciEnumerator
    {
        /// <summary>
        /// Generate enumeration of fibonacci numbers 
        /// </summary>
        /// <param name="n">count of fibonacci numbers </param>
        /// <returns>fibonacci enumerator</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static IEnumerable<int> CreateFibonacciEnumerator(int n)
        {
            if(n < 0)
                throw new ArgumentOutOfRangeException();

            int x = 1, y = 1;

            for (int i = 0; i < n; i++)
            {
                yield return y;

                y = x + y;
                x = y - x;
            }
        }
    }
}
