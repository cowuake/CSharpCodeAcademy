using System;
using System.Collections.Generic;
using System.Text;

namespace BankAccount.Model
{
    public class PersonalAccount
    {
        public uint ID { get; }
        public string BankName { get; }
        public decimal Credit { get; }
        public IList<IMovement> Movements { get; }
        public DateTime LastAction { get; }
        public bool Blocked { get; private set; }

        private ushort PIN = (ushort)new Random().Next(10000, 99999);
        private byte attempts = 0;

        public PersonalAccount (uint id, string bankName)
        {
            ID = id;
            BankName = bankName;
            Credit = 0;
            Movements = new List<IMovement>();
            LastAction = DateTime.UtcNow;
            Blocked = false;
            Console.WriteLine($"Your PIN is {PIN}.");
        }

        public bool CheckPin(uint code)
        {
            if (code == PIN)
                return true;

            attempts++;
            
            if (attempts >= 5)
            {
                Blocked = true;
                Console.WriteLine("Enter wrong PIN 5 times. Your bank account has been blocked.");
            }

            return false;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Personal bank account unique ID: {ID}");
            sb.AppendLine($"Your bank: {BankName}");
            sb.AppendLine($"Credit balance: {Credit}");

            return sb.ToString();
        }
    }
}
