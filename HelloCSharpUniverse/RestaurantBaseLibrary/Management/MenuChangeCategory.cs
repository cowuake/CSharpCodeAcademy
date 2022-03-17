using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.BaseLibrary
{
    public enum MenuChanges : byte
    {
        NewItem,

        ChangeItem,

        RemoveItem,

        NewCategory,

        ChangeCategory,

        RemoveCategory
    }
}
