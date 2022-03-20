using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BankAccount.Model;

namespace BankAccount.Services
{
    public static class InterfaceCLI
    {
        public static void PrintMenu(PersonalAccount account = null)
        {
            Console.WriteLine();
            Console.WriteLine("Available options:");

            if (account == null)
            {
                Console.WriteLine("A - Add new bank account");
            }
            else
            {
                Console.WriteLine("M - Perform movement");
                Console.WriteLine("P - Print account information and movement history");
            }

            Console.WriteLine("Q - Exit from the program");
            Console.WriteLine();
        }

        public static void ReadInput(PersonalAccount account = null)
        {
            PrintMenu(account);
            string[] availableOptions;

            if (account == null)
                availableOptions = new string[] { "a", "q" };
            else
                availableOptions = new string[] { "m", "p", "q" };

            string input = SimpleIO.ReadFromConsole(
                "Insert one of the options above: ",
                s => availableOptions.Contains(s.ToLower())).ToLower();

            switch (input)
            {
                case "a":
                    account = CreateAccount();
                    Console.WriteLine($"");
                    break;
                case "h":
                    PrintMenu();
                    break;
                case "m":
                    RequestMovement(account);
                    break;
                case "p":
                    if (account != null)
                        PrintAccountAndCreditInfo(account);
                    break;
                case "q":
                    System.Environment.Exit(0);
                    break;
            }

            ReadInput(account);
        }

        public static PersonalAccount CreateAccount()
        {
            uint accountID = (uint)DateTime.Now.Millisecond;

            string bankName = SimpleIO.ReadFromConsole(
                "Insert the name of the bank (no symbols, no numbers): ",
                s => !String.IsNullOrEmpty(s) && s.All(c => Char.IsLetter(c)));

            return new PersonalAccount(accountID, bankName);
        }

        public static decimal ReadAmount()
        {
            decimal amount = SimpleIO.ReadUIntFromConsole(
                "Insert the amount of money (with no symbols): ");

            return amount;
        }

        public static short ReadPIN(PersonalAccount account)
        {
            return (short)SimpleIO.ReadIntegerFromConsole("Please insert your PIN: ",
                                                            (n => n >= 100000 && n <= 99999));
        }

        public static void CheckBlocked(PersonalAccount account)
        {
            if (account.Blocked)
            {
                Console.WriteLine("Sorry, your account has been blocked.");
                Console.WriteLine("You will be logged out now.");
                ReadInput(null);
            }
        }

        public static void RequestMovement(PersonalAccount account)
        {
            CheckBlocked(account);

            account.CheckPin(ReadPIN);

            if (account.Blocked)
            {
                Console.WriteLine("Enter wrong PIN 5 times. Your bank account has been blocked.");
                return;
            }

            string[] availableOptions = new string[] { "c", "t", "cc", "q" };

            Console.WriteLine();
            Console.WriteLine("Available options:");
            Console.WriteLine("C - Perform cash movement");
            Console.WriteLine("T - Perform t movement");
            Console.WriteLine("CC - Perform credit card movement");
            Console.WriteLine("Q - Cancel operation");

            string input = SimpleIO.ReadFromConsole(
                "Insert one of the options above: ",
                s => availableOptions.Contains(s.Trim().ToLower())).Trim().ToLower();

            Console.WriteLine();

            decimal amount;

            switch (input)
            {
                case "c":
                    amount = ReadAmount();
                    break;
                case "t":
                    amount = ReadAmount();
                    break;
                case "cc":
                    amount = ReadAmount();
                    if (amount > account.Credit)
                        throw new Exception();
                    break;
                case "q":
                    ReadInput(account);
                    break;
            }
        }

        public static void PrintAccountAndCreditInfo(PersonalAccount account)
        {
            Console.WriteLine(account.ToString());

            if (account.Movements.Count > 0)
            {
                foreach (IMovement movement in account.Movements)
                {
                    Console.WriteLine();
                    Console.WriteLine(movement.ToString());
                }
            }
            else
            {
                Console.WriteLine("No movements to date.");
            }
        }
    }
}
