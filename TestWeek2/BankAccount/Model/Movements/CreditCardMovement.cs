using System;
using System.Collections.Generic;
using System.Text;

namespace BankAccount.Model
{
    public class CreditCardMovement : Movement
    {
        public uint CardNumber { get; }

        public CreditCardMovement(decimal amount, uint cardNumber) : base(amount)
        {
            CardNumber = cardNumber;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(base.ToString());
            sb.AppendLine($"Card number:  {CardNumber}");
            sb.AppendLine($"Receiving bank: {CardNumber}");

            return sb.ToString();
        }
    }

    public enum CreditCardType : byte
    {
        AMEX,

        VISA,

        MASTERCARD,

        OTHER
    }
}
