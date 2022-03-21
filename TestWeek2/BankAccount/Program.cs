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
        static void Main(string[] args)
        {
            string basePath;

            if (!GetBasePath(out basePath))
            {
                Console.WriteLine("Impossible to locate a suitable data folder.");
                Environment.Exit(1);
            }

            string dataFilePath = basePath + "/data/restaurant.dat";
            string backupFilePath = basePath + "/data/restaurant.dat.bak";

            Console.WriteLine("Welcome to the AvaNODE bank account system.");

            BankAccountDataContext context = new BankAccountDataContext();

            var mockBankAccounts = new List<PersonalAccount>
            {
                new PersonalAccount(19, "Capital One", 99999.99m),
                new PersonalAccount(10, "JPMorgan Chase"),
                new PersonalAccount(87, "Deutsche Bank"),
                new PersonalAccount(27, "Unicredit")
            };

            mockBankAccounts.ForEach(x => context.Data.Accounts.Add(x));

            string savingOperationOutcome;
            if (!context.Save(backupFilePath, out savingOperationOutcome))
                Console.WriteLine(savingOperationOutcome);

            context.Data.Accounts.Add(new PersonalAccount(99, "ZombieBank", 10000));
            context.Data.Accounts.Clear();

            string loadingOperationOutcome;
            if (!context.Load(backupFilePath, out loadingOperationOutcome))
                Console.WriteLine(loadingOperationOutcome);

            context.Data.Accounts.ForEach(x => InterfaceCLI.PrintAccountAndCreditInfo(x));

            //InterfaceCLI.ReadInput();

            Console.Write("\nPrint any key to exit program... ");
            Console.ReadKey();
        }

        public static bool GetBasePath(out string basePath)
        {
            basePath = null;

            string currentDirectory = Environment.CurrentDirectory;
            string currentDirectoryName = new DirectoryInfo(currentDirectory).Name;

            if (currentDirectoryName == "BankAccount")
            {
                basePath = currentDirectory;
                return true;
            }

            string name = System.IO.Directory.GetParent(
                System.IO.Directory.GetParent(currentDirectory).FullName).Name;

            if (name == "bin")
            {
                basePath = "../../../../";
                return true;
            }

            return false;
        }
    }
}