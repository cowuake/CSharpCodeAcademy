using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using TicketingEFCore.EFCore;
using TicketingEFCore.Entities;
using EasyConsoleFramework.IO;
using EasyConsoleFramework.Utils;

namespace TicketingEFCore.Catalogue
{
    public static class CRUD
    {
        public static void ListTickets()
        {
            using (var context = new DataContext())
            {
                var tickets = context.Tickets
                    .Include(x => x.Category)
                    .OrderByDescending(x => x.CreatedDate)
                    .ToList();

                string header = String.Format("{0,3}  {1,-55}{2,-12}{3,-17}{4,-10}{5,-8}",
                    "Id", "Description", "Opened", "Customer", "State", "Category");

                string line = new string('-', Console.BufferWidth);

                Console.WriteLine();
                Console.WriteLine(line);
                Console.WriteLine(header);
                Console.WriteLine(line);

                tickets.ForEach(t =>
                    Console.WriteLine(
                        $"{t.Id,3}  {t.Description,-55}" +
                        $"{Convert.ToDateTime(t.CreatedDate).ToString("MM/dd/yyyy"),-12}" +
                        $"{t.Customer,-17}{t.State,-10}{t.Category.Name,-8}"));

                Console.WriteLine(line);
                Console.WriteLine();
            }
        }

        public static void InsertTicket()
        {
            string description = BaseIO.ReadFromConsole(
                "\tDescription: ",
                s => !String.IsNullOrEmpty(s) && !String.IsNullOrWhiteSpace(s));

            string customer = BaseIO.ReadFromConsole(
                "\tCustomer's name: ",
                s => !String.IsNullOrEmpty(s) && !String.IsNullOrWhiteSpace(s));

            string category = BaseIO.ReadFromConsole(
                "\tCategory: ",
                s => !String.IsNullOrEmpty(s) && !String.IsNullOrWhiteSpace(s));

            using (var context = new DataContext())
            {
                //category = context.Categories.FirstOrDefault(c => c.Name == "NOT ASSIGNED");

                var ticket = new Ticket
                {
                    Description = description,
                    CreatedDate = DateTime.Now,
                    Customer = customer,
                    State = "new",

                    //Category = new Category
                    //{
                    //    Name = category,
                    //}
                };

                try
                {
                    context.Tickets.Add(ticket);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    ErrorHandling.Catch(ex);
                }
            }
        }

        public static void DeleteTicket()
        {
            int id = 0;

            BaseIO.ReadFromConsole(
                "\tTicket Id: ",
                s => int.TryParse(s, out id));

            using (var context = new DataContext())
            {
                var ticket = context.Tickets.Find(id);

                if (ticket != null)
                {
                    context.Tickets.Remove(ticket);

                    try
                    {
                        context.SaveChanges();
                        Console.WriteLine("Done!");
                    }
                    catch (Exception ex)
                    {
                        ErrorHandling.Catch(ex);
                    }
                }
                else
                {
                    Console.WriteLine("Record not found!");
                }
            }
            
            Console.WriteLine();
        }
    }
}