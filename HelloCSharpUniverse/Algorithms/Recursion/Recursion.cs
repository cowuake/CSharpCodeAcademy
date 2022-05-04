using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Algorithms.Recursion
{
    public static class Classics
    {
        public static BigInteger Factorial(int n)
        {
            static BigInteger Aux(BigInteger n, BigInteger acc)
            {
                if (n == 1)
                    return acc;

                return Aux(n - 1, n * acc);
            }

            if (n <= 2)
                return n;

            return Aux(n, 1);
        }
    }
}