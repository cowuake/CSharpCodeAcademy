﻿using System;
using System.Collections.Generic;
using System.Linq;
using EasyConsoleFramework.IO;
using EasyConsoleFramework.ExtensionMethods;
using EasyConsoleFramework.Utils;
using System.Threading.Tasks;

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

        public void AddAction(string command, string description, Action action)
        {
            _commandInfo.Add(command, description);
            _commandAction.Add(command, action);
        }

        public void SetApplicationName(string name)
        {
            ApplicationName = name;
        }

        private void ShowMenu()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(_commandInfo.ToFormattedString());
            Console.ResetColor();
        }

        public void ExitProgram()
        {
            Environment.Exit(0);
        }

        public void Run()
        {
            try
            {
                if (ApplicationName != null)
                {
                    string welcomeString = "    Welcome to ";
                    string line = new string('-', welcomeString.Length + ApplicationName.Length + 4);

                    Console.WriteLine(line);
                    Console.Write(welcomeString);
                    Console.ForegroundColor = ConsoleColor.Green;

                    Console.WriteLine($"{ApplicationName.ToItalic().ToBold()}");

                    Console.ResetColor();
                    Console.WriteLine(line);
                }

                ShowMenu();
            }
            catch (Exception ex)
            {
                ErrorHandling.Catch(ex);
                ExitProgram();
            }

            do
            {
                // Read command from console
                string input = BaseIO.ReadFromConsole(
                    "Please choose a valid option: ",
                    s => _commandInfo.Keys.Contains(s.ToUpper()));

                // Match the _commandInfo key
                input = input.ToUpper();

                // Launch the command
                try
                {
                    _commandAction[input]();
                    //await Task.Run(() => _commandAction[input]());
                }
                catch (Exception e)
                {
                    ErrorHandling.Catch(e);
                }
            } while (true);
        }
    }
}