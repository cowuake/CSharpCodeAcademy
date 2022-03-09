using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;
using System.Diagnostics;

using ExtensionLibrary;

namespace Library
{
    public class StringLibrary
    {
        public static void ToCamelCase(string input, out string noSpace, out string withSpace)
        {
            // Split input string
            //List<string> splitted = input.Split(' ').ToList();
            
            string[] splitted = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            // Remove additional whitespace
            //splitted.RemoveAll(s => String.IsNullOrWhiteSpace(s));

            // Initialize two StringBuilder object for storing intermediate results
            var sb = new StringBuilder();

            // Change first char to upper case for each string
            foreach (string s in splitted)
            {
                sb.Append(s.Substring(0, 1).ToUpper() + s.Substring(1) + " ");
            }

            // Assign to out variables
            withSpace = sb.ToString();
            noSpace = withSpace.Replace(" ", "");
        }

        public static void SplittingBenchmark()
        {
            string source = File.ReadAllText("book.txt");
            string text = source.RepeatString(10);

            //string[] splittedAsArray;
            //List<string> splittedAsList;

            int[] nIterations = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            long[] millisecondsArray = new long[nIterations.Length];
            float[] avgsArray = new float[nIterations.Length];

            long[] millisecondsList = new long[nIterations.Length];
            float[] avgsList = new float[nIterations.Length];

            var watch = new Stopwatch();

            for (int k = 0; k < nIterations.Length; k++)
            {
                int nTimes = nIterations[k];

                Console.WriteLine("===========================================================");
                Console.WriteLine($"\tExecuting {nIterations[k]} times...");
                Console.WriteLine("===========================================================");

                watch.Start();
                for (int i = 0; i < nTimes; i++)
                {    
                    text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                }
                watch.Stop();

                millisecondsArray[k] = watch.ElapsedMilliseconds;
                avgsArray[k] = (float)millisecondsArray[k] / 1000 / nTimes;
                Console.WriteLine($"Execution time (ARRAY):\t\t{millisecondsArray[k]} ms (average {avgsArray[k]:F3} s)");

                watch.Restart();
                for (int i = 0; i < nTimes; i++)
                {
                    text.Split(' ').ToList().Where(s => !String.IsNullOrWhiteSpace(s));
                }
                watch.Stop();

                millisecondsList[k] = watch.ElapsedMilliseconds;
                avgsList[k] = (float)millisecondsList[k] / 1000 / nTimes;
                Console.WriteLine($"Execution time (LIST):\t\t{millisecondsList[k]} ms (average {avgsList[k]:F3} s)");

                Console.WriteLine();
            }
        }
    }
}
