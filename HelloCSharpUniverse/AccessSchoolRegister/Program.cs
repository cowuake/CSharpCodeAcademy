using System;
using System.Collections.Generic;
using AccessSchoolRegister.Model;

namespace AccessSchoolRegister
{
    internal class Program
    {
        private static SchoolRegister register; // Must be accessible by state handlers

        static void Main(string[] args)
        {
            Console.WriteLine("#################################");
            Console.WriteLine("######   SCHOOL REGISTER   ######");
            Console.WriteLine("#################################");

            register = new SchoolRegister();

            var CommandInfo = new Dictionary<string, string>
            {
                { "AS", "Add new student to register" },
                { "DS", "Delete student from register" },
                { "SS", "Search student in register" },
                { "L",  "List all students in register" },
                { "H",  "Print this help to screen" },
                { "I",  "Insert test results for a student" },
                { "D",  "Delete test results for a student" },
                { "S",  "Save changes" },
                { "Q",  "Quit the program" },
            };

            Action help = () => ShowHelp(CommandInfo);

            var Commands = new Dictionary<string, Action>
            {
                { "AS", () => register.AddStudent() },
                { "DS", () => register.DeleteStudent() },
                { "SS", () => register.SearchStudent() },
                { "L",  () => register.ListStudents() },
                { "H",  () => help() },
                { "I",  () => register.InsertTestResults() },
                { "D",  () => register.DeleteTestResults() },
                { "S",  () => register.SaveToASCII() },
                { "Q",  () => Quit() },
            };


            ShowHelp(CommandInfo);
            string input;

            do
            {
                Console.WriteLine();
                Console.Write("Please choose an options (H for help): ");
                input = Console.ReadLine().ToUpper();

                if (input == "H")
                {
                    ShowHelp(CommandInfo);
                } else if (Commands.ContainsKey(input))
                {
                    Console.WriteLine();
                    Commands[input]();
                }
            } while (true);
        }

        private static void ShowHelp(Dictionary<string, string> dict)
        {
            Console.WriteLine();
            Console.WriteLine("AVAILABLE OPTIONS");
            Console.WriteLine("==============================================================");

            foreach (string key in dict.Keys)
            {
                Console.WriteLine($"\t{key}\t=>\t{dict[key]}");
            }
            Console.WriteLine("==============================================================");
        }

        private static void Quit()
        {
            Environment.Exit(0);
        }
    }
}
