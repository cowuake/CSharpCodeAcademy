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
            if (n < 0)
                throw new ArgumentOutOfRangeException(nameof(n));

            static BigInteger Aux(BigInteger n, BigInteger acc)
            {
                if (n == 1)
                    return acc;

                return Aux(n - 1, n * acc);
            }

            if (n == 0)
                return 1;

            return Aux(n, 1);
        }

        public static BigInteger Fibonacci(int n)
        {
            if (n < 1)
                throw new ArgumentOutOfRangeException(nameof(n));

            static BigInteger Aux(BigInteger n, BigInteger acc1, BigInteger acc2)
            {
                if (n == 1 || n == 2)
                    return acc2;

                return Aux(n - 1, acc2, acc1 + acc2);
            }

            if (n == 1)
                return 0;

            if (n == 2)
                return 1;

            return Aux(n, 0, 1);
        }
    }
}