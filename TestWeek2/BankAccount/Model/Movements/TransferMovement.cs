using System;
using System.Collections.Generic;
using System.Text;

namespace BankAccount.Model
{
    public class TransferMovement : Movement
    {
        public string SendingBank { get; }
        public string ReceivingBank { get; }

        public TransferMovement(decimal amount, string sendingBank, string receivingBank) : base(amount)
        {
            SendingBank = sendingBank;
            ReceivingBank = receivingBank;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(base.ToString());
            sb.AppendLine($"Sending bank:  {SendingBank}");
            sb.AppendLine($"Receiving bank: {ReceivingBank}");

            return sb.ToString();
        }
    }
}
