using System;
using System.Collections.Generic;
using Test2.Model;
using Test2.Services;

namespace Test2
{
    internal class Program
    {
        private const string sourceFile = @"../../../../files/in/source_ex2.txt";
        private const string targetFile = @"../../../../files/out/processed_expenses.txt";

        static void Main(string[] args)
        {
            IList<ItemOfExpenditure> list = new List<ItemOfExpenditure>();

            try
            {
                Console.WriteLine($"Going to read file {sourceFile} ...");
                bool read = ExpenseIO.Read(sourceFile, out list);

                if (read) {
                    Console.WriteLine("Pending approval for expenses...");

                    foreach (ItemOfExpenditure item in list)
                    {
                        item.AssignApproval();
                        item.AssignRefund();
                    }

                    Console.WriteLine("Refunds assigned to approved expenses.");
                    Console.WriteLine();

                    Console.WriteLine($"Going to write log to {targetFile} ...");
                    bool write = ExpenseIO.Write(list, targetFile);

                    if (write)
                    {
                        Console.WriteLine("Complete!");
                    }
                    else
                    {
                        Console.WriteLine($"Log creation at {targetFile} failed.");
                    }
                } else
                {
                    Console.WriteLine($"Failed to read from source file {sourceFile}!");
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed!");
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine();
            Console.Write("Press any button to exit.");
            Console.ReadKey();
        }
    }
}
