﻿using EasyConsoleFramework;
using System;

namespace Algorithms.Check
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CLI cli = new CLI();

            cli.AddAction("FACT", "Compute the factorial of a number", () => Catalog.Factorial());
            cli.AddAction("FIB", "Compute n-th Fibonacci number", () => Catalog.Fibonacci());

            cli.SetApplicationName("ALGORITHMS: MANUAL CHECK");

            cli.Run();
        }
    }
}