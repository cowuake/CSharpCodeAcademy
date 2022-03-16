using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.BaseLibrary
{
    public interface ITable
    {
        public uint NCustomer { get; }
        public IOrder Order { get; set; }
    }
}
