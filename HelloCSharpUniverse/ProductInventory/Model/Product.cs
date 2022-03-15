using System;
using System.Text;
using System.IO;

namespace ProductInventory.Model
{
    internal class Product
    {
        public string Code { get; }
        public string Description { get; }
        public decimal Price { get; }
        public DateTime Created { get; }

        #region ====================   Ctor   ====================
        public Product() : this(string.Empty, 0) {}

        public Product(string code, decimal price) : this(code, string.Empty, price) {}

        public Product(string code, string description, decimal price)
        {
            Code = code;
            Description = description;
            Price = price;
            Created = DateTime.Now;
        }
        #endregion

        public decimal DiscountedPrice(double discount)
        {
            if (discount < 0)
                discount = 0;

            if (discount > 20)
                discount = 20;

            return Price * (100 - (decimal)discount) / 100.0m;
        }

        public int ProductAge()
        {
            TimeSpan span = DateTime.Now - Created;
            return (int)span.TotalDays;
        }

        public bool SaveProduct()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{Code}:");
            sb.AppendLine($"\t- {Description}");
            sb.AppendLine($"\t- {Price}");
            sb.AppendLine($"\t- {Created}");

            string s = sb.ToString();

            string fileName = $"Product_{Code}.txt";

            // NOTE: 'using' requires the class to implement the IDisposable interface
            //       (FileStream in the present case)
            using (FileStream fs = new FileStream(fileName,
                                                  FileMode.OpenOrCreate,
                                                  FileAccess.Write,
                                                  FileShare.None))
            {
                // NOTE: WriteAllText (instead of a file stremer) would be enough
                //       for the present, simple case
                byte[] data = Encoding.UTF8.GetBytes(s);

                try
                {
                    fs.Write(data, 0, data.Length);
                } catch
                {
                    return false;
                } finally
                {
                    fs.Close();
                }
                
            }

            return true;
        }
    }
}