using ECommerce.Core.EF.Entities;
using ECommerce.Core.Interface;
using EasyConsoleFramework.IO;
using EasyConsoleFramework.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Core.EF
{
    public class EFCatalogue : ICatalogue
    {
        #region ========================= PUBLIC =========================

        public void ChangeProductCategory()
        {
            using (var context = new DataContext())
            {
                int id = 0;

                BaseIO.ReadFromConsole(
                    "\tProduct ID: ",
                    s => int.TryParse(s, out id) && context.Products.Any(p => p.Id == id));

                Product product = context.Products.Find(id);

                string category = BaseIO.ReadFromConsole(
                    "\tCategory: ",
                    s => !String.IsNullOrEmpty(s) && !String.IsNullOrWhiteSpace(s));

                Category cat = context.Categories.FirstOrDefault(c => c.Name == category);

                if (cat == null)
                {
                    Category newCategory = new Category()
                    {
                        Name = category,
                    };

                    context.Categories.Add(newCategory);

                    product.Category = newCategory;
                }
                else
                {
                    product.Category = cat;
                }

                try
                {
                    context.SaveChanges();
                    Console.WriteLine("Done!");
                    Console.WriteLine();
                }
                catch (Exception ex)
                {
                    ErrorHandling.Catch(ex);
                }
            }
        }

        public void ChangeProductDetails()
        {
            using (var context = new DataContext())
            {
                int id = 0;

                BaseIO.ReadFromConsole(
                    "\tProduct ID: ",
                    s => int.TryParse(s, out id) && context.Products.Any(p => p.Id == id));

                Product product = context.Products.Find(id);

                string[] options = new string[] { "name", "description", "price" };

                string toBeModified = BaseIO.ReadFromConsole(
                    "\tProperty to be changed [Name/Description/Price]: ",
                    s => options.Contains(s.ToLower()));

                toBeModified = toBeModified.ToLower();

                switch (toBeModified)
                {
                    case "name":
                        string name = BaseIO.ReadFromConsole(
                            "\tNew name: ",
                            s => !String.IsNullOrEmpty(s) && !String.IsNullOrWhiteSpace(s));
                        product.Name = name;
                        break;
                    case "description":
                        string description = BaseIO.ReadFromConsole(
                            "\tNew description: ",
                            s => !String.IsNullOrEmpty(s) && !String.IsNullOrWhiteSpace(s));
                        product.Description = description;
                        break;
                    case "price":
                        decimal price = 0;
                        BaseIO.ReadFromConsole(
                            "\tNew price: ",
                            s => decimal.TryParse(s, out price));
                        product.Price = price;
                        break;
                }

                try
                {
                    context.SaveChanges();
                    Console.WriteLine("Done!");
                    Console.WriteLine();
                }
                catch (Exception ex)
                {
                    ErrorHandling.Catch(ex);
                }
            }
        }

        public void ChangeProductVisibility()
        {
            using (var context = new DataContext())
            {
                int id = 0;

                BaseIO.ReadFromConsole(
                    "\tProduct ID: ",
                    s => int.TryParse(s, out id) && context.Products.Any(p => p.Id == id));

                Product product = context.Products.Find(id);

                bool visible = false;

                BaseIO.ReadFromConsole(
                    "\tVisibility {true, false}: ",
                    s => bool.TryParse(s, out visible));

                if (product.Visible != visible)
                    product.Visible = visible;

                try
                {
                    context.SaveChanges();
                    Console.WriteLine("Done!");
                    Console.WriteLine();
                }
                catch (Exception ex)
                {
                    ErrorHandling.Catch(ex);
                }
            }
        }

        public void InsertProduct()
        {
            Console.WriteLine();

            string name = BaseIO.ReadFromConsole(
                "\tName: ",
                s => !String.IsNullOrEmpty(s) && !String.IsNullOrWhiteSpace(s));

            string description = BaseIO.ReadFromConsole(
                "\tDescription: ",
                s => !String.IsNullOrEmpty(s) && !String.IsNullOrWhiteSpace(s));

            string category = BaseIO.ReadFromConsole(
                "\tCategory: ",
                s => !String.IsNullOrEmpty(s) && !String.IsNullOrWhiteSpace(s));

            decimal price = 0;

            BaseIO.ReadFromConsole(
                "\tPrice: ",
                s => Decimal.TryParse(s, out price));

            bool visible = true;

            BaseIO.ReadFromConsole(
                "\tMake visible {true, false}: ",
                s => bool.TryParse(s, out visible));

            using (var context = new DataContext())
            {
                Category cat = context.Categories.FirstOrDefault(c => c.Name == category);

                var product = new Product
                {
                    Name = name,
                    Description = description,
                    Category = cat ?? new Category
                    {
                        Name = category
                    },
                    Visible = visible ? true : false
                };

                try
                {
                    if (cat != null)
                        context.Categories.Add(cat);

                    context.Products.Add(product);
                    context.SaveChanges();

                    Console.WriteLine("Done!");
                    Console.WriteLine();
                }
                catch (Exception ex)
                {
                    ErrorHandling.Catch(ex);
                }
            }
        }

        public void ListProductsFiltered()
        {
            bool byNameFlag = ReadYN("Filter by NAME");
            bool byPriceFlag = ReadYN("Filter by PRICE");
            bool byCategoryFlag = ReadYN("Filter by CATEGORY");

            IQueryable<Product> products;
            List<Product> productList;

            using (var context = new DataContext())
            {
                products = context.Products.AsQueryable();

                if (byNameFlag)
                    products = ProductsByName(products, ReadName());

                if (byPriceFlag)
                    products = ProductsByPrice(products, ReadPriceRange());

                if (byCategoryFlag)
                    products = ProductByCategory(products, ReadCategoryName());

                productList = products.ToList();
            }

            if (productList != null)
                PrintProductList(productList);
            else
                Console.WriteLine("No products found.");
        }

        public void ListProductsFilteredByCategory()
        {
            List<Product> products;

            using (var context = new DataContext())
            {
                products = ProductByCategory(context.Products.AsQueryable(), ReadCategoryName()).ToList();
            }

            if (products != null)
                 PrintProductList(products);
            else
                 Console.WriteLine("No products found.");
        }

        public void ListProductsFilteredByName()
        {
            List<Product> products;

            using (var context = new DataContext())
            {
                products = ProductsByName(context.Products.AsQueryable(), ReadName()).ToList();
            }

            if (products != null)
                PrintProductList(products);
            else
                Console.WriteLine("No products found.");
        }

        public void ListProductsFilteredByPrice()
        {
            List<Product> products;

            using (var context = new DataContext())
            {
                products = ProductsByPrice(context.Products.AsQueryable(), ReadPriceRange()).ToList();
            }
               
            if (products != null)
                PrintProductList(products);
            else
                Console.WriteLine("No products found.");
        }

        public void ListVisibleProducts()
        {
            using (var context = new DataContext())
            {
                if (context.Products.Count() == 0)
                    GenProductMockup();

                var products = context.Products
                    .Include(x => x.Category)
                    .Where(x => x.Visible)
                    .ToList();

                PrintProductList(products);
            }
        }

        public void ListOrdersByDescendingDate()
        {
            using (var context = new DataContext())
            {
                if (!context.Orders.Any())
                    GenOrderMockup();

                var orders = context.Orders
                    .Include(x => x.OrderLines)
                    .ThenInclude(x => x.Product)
                    .OrderByDescending(x => x.Date)
                    .ToList();

                string header = String.Format("{0,5}  {1,-10}{2,10}{3,90}",
                    "Id", "Date", "Total", "Products");

                string line = new string('-', Console.BufferWidth);

                Console.WriteLine();
                Console.WriteLine(line);
                Console.WriteLine(header);
                Console.WriteLine(line);

                foreach (var order in orders)
                {
                    string products = "";

                    foreach (OrderLine ol in order.OrderLines)
                        products += $" [ {ol.Product.Name} x {ol.Quantity} ] ";

                    Console.WriteLine(
                        $"{order.Id,5}  {Convert.ToDateTime(order.Date).ToString("d"),-10}" +
                        $"{order.Total.ToString("F2"),10}{products,90}");
                }

                Console.WriteLine(line);
                Console.WriteLine();
            }
        }

        public void ListTotalSalesByCategory()
        {
            using (var context = new DataContext())
            {
                var categories = context.Categories;
                var orderLines = context.OrderLines;

                string header = $"  {"Category",20}{"N. of sold items",20}{"Money earned",20}";
                string line = new string('-', header.Length);

                Console.WriteLine();
                Console.WriteLine(line);
                Console.WriteLine(header);
                Console.WriteLine(line);

                foreach (Category category in categories.ToList())
                {
                    var query = orderLines
                        .Where(ol => ol.Product.Category.Name == category.Name);

                    int totalSales = query
                        .Sum(ol => ol.Quantity);

                    decimal totalMoney = query
                        .Sum(ol => ol.Order.Total);

                    Console.WriteLine($"  {category.Name,20}{totalSales,20}{totalMoney.ToString("F2"),20}");
                }

                Console.WriteLine(line);
                Console.WriteLine();
            }
        }

        #endregion ========================= PUBLIC =========================

        #region ========================= PRIVATE =========================

        private bool ReadYN(string begin)
        {
            string check = BaseIO.ReadFromConsole(
                $"\t{begin} [Y/N]: ",
                s => s.ToUpper() == "Y" || s.ToUpper() == "N");

            check = check.ToUpper();

            if (check == "Y")
                return true;
            else
                return false;
        }

        private string ReadCategoryName()
        {
            return BaseIO.ReadFromConsole(
                "\tCategory: ",
                s => !String.IsNullOrEmpty(s) && !String.IsNullOrWhiteSpace(s));
        }

        private decimal[] ReadPriceRange()
        {
            decimal minPrice = 0, maxPrice = 0;

            BaseIO.ReadFromConsole(
                "\tMinimum price: ",
                s => Decimal.TryParse(s, out minPrice));

            BaseIO.ReadFromConsole(
                "\tMaximum price: ",
                s => Decimal.TryParse(s, out maxPrice));

            return new decimal[] {minPrice, maxPrice};
        }

        private string ReadName()
        {
            return BaseIO.ReadFromConsole(
                "\tName (substring also allowed): ",
                s => !String.IsNullOrEmpty(s) && !String.IsNullOrWhiteSpace(s));
        }

        private IQueryable<Product> ProductByCategory(IQueryable<Product> products, string category)
        {
            return products
                .Include(p => p.Category)
                .Where(p => p.Category.Name == category);
        }

        private IQueryable<Product> ProductsByName(IQueryable<Product> products, string substring)
        {
            return products
                .Include(p => p.Category)
                .Where(p => p.Name.Contains(substring));
        }

        private IQueryable<Product> ProductsByPrice(IQueryable<Product> products, decimal[] priceRange)
        {
            return products
                .Include(p => p.Category)
                .Where(p => p.Price >= priceRange[0] && p.Price <= priceRange[1]);
        }

        private void PrintProductList(List<Product> products)
        {
            string header = String.Format("{0,5}  {1,-30}{2,-20}{3,-50}{4,10}",
                    "Id", "Name", "Category", "Description", "Price");

            string line = new string('-', Console.BufferWidth);

            Console.WriteLine();
            Console.WriteLine(line);
            Console.WriteLine(header);
            Console.WriteLine(line);

            products.ForEach(p =>
                Console.WriteLine(
                    $"{p.Id,5}  {p.Name,-30}{p.Category.Name,-20}{p.Description,-50}{p.Price.ToString("F2"),10}"));

            Console.WriteLine(line);
            Console.WriteLine();
        }

        private void GenProductMockup()
        {
            using (var context = new DataContext())
            {
                context.Categories.Add(new Category()
                {
                    Name = "Generic"
                });

                context.Products.Add(new Entities.Product
                {
                    Name = "HHKB ProClassic",
                    Category = new Entities.Category()
                    {
                        Name = "PC Keyboard"
                    },
                    Description = "3rd-gen HHKB keybord with Topre switches",
                    Price = 249.99m,
                    Visible = true
                });

                context.Products.Add(new Entities.Product
                {
                    Name = "AMD Ryzen 5 Pro 4650U",
                    Category = new Entities.Category()
                    {
                        Name = "Laptop APU"
                    },
                    Description = "Zen2-based APU (6 cores, 12 threads)",
                    Price = 350.00m,
                    Visible = true
                });

                context.Products.Add(new Entities.Product
                {
                    Name = "Pilot Custom 742 SF",
                    Category = new Entities.Category()
                    {
                        Name = "Fountain Pen"
                    },
                    Description = "Japan-crafted pen with 14-kt soft fine nib",
                    Price = 234.90m,
                    Visible = true
                });

                context.Products.Add(new Entities.Product
                {
                    Name = "Pilot Iroshizuku Shin-Kai",
                    Category = new Entities.Category()
                    {
                        Name = "Fountain Pen Ink"
                    },
                    Description = "Blu-black washable fountain pen ink",
                    Price = 23.90m,
                    Visible = true
                });

                context.Products.Add(new Entities.Product
                {
                    Name = "Logitech G403",
                    Category = new Entities.Category()
                    {
                        Name = "PC Mouse"
                    },
                    Description = "Wired gaming mouse with on-the-fly DPI selection",
                    Price = 45.00m,
                    Visible = true
                });

                context.Products.Add(new Entities.Product
                {
                    Name = "Ghost product",
                    Category = new Entities.Category()
                    {
                        Name = "Dummy"
                    },
                    Description = "Something you're not allowed to see :)",
                    Price = 234.90m,
                    Visible = false
                });

                context.SaveChanges();
            }
        }

        private void GenOrderMockup()
        {
            using (var context = new DataContext())
            {
                if (context.Products.Count() == 0)
                    GenProductMockup();

                IList<OrderLine> orderLines1 = new List<OrderLine>()
                {
                    new OrderLine()
                    {
                        Product = context.Products.Find(1),
                        Quantity = 2
                    },
                    new OrderLine()
                    {
                        Product = context.Products.Find(2),
                        Quantity = 3
                    },
                    new OrderLine()
                    {
                        Product = context.Products.Find(3),
                        Quantity = 1
                    }
                };

                context.Orders.Add(new Entities.Order
                {
                    Date = DateTime.Now,
                    OrderLines = orderLines1,
                    Total = orderLines1.Sum(ol => ol.Product.Price * ol.Quantity)
                });

                IList<OrderLine> orderLines2 = new List<OrderLine>()
                {
                    new OrderLine()
                    {
                        Product = context.Products.Find(4),
                        Quantity = 1
                    },
                    new OrderLine()
                    {
                        Product = context.Products.Find(2),
                        Quantity = 1
                    },
                };

                context.Orders.Add(new Entities.Order
                {
                    Date = DateTime.Now,
                    OrderLines = orderLines2,
                    Total = orderLines2.Sum(ol => ol.Product.Price * ol.Quantity)
                });

                IList<OrderLine> orderLines3 = new List<OrderLine>()
                {
                    new OrderLine()
                    {
                        Product = context.Products.Find(1),
                        Quantity = 5
                    },
                    new OrderLine()
                    {
                        Product = context.Products.Find(3),
                        Quantity = 2
                    },
                };

                context.Orders.Add(new Entities.Order
                {
                    Date = DateTime.Now,
                    OrderLines = orderLines3,
                    Total = orderLines3.Sum(ol => ol.Product.Price * ol.Quantity)
                });

                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    ErrorHandling.Catch(ex);
                }
            }
        }

        #endregion ========================= PRIVATE =========================
    }
}