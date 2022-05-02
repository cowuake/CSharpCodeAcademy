using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Algorithms.Sorting
{
    public static class Sorting
    {
        public static void BubbleSort<T>(this IList<T> list) where T : IComparable<T>
        {
            bool test = false;

            while (!test)
            {
                test = true;

                for (int i = 0; i < list.Count - 1; i++)
                {
                    if (list[i].CompareTo(list[i + 1]) > 0)
                    {
                        test = false;

                        (list[i], list[i + 1]) = (list[i + 1], list[i]);
                    }
                }
            }
        }

        public static void MergeSort<T>(this IList<T> list) where T : IComparable<T>
        {

        }

        private static int Partition<T>(IList<T> list, int left, int right)
            where T : IComparable<T>
        {
            int pivot = (left + right) / 2 - 1;
            int temp;

            while (left < right)
            {
                temp = list
                    .Skip(left)
                    .Take(pivot - left)
                    .ToList()
                    .FindIndex(x => x.CompareTo(list[pivot]) < 0);

                if (temp >= 0)
                {
                    left = temp;

                    (list[left], list[pivot]) = (list[pivot], list[left]);

                    pivot = left;
                    left++;
                }
                else
                {
                    left = pivot;
                }

                temp = list
                    .Skip(pivot + 2)
                    .Take(right - pivot)
                    .ToList()
                    .FindIndex(x => x.CompareTo(list[pivot]) > 0);

                if (temp >= 0)
                {
                    right = temp;

                    (list[pivot], list[right]) = (list[right], list[pivot]);

                    pivot = right;
                    right--;
                }
                else
                {
                    right = pivot;
                }
            }
            
            return pivot;
        }

        public static void QuickSort<T>(this IList<T> list, int? left = null, int? right = null)
            where T : IComparable<T>
        {
            left ??= 0;
            right ??= list.Count - 1;

            if (left < right)
            {
                int pivot = Partition(list, (int)left, (int)right);

                list.QuickSort(left, pivot - 1);
                list.QuickSort(pivot + 1, right);
            }
        }
    }
}