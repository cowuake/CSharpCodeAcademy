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
        public IList<IMovement> Movements { get; } = new List<IMovement>();
        public DateTime? LastMovement { get; } = null;

        private short PIN { get; set; } = (short)new Random().Next(10000, 99999);
        private byte FailedAccessAttempts { get; set; } = 0;
        public bool Blocked { get; private set; } = false;

        public PersonalAccount(uint id, string bankName)
        {
            ID = id;
            BankName = bankName;

            Console.WriteLine($"Your PIN is {PIN}.");
        }

        public PersonalAccount(uint id, string bankName, decimal credit) : this(id, bankName)
        {
            Credit = credit;
        }

        public bool CheckPin(Func<PersonalAccount, short> reader)
        {
            short code;

            do
            {
                code = reader(this);

                if (code == PIN)
                    return true;

                FailedAccessAttempts++;
            }
            while (FailedAccessAttempts < 5);

            Blocked = true;

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