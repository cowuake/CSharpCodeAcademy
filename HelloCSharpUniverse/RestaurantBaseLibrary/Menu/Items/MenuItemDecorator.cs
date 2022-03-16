using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.BaseLibrary
{
    public abstract class MenuItemDecorator : IMenuItem
    {
        protected MenuItem _item;

        public MenuItemDecorator(MenuItem item) => _item = item;

        public abstract bool Read(Object source);
        public virtual bool Show() => _item.Show();
    }
}
