using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures
{
    internal class MyList<T> : IMyList<T>
    {
        private T[] array;
        private const int DEFAULT_INITIAL_CAPACITY = 10;
        private int count;

        public MyList(int initialCapacity)
        {
            this.array = new T[initialCapacity > 0 ? initialCapacity : DEFAULT_INITIAL_CAPACITY];
            this.count = 0;
        }

        public MyList()
        {
            this.array = new T[DEFAULT_INITIAL_CAPACITY];
        }

        public void Add(T value)
        {
            if (count == this.array.Length)
            {
                ExpandCapacity();
            }
            this.array[count++] = value;
        }

        public void Add(T[] array)
        {
            foreach (T value in array)
            {
                Add(value);
            }
        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        private void ExpandCapacity()
        {
            T[] newArray = new T[array.Length + DEFAULT_INITIAL_CAPACITY];

            for (int i = 0; i < array.Length; i++)
            {
                newArray[i] = array[i];
            }

            array = newArray;
        }

        /// <summary>
        /// Returns element at index 'index'
        /// </summary>
        /// <param name="index">0-based index in the list</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public T Get(int index)
        {
            if (index <=0 && index > this.count)
            {
                throw new Exception("Invalid index.");
            }

            return this.array[index];
        }

        public bool IndexOf(T value, out int index)
        {
            index = 0;

            for (int i = 0; i < this.count; i++)
            {
                if (this.array[i].Equals(value)) // Cannot use == for generic type T
                {
                    index = i;
                    return true;
                }
            }

            return false;
        }

        public void Remove(int index)
        {
            if (index <= 0 && index > this.count)
            {
                throw new Exception("Invalid index.");
            }

            for (int i = index; i < this.count - 1; i++)
            {
                this.array[i] = this.array[i + 1];
            }

            this.array[count - 1] = default(T);
            this.count--;
        }
    }
}
