using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Restaurant.BaseLibrary
{
    public class MenuCLI : MenuDecorator
    {
        public MenuCLI(Menu menu) : base(menu) { }

        public override bool Show()
        {
            Console.WriteLine("Printing menu.");

            _menu.Items.Select(item => item.ToString()).ToList()
                .ForEach(item => Console.WriteLine(item));

            return true;
        }
    }
}
