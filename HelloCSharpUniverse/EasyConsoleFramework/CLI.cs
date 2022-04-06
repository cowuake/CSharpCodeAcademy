using System;
using System.Collections.Generic;
using System.Linq;
using EasyConsoleFramework.IO;
using EasyConsoleFramework.ExtensionMethods;

namespace EasyConsoleFramework
{
    public sealed class CLI
    {
        private static SortedDictionary<string, string> _commandInfo;

        private static Dictionary<string, Action> _commandAction;

        public string ApplicationName { get; private set; } = null;

        public CLI()
        {
            _commandInfo = new SortedDictionary<string, string>();
            _commandAction = new Dictionary<string, Action>();

            AddAction("H", "Show menu entries", ShowMenu);
            AddAction("Q", "Exit program", ExitProgram);
        }

        public void AddAction(string command, string name, Action action)
        {
            _commandInfo.Add(command, name);
            _commandAction.Add(command, action);
        }

        public void SetApplicationName(string name)
        {
            ApplicationName = name;
        }

        private void ShowMenu()
        {
            Console.WriteLine();
            Console.WriteLine(_commandInfo.ToFormattedString());
        }

        public void ExitProgram()
        {
            Environment.Exit(0);
        }

        public void Run()
        {
            if (ApplicationName != null)
            {
                string welcomeString = "    Welcome to ";
                string line = new string('-', welcomeString.Length + ApplicationName.Length + 4);

                Console.WriteLine(line);
                Console.Write(welcomeString);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{ApplicationName}");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(line);
            }

            ShowMenu();

            do
            {
                // Read command from console
                string input = BaseIO.ReadFromConsole(
                    "Please choose a valid option: ",
                    s => _commandInfo.Keys.Contains(s.ToUpper()));

                // Match the _commandInfo key
                input = input.ToUpper();

                // Launch the command
                _commandAction[input]();
            } while (true);
        }
    }
}