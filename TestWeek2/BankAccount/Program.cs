using System;
using BankAccount.Model;
using BankAccount.Services;

namespace BankAccount.CLI
{
    internal class Program
    {
        //public const string DATA_FILE_PATH = "data/rist.bin";

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the AvaNODE bank account system.");

            //IBankAccountDataContext context = new BankAccountDataContext();
            //PersonalAccount account = new PersonalAccount(10, "bank");
            //InterfaceCLI.PrintAccountAndCreditInfo(account);

            //Console.ReadKey();

            InterfaceCLI.ReadInput();
        }
    }
}
