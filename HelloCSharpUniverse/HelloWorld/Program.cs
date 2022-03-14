using System;

namespace HelloWorld
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("What's your name?");
            string name = Console.ReadLine();
            //Console.WriteLine("Hello World!");
            Console.WriteLine("Hello, " + name + "!");
        }
    }
}
