using System;
using System.Collections.Generic;
using System.Linq;
using EasyConsoleFramework.IO;

namespace EasyConsoleFramework
{
    public class CLI
    {
        private static SortedDictionary<string, string> _commandInfo;

        private static SortedDictionary<string, Action> _commandAction;

        public string ApplicationName { get; private set; } = null;

        public CLI()
        {
            _commandInfo = new SortedDictionary<string, string>();
            _commandAction = new SortedDictionary<string, Action>();

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

        public void ShowMenu()
        {
            BaseIO.PrintSortedDictionary(_commandInfo);
        }

        public void ExitProgram()
        {
            Environment.Exit(0);
        }

        public void Run()
        {
            if (ApplicationName != null)
            { 
                string welcomeString = $"    Welcome to {ApplicationName}    ";
                string line = new string('-', welcomeString.Length);

                Console.WriteLine(line);
                Console.WriteLine(welcomeString);
                Console.WriteLine(line);
            }

            ShowMenu();

            do
            {
                string input = BaseIO.ReadFromConsole(
                    "Please choose a valid option: ",
                    s => _commandInfo.Keys.Contains(s.ToUpper()));

                // Match the _commandInfo key
                input = input.ToUpper();

                // Launch the command
                _commandAction[input]();

                Console.WriteLine();
            } while (true);
        }
    }
}