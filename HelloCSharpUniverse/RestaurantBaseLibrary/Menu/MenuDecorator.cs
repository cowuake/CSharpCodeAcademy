using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.BaseLibrary
{
    public abstract class MenuDecorator : IMenu
    {
        protected Menu _menu;

        public MenuDecorator(Menu menu) => _menu = menu;

        public virtual bool Show() => _menu.Show();
    }
}
