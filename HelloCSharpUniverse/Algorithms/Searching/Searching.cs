using Algorithms.Sorting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Searching
{
    public static class Searching
    {
        public static int? MyLinearSearch<T>(this IList<T> list, T value) where T : IComparable<T>
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].CompareTo(value) == 0)
                    return i;
            }

            return null;
        }

        public static int MyBinarySearch<T>
            (this IList<T> list, T value, bool sorted = false, int? left = null, int? right = null)
            where T : IComparable<T>
        {
            left ??= 0;
            right ??= list.Count - 1;

            if (!sorted)
                list.BubbleSort();

            if (left == right)
            {
                if (list[left.Value].CompareTo(value) == 0)
                    return left.Value;
                else
                    return -1;
            }
            else
            {
                int pivot = (right.Value - left.Value) / 2;

                if (list[pivot].CompareTo(value) == 0)
                    return pivot;
                else if (list[pivot].CompareTo(value) > 0)
                    return list.MyBinarySearch(value, true, (int)left, pivot - 1);
                else
                    return list.MyBinarySearch(value, true, pivot + 1, (int)right);
            }
        }
    }
}