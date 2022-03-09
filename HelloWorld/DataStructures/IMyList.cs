using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures
{
    public interface IMyList<T>
    {
        void Add(T value);

        void Add(T[] array);

        void Remove(int index);

        T Get(int index);

        bool IndexOf(T value, out int index);

        int Count();
    }
}
