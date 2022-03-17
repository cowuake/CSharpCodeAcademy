using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.BaseLibrary
{
    public interface IDataContext
    {
        bool Save(out string msg);

        bool Load(out string msg);
    }
}