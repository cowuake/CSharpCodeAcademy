using System;

namespace GiveMeStackOverflow
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n = 1;

            // My precious!
            GiveMeThatSweetStackOverflow(n);
        }

        internal static void GiveMeThatSweetStackOverflow(int n)
        {
            // See how many times the method has been called
            // and how much of the stack has been consumed so far
            Console.WriteLine(
                $"{n},\tallocated {(float)n * sizeof(int) / 1024 / 1024:F6} + MiB");

            // Recursion is served!
            GiveMeThatSweetStackOverflow(++n);
        }
    }
}