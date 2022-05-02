using Algorithms.Sorting;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Algorithms.Test
{
    public class SortingTest
    {
        [Fact]
        public void BubbleSortWorks()
        {
            List<int> list = new List<int>();

            foreach (var _ in Enumerable.Range(1, 10000))
                list.Add(new Random().Next());

            List<int> copy = new List<int>(list);

            list.Sort();
            copy.BubbleSort();

            Assert.Equal(list, copy);
        }

        [Fact]
        public void QuickSortWorks()
        {
            List<int> list = new List<int>();

            foreach (var _ in Enumerable.Range(1, 10000))
                list.Add(new Random().Next());

            List<int> copy = new List<int>(list);

            list.Sort();
            copy.QuickSort();

            Assert.Equal(list, copy);
        }
    }
}