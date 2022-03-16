using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.BaseLibrary
{
    public class MenuItemCLI : MenuItemDecorator
    {
        public MenuItemCLI(MenuItem item) : base(item) {}

        public override bool Read(Object source)
        {
            if (!(source is string))
                return false;

            source = source as string;

            return false;
        }

        public override bool Show()
        {
            string s = _item.ToString();

            if (s == null)
                return false;

            Console.WriteLine(s);
            return true;
        }
    }
}
