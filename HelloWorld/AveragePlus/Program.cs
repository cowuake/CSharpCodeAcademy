using System;

namespace AveragePlus
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Spawn user prompt
            Console.Write("How many number do you want to insert?   ");

            // Store the number of expected inputs
            int nNumbers = int.Parse(Console.ReadLine());

            // Initialize variable for the sum
            int sum = 0;

            // Take inputs and increment the sum
            for (int i = 1; i <= nNumbers; i++)
            {
                Console.Write($"Please insert number {i}:   ");
                sum += int.Parse(Console.ReadLine());           
            }

            // Print output to the console
            Console.WriteLine($"The sum of all numbers is {sum}!");
            Console.WriteLine($"The average of all numbers is {(float)sum / nNumbers}!");p
        }
    }
}
