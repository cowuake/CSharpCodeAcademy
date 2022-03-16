using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.BaseLibrary
{
    public interface IMenuItem
    {
        public bool Read(Object source);
        public bool Show();
    }
}
