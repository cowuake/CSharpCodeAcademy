using System;
using System.Collections.Generic;
using System.Text;

namespace MainLibrary
{
    public class IntArrayLib
    {
        public static void Print(int[] array)
        {
            Console.WriteLine("*** Printing the array ***");
            Console.WriteLine();
            foreach (int element in array)
            {
                Console.Write($" {element}");
            }
        }
        public static int Sum(int[] array)
        {
            int result = 0;

            foreach (int element in array)
            {
                result += element;
            }

            return result;
        }

        public static float Average(int[] array)
        {
            int s = Sum(array);
            return (float) s / array.Length;
        }

        public static int Min(int[] array)
        {
            int min = int.MaxValue;

            foreach (int element in array)
            {
                min = element < min ? element : 0;
            }

            return min;
        }

        public static int Max(int[] array)
        {
            int max = int.MinValue;

            foreach (int element in array)
            {
                max = element > max ? element : 0;
            }

            return max;
        }

        public static void Order(int[] array)
        {
            Array.Sort(array);
        }

        public static bool Calculate(int[] v, out int sum, out int avg, out int min, out int max)
        {
            float _sum = 0;
            min = v[0];
            max = v[0];

            for (int i = 0; i < v.Length; i++)
            {
                _sum += v[i];

                if (v[i] < min)
                    min = v[i];

                if (v[i] > max)
                    max = v[i];
            }

            sum = (int)_sum;
            avg = sum / v.Length;

            return true;
        }
    }
}
