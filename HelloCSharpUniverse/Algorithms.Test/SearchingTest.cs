using Algorithms.Searching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Algorithms.Test
{
    public class SearchingTest
    {
        [Fact]
        public void LinearSearchWorks()
        {
            int max = 10000;

            List<int> list = new List<int>();

            foreach (var _ in Enumerable.Range(1, max))
                list.Add(new Random().Next());

            int idx = new Random().Next(0, list.Count);
            int value = list[idx];

            Assert.Equal(idx, list.MyLinearSearch(value));
        }

        [Fact]
        public void BinarySearchWorks()
        {
            int max = 10000;

            List<int> list = new List<int>();

            foreach (var _ in Enumerable.Range(1, max))
                list.Add(new Random().Next());

            int expected = new Random().Next(0, list.Count);
            int value = list[expected];

            int actual = list.MyBinarySearch(value);

            Assert.Equal(expected, actual);
        }
    }
}