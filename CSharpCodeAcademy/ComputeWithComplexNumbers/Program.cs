using System;
using MainLibrary;
using MathLibrary.Model;

namespace ComputeWithComplexNumbers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*** Computations with complex numbers! ***");
            Console.WriteLine();
            Console.WriteLine("The program will compute (a+bi) [+,-,*,/] (c+di).");
            Console.WriteLine();

            ComplexNumber a, b;
            ReadComplexNumber("Going to read complex number a:", out a);
            ReadComplexNumber("Going to read complex number b: ", out b);



        }

        static void ReadComplexNumber(string header, out ComplexNumber complex)
        {
            
            Console.Write("Please insert the real part");
            double re = InputLib.ReadDoubleFromConsole("Please insert the real part: ");
            double im = InputLib.ReadDoubleFromConsole("Plase insert the imaginary part: ");

            complex = new ComplexNumber(re, im);
        }
    }
}
