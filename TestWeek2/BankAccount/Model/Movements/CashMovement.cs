using System;
using System.Collections.Generic;
using System.Text;

namespace BankAccount.Model
{
    public class CashMovement : Movement
    {
        public PersonalAccount Executor { get; }

        public CashMovement(decimal amount, PersonalAccount executor) : base(amount)
        {
            Executor = executor;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(base.ToString());
            sb.AppendLine($"Executor: {Executor.ID}");

            return sb.ToString();
        }
    }
}
