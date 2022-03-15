using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Test2.Model;

namespace Test2.Services
{
    public static class ExpenseIO
    {
        public static bool Read(string path, out IList<ItemOfExpenditure> list)
        {
            list = new List<ItemOfExpenditure>();

            if (!File.Exists(path))
                return false;

            string[] rawItems = File.ReadAllLines(path);

            foreach (string item in rawItems)
            {
                string[] splitted = item.Split(';', StringSplitOptions.RemoveEmptyEntries);

                DateTime date;
                DateTime.TryParse(splitted[0], out date);

                ItemCategory category;
                Enum.TryParse(splitted[1], out category);

                string description = splitted[2];

                decimal expense;
                Decimal.TryParse(splitted[3], out expense);

                list.Add(new ItemOfExpenditure((byte)category, expense, date, description));
            }

            return true;
        }

        public static bool Write(IList<ItemOfExpenditure> expenditureList, string path)
        {
            string directory = Path.GetDirectoryName(path);

            if (!Directory.Exists(directory))
                return false;

            IList<string> approvedStringList = expenditureList.Where(
                x => x.Approval != LevelOfApproval.None).Select(
                    x => $"{x.Date:d};{x.Category};{x.Description};APPROVED;" +
                    $"{x.Approval};{(float)x.Refund:F2}").ToList();

            IList<string> rejectedStringList = expenditureList.Where(
                x => x.Approval == LevelOfApproval.None).Select(
                    x => $"{x.Date:d};{x.Category};{x.Description};REJECTED;-;-").ToList();


            File.WriteAllLines(path, approvedStringList.Concat(rejectedStringList));

            return true;
        }
    }
}
