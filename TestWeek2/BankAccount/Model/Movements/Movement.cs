using System;
using System.Collections.Generic;
using System.Text;

namespace BankAccount.Model
{
    public abstract class Movement : IMovement
    {
        public decimal Amount { get; }
        public DateTime Date { get; }

        public Movement(decimal amount)
        {
            Amount = amount;
            Date = DateTime.UtcNow;
        }

        // Constructor overload with DateTime

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Amount transferred: {Amount}");
            sb.AppendLine($"Movement execution date: {Date.ToLocalTime():d}");

            return sb.ToString();
        }
    }
}
