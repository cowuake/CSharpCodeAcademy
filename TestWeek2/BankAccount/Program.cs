using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BankAccount.Model;
using BankAccount.Services;

namespace BankAccount.CLI
{
    internal class Program
    {
        public const string BACKUP_DATA_FILE_PATH = "../../../data/restaurant.dat.bak";

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the AvaNODE bank account system.");

            IBankAccountDataContext context = new BankAccountDataContext();

            IList<PersonalAccount> mockBankAccounts = new List<PersonalAccount>
            {
                new PersonalAccount(19, "Capital One"),
                new PersonalAccount(10, "JPMorgan Chase"),
                new PersonalAccount(87, "Deutsche Bank"),
                new PersonalAccount(27, "Unicredit")
            };

            mockBankAccounts.ToList().ForEach(x => InterfaceCLI.PrintAccountAndCreditInfo(x));

            Console.Write("\nPrint any key to exit program... ");
            Console.ReadKey();

            //InterfaceCLI.ReadInput();
        }
    }
}
