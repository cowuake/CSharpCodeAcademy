using System;

namespace Test2.Model
{
    public class ItemOfExpenditure
    {
        public ItemCategory Category { get; }
        public decimal Expense { get; }
        public string Description { get; }
        public LevelOfApproval Approval { get; set; }

        public decimal Refund { get; set; }

        public DateTime Date { get; }

        public ItemOfExpenditure(byte category, decimal expense, DateTime date, string description)
        {
            Category = (ItemCategory)category;
            Date = date;
            Description = description;
            Expense = expense;
            Approval = LevelOfApproval.None;
            Refund = 0;
        }

        public ItemOfExpenditure(byte category, decimal expense)
        {
            Category = (ItemCategory)category;
            Date = DateTime.Now;
            Description = "";
            Expense = expense;
            Approval = LevelOfApproval.None;
            Refund = 0;
        }

        public void AssignApproval()
        {
            switch (Expense)
            {
                case decimal c when c <= 400:
                    Approval = LevelOfApproval.Manager;
                    break;
                case decimal c when c <= 1000:
                    Approval = LevelOfApproval.OperationalManager;
                    break;
                case decimal c when c <= 2500:
                    Approval = LevelOfApproval.CEO;
                    break;
                default:
                    Approval = LevelOfApproval.None;
                    break;
            }
        }

        public void AssignRefund()
        {
            if (Approval != LevelOfApproval.None && Refund == 0)
            {
                switch (Category)
                {
                    case ItemCategory.Travel:
                        Refund += Expense + 50;
                        break;
                    case ItemCategory.Board:
                        Refund += (decimal)0.7 * Expense;
                        break;
                    case ItemCategory.Lodging:
                        Refund += Expense;
                        break;
                    default:
                        Refund += (decimal)0.1 * Expense;
                        break;
                }
            }
        }
    }
}
