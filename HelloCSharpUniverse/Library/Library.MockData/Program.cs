using EasyConsoleFramework;
using EasyConsoleFramework.ExtensionMethods;
using EasyConsoleFramework.Utils;
using Library.Core.EFCore;
using Library.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.MockData
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CLI cli = new CLI();

            cli.SetApplicationName("MOCK LIBRARY DATA");
            cli.AddAction("MOCK", "Create mocked library data", MockLibraryData);

            cli.Run();
        }

        internal static void MockLibraryData()
        {
            using (var context = new LibraryContext())
            {
                List<Book> mocked = new List<Book>
                {
                    new Book
                    {
                        ISBN = "9784062748681",
                        Title = "Noruwei no Mori",
                        Author = "Murakami Haruki",
                        Year = 1987,
                        Publisher = "Kodansha",
                        Edition = 1,
                        Language = "Japanese"
                    },
                    new Book
                    {
                        ISBN = "9788841886922",
                        Title = "Critica della Ragion Pura",
                        Author = "Immanuel Kant",
                        Year = 2013,
                        Publisher = "UTET",
                        Language = "Italian",
                        Pages = 500,
                    },
                    new Book
                    {
                        ISBN = "9788845257100",
                        Author = "Arthur Schopenhauer",
                        Title = "Il Mondo come Volontà e Rappresentazione",
                        Year = 2006,
                        Publisher = "Bompiani",
                        Language = "Italian",
                    },
                    new Book
                    {
                        ISBN = "9788804492733",
                        Author = "Marco Aurelio",
                        Title = "Pensieri",
                        Year = 2001,
                        Publisher = "Mondadori",
                        Edition = 4,
                        Language = "Italian",
                        Summary = "(Roma 121 - Vindobona, odierna Vienna, 180 d.C.) imperatore e filosofo romano. Nato da famiglia di origine spagnola, ebbe presto il favore dell’imperatore Adriano, che ne curò l’educazione con l’aiuto dei maggiori maestri di retorica, grammatica, filosofia e diritto del tempo. La sua adesione allo stoicismo si può far risalire agli anni intorno al 145, quando divenne genero di Antonino Pio, succeduto all’imperatore Adriano nel 138. Divenuto a sua volta imperatore nel 161, M.A. combatté numerose guerre sul confine danubiano e in Asia Minore; durante queste campagne militari scrisse in greco 12 libri di meditazioni (Colloqui con se stesso, più noti come Ricordi), documento principale della sua fama letteraria e filosofica. Nel primo libro, M.A. ricorda con gratitudine la propria famiglia e i propri maestri; nei successivi raccoglie senza alcun ordine sistematico aforismi e riflessioni di vario argomento, spesso concise e oscure, che furono trascritte dal suo diario personale dopo la sua morte. Sono per lo più pensieri ispirati alla tradizione stoica (specialmente a Posidonio), ma rivissuti con una intensità religiosa e morale e stesi in uno stile lapidario che hanno fatto dei Ricordi un breviario di vita contemplativa per i secoli successivi. Di M.A. ci sono giunte anche alcune Lettere giovanili, in latino e in greco, indirizzate al maestro Frontone.",
                    },
                    new Book
                    {
                        ISBN = "978-0812973815",
                        Author = "Nassim Nicholas Taleb",
                        Title = "The Black Swan: The Impact of the Highly Improbable",
                        Year = 2010,
                        Publisher = "Random House Publishing Group",
                        Language = "English",
                        Pages = 480,
                        Edition = 2,
                    },
                    new Book
                    {
                        ISBN = "978-1473217409",
                        Author = "William Gibson",
                        Title = "Count Zero",
                        Year = 2017,
                        Publisher = "Orion",
                        Language = "English",
                        Pages = 320,
                        Edition = 1,
                    },
                    new Book
                    {
                        ISBN = "978-0099595861",
                        Author = "James Hilton",
                        Title = "Lost Horizon",
                        Year = 2015,
                        Publisher = "Vintage Classics",
                        Language = "English",
                        Pages = 224,
                    },
                    new Book
                    {
                        ISBN = "978-0553418026",
                        Author = "Andy Weir",
                        Title = "The Martian: A Novel",
                        Year = 2014,
                        Publisher = "Ballantine Books",
                        Language = "English",
                        Pages = 416,
                    },
                    new Book
                    {
                        ISBN = "978-1473204300",
                        Author = "Dmitry Glukhovsky",
                        Title = "Metro 2034",
                        Year = 2014,
                        Publisher = "Orion",
                        Language = "English",
                        Pages = 320,
                        Edition = 1,
                    },
                    new Book
                    {
                        ISBN = "978-8804671886",
                        Author = "Fëdor Dostoevskij",
                        Title = "L'Idiota",
                        Year = 2016,
                        Publisher = "Mondadori",
                        Language = "Italiano",
                        Pages = 957,
                        Edition = 2,
                    },
                    new Book
                    {
                        ISBN = "978-8804670957",
                        Author = "Jean-Jacques Rousseau",
                        Title = "Emilio",
                        Year = 2017,
                        Publisher = "Mondadori",
                        Language = "Italiano",
                        Pages = 776,
                    },
                };

                mocked.ForEach(b =>
                {
                    var alreadyThere = context.Books.Find(b.ISBN);

                    if (alreadyThere == null)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"\tAdding {b.Title.ToUnderlined()}...");
                        Console.ResetColor();

                        context.Books.Add(b);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"\t{b.Title.ToUnderlined()} already in database.");
                        Console.ResetColor();
                    }
                });

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