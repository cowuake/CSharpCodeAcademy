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
            int pivot = (left + right) / 2;
            int temp;

            Func<int, bool> isInRange = (pntr) => (pntr >= left && pntr <= right) ? true : false;

            while (left < right)
            {
                temp = list
                    .Skip(left)
                    .ToList()
                    .FindIndex(x => x.CompareTo(list[pivot]) > 0) - left;

                // 'left' points to first greater element than 'pivot', otherwise 'pivot' itself
                left = isInRange(temp) ? temp : pivot;

                // NOTE: The index will be translated on the left by (pivot + 1)!
                temp = list
                    .Skip(left + 1)
                    .Take(right - left)
                    .ToList()
                    .FindLastIndex(x => x.CompareTo(list[pivot]) < 0) + left + 1;

                // 'right' points to last lesser element than 'pivot', otherwise 'pivot' itself 
                right = isInRange(temp) ? temp : pivot;

                if (left < right)
                {
                    // Swap left and right elements
                    (list[left], list[right]) = (list[right], list[left]);

                    // If 'pivot' was 'right', now it has been swapped with 'left'
                    if (pivot == right)
                        pivot = left;

                    // If 'pivot' was left, now it has been swapped with 'right'
                    if (pivot == left)
                        pivot = right;

                    left++;
                    right--;
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