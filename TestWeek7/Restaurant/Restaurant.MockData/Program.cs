using EasyConsoleFramework;
using EasyConsoleFramework.ExtensionMethods;
using EasyConsoleFramework.Utils;
using Restaurant.Core;
using Restaurant.Core.EF;
using Restaurant.Core.Entities;
using Restaurant.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace Library.MockData
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CLI cli = new CLI();

            cli.SetApplicationName("MOCK LIBRARY DATA");
            cli.AddAction("MOCK", "Create mocked restaurant data", MockRestaurantData);

            cli.Run();
        }

        internal static void MockRestaurantData()
        {
            using (var context = new RestaurantContext())
            {
                List<Dish> mockedDishes = new List<Dish>
                {
                    new Dish
                    {
                        Name = "Beef Enchiladas",
                        Type = DishType.MainCourse,
                        Price = 15.90m
                    },

                    new Dish
                    {
                        Name = "Coconut Shrimp with Mango Dipping Sauce",
                        Type = DishType.MainCourse,
                        Price = 24.90m
                    },

                    new Dish
                    {
                        Name = "Little Gem Salad with Lemon Vinaigrette",
                        Type = DishType.MainCourse,
                        Price = 21.00m
                    },

                    new Dish
                    {
                        Name = "Little Gem Salad with Lemon Vinaigrette",
                        Type = DishType.FirstCourse,
                        Price = 21.00m
                    },

                    new Dish
                    {
                        Name = "Zucchini Carpaccio with Salt-Broiled Shrimp",
                        Type = DishType.FirstCourse,
                        Price = 27.40m
                    },

                    new Dish
                    {
                        Name = "Red Snapper Crudo with Watercress Pesto",
                        Type = DishType.FirstCourse,
                        Price = 23.25m
                    },

                    new Dish
                    {
                        Name = "Duck & Orange Salad",
                        Type = DishType.Starter,
                        Price = 18.00m
                    },

                    new Dish
                    {
                        Name = "Sorta Salmon Niçoise",
                        Type = DishType.Starter,
                        Price = 15.00m
                    },

                    new Dish
                    {
                        Name = "Sticky Mango Prawns",
                        Type = DishType.Starter,
                        Price = 17.15m
                    },

                    new Dish
                    {
                        Name = "Cheesecake",
                        Type = DishType.Dessert,
                        Price = 5.00m
                    },

                    new Dish
                    {
                        Name = "Milkshake",
                        Type = DishType.Dessert,
                        Price = 4.00m
                    },

                    new Dish
                    {
                        Name = "Ice Cream",
                        Type = DishType.Dessert,
                        Price = 3.50m
                    },
                };

                List<Account> mockedAccounts = new List<Account>()
                {
                    new Account
                    {
                        Email = "count.zero@cyberspace.com",
                        Password = AccountUtils.Hash("count.zero", Constants.PREFERRED_HASHING_ALGORITHM),

                        Role = Role.Administrator,
                    },
                    new Account
                    {
                        Email = "r.mura@academy.it",
                        Password = AccountUtils.Hash("r.mura", Constants.PREFERRED_HASHING_ALGORITHM),
                        Role = Role.User,
                    },
                    new Account
                    {
                        Email = "j.carmack@idsoftware.com",
                        Password = AccountUtils.Hash("j.carmack", Constants.PREFERRED_HASHING_ALGORITHM),
                        Role = Role.User,
                    },
                    new Account
                    {
                        Email = "n.n.taleb@blackswan.com",
                        Password = AccountUtils.Hash("n.n.taleb", Constants.PREFERRED_HASHING_ALGORITHM),
                        Role = Role.User,
                    },
                };

                mockedAccounts.ForEach(a =>
                {
                    var alreadyThere = context.Accounts.FirstOrDefault(aa => aa.Password == a.Password);

                    if (alreadyThere == null)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"\tACCOUNT: Adding {a.Email.ToUnderlined()}...");
                        Console.ResetColor();

                        context.Accounts.Add(a);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"\tACCOUNT: {a.Email.ToUnderlined()} already in database.");
                        Console.ResetColor();
                    }
                });

                Console.WriteLine();

                mockedDishes.ForEach(d =>
                {
                    var alreadyThere = context.Dishes.FirstOrDefault(dd => dd.Name == d.Name);

                    if (alreadyThere == null)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"\tDISH: Adding {d.Name.ToUnderlined()}...");
                        Console.ResetColor();

                        context.Dishes.Add(d);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"\tDISH: {d.Name.ToUnderlined()} already in database.");
                        Console.ResetColor();
                    }
                });

                Console.WriteLine();

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
    }
}