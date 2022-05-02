using EasyConsoleFramework.Demo.Models;
using EasyConsoleFramework.ExtensionMethods;
using EasyConsoleFramework.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyConsoleFramework.Demo
{
    internal static class DemoCatalog
    {
        internal static void PrintMockedData(int number = 25)
        {
            IList<StupidEntity> stupidEntities = new List<StupidEntity>();

            foreach (int _ in Enumerable.Range(1, number))
                stupidEntities.Add(new StupidEntity());

            IList<int> columnWidths = new List<int>() { 20, 25, 25, 20, 20 };

            Console.WriteLine(stupidEntities.ToFormattedString(columnWidths, alignment: "left"));
            Console.WriteLine(stupidEntities.ToFormattedString(columnWidths, alignment: "right"));
            Console.WriteLine(stupidEntities.ToFormattedString(columnWidths, alignment: "center"));
        }
    }
}