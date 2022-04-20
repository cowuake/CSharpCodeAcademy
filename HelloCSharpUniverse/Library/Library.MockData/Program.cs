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
                List<BookGenre> mockedGenres = new List<BookGenre>
                {
                    new BookGenre
                    {
                        Family = "Fiction",
                        Name = "Action and adventure",
                    },
                    new BookGenre
                    {
                        Family = "Fiction",
                        Name = "Alternate history",
                    },
                    new BookGenre
                    {
                        Family = "Fiction",
                        Name = "Anthology",
                    },
                    new BookGenre
                    {
                        Family = "Fiction",
                        Name = "Chick lit",
                    },
                    new BookGenre
                    {
                        Family = "Fiction",
                        Name = "Children's",
                    },
                    new BookGenre
                    {
                        Family = "Fiction",
                        Name = "Classic",
                    },
                    new BookGenre
                    {
                        Family = "Fiction",
                        Name = "Comic book",
                    },
                    new BookGenre
                    {
                        Family = "Fiction",
                        Name = "Coming-of-age",
                    },
                    new BookGenre
                    {
                        Family = "Fiction",
                        Name = "Crime",
                    },
                    new BookGenre
                    {
                        Family = "Fiction",
                        Name = "Drama",
                    },
                    new BookGenre
                    {
                        Family = "Fiction",
                        Name = "Fairytale",
                    },
                    new BookGenre
                    {
                        Family = "Fiction",
                        Name = "Fantasy",
                    },
                    new BookGenre
                    {
                        Family = "Fiction",
                        Name = "Graphic novel",
                    },
                    new BookGenre
                    {
                        Family = "Fiction",
                        Name = "Historical fiction",
                    },
                    new BookGenre
                    {
                        Family = "Fiction",
                        Name = "Horror",
                    },
                    new BookGenre
                    {
                        Family = "Fiction",
                        Name = "Mystery",
                    },
                    new BookGenre
                    {
                        Family = "Fiction",
                        Name = "Paranormal romance",
                    },
                    new BookGenre
                    {
                        Family = "Fiction",
                        Name = "Picture book",
                    },
                    new BookGenre
                    {
                        Family = "Fiction",
                        Name = "Poetry",
                    },
                    new BookGenre
                    {
                        Family = "Fiction",
                        Name = "Political thriller",
                    },
                    new BookGenre
                    {
                        Family = "Fiction",
                        Name = "Romance",
                    },
                    new BookGenre
                    {
                        Family = "Fiction",
                        Name = "Satire",
                    },
                    new BookGenre
                    {
                        Family = "Fiction",
                        Name = "Science fiction",
                    },
                    new BookGenre
                    {
                        Family = "Fiction",
                        Name = "Short story",
                    },
                    new BookGenre
                    {
                        Family = "Fiction",
                        Name = "Suspense",
                    },
                    new BookGenre
                    {
                        Family = "Fiction",
                        Name = "Thriller",
                    },
                    new BookGenre
                    {
                        Family = "Fiction",
                        Name = "Western",
                    },
                    new BookGenre
                    {
                        Family = "Fiction",
                        Name = "Western",
                    },
                    new BookGenre
                    {
                        Family = "Nonfiction",
                        Name = "Art/architecture",
                    },
                    new BookGenre
                    {
                        Family = "Nonfiction",
                        Name = "Autobiography",
                    },
                    new BookGenre
                    {
                        Family = "Nonfiction",
                        Name = "Biography",
                    },
                    new BookGenre
                    {
                        Family = "Nonfiction",
                        Name = "Business/economics",
                    },
                    new BookGenre
                    {
                        Family = "Nonfiction",
                        Name = "Crafts/hobbies",
                    },
                    new BookGenre
                    {
                        Family = "Nonfiction",
                        Name = "Cookbook",
                    },
                    new BookGenre
                    {
                        Family = "Nonfiction",
                        Name = "Diary",
                    },
                    new BookGenre
                    {
                        Family = "Nonfiction",
                        Name = "Dictionary",
                    },
                    new BookGenre
                    {
                        Family = "Nonfiction",
                        Name = "Encyclopedia",
                    },
                    new BookGenre
                    {
                        Family = "Nonfiction",
                        Name = "Guide",
                    },
                    new BookGenre
                    {
                        Family = "Nonfiction",
                        Name = "Health/fitness",
                    },
                    new BookGenre
                    {
                        Family = "Nonfiction",
                        Name = "History",
                    },
                    new BookGenre
                    {
                        Family = "Nonfiction",
                        Name = "Home and garden",
                    },
                    new BookGenre
                    {
                        Family = "Nonfiction",
                        Name = "Humor",
                    },
                    new BookGenre
                    {
                        Family = "Nonfiction",
                        Name = "Journal",
                    },
                    new BookGenre
                    {
                        Family = "Nonfiction",
                        Name = "Math",
                    },
                    new BookGenre
                    {
                        Family = "Nonfiction",
                        Name = "Memoir",
                    },
                    new BookGenre
                    {
                        Family = "Nonfiction",
                        Name = "Philosophy",
                    },
                    new BookGenre
                    {
                        Family = "Nonfiction",
                        Name = "Prayer",
                    },
                    new BookGenre
                    {
                        Family = "Nonfiction",
                        Name = "Religion, spirituality, and new age",
                    },
                    new BookGenre
                    {
                        Family = "Nonfiction",
                        Name = "Textbook",
                    },
                    new BookGenre
                    {
                        Family = "Nonfiction",
                        Name = "True crime",
                    },
                    new BookGenre
                    {
                        Family = "Nonfiction",
                        Name = "Review",
                    },
                    new BookGenre
                    {
                        Family = "Nonfiction",
                        Name = "Science",
                    },
                    new BookGenre
                    {
                        Family = "Nonfiction",
                        Name = "Self help",
                    },
                    new BookGenre
                    {
                        Family = "Nonfiction",
                        Name = "Sports and leisure",
                    },
                    new BookGenre
                    {
                        Family = "Nonfiction",
                        Name = "Travel",
                    },
                    new BookGenre
                    {
                        Family = "Nonfiction",
                        Name = "True crime",
                    },
                };

                List<Book> mockedBooks = new List<Book>
                {
                    new Book
                    {
                        ISBN = "9784062748681",
                        Title = "Noruwei no Mori",
                        Author = "Murakami Haruki",
                        BookGenre = mockedGenres.Find(c => c.Name == "Romance"),
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
                        BookGenre = mockedGenres.Find(c => c.Name == "Philosophy"),
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
                        BookGenre = mockedGenres.Find(c => c.Name == "Philosophy"),
                        Year = 2006,
                        Publisher = "Bompiani",
                        Language = "Italian",
                    },
                    new Book
                    {
                        ISBN = "9788804492733",
                        Author = "Marco Aurelio",
                        Title = "Pensieri",
                        BookGenre = mockedGenres.Find(c => c.Name == "Philosophy"),
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
                        BookGenre = mockedGenres.Find(c => c.Name == "Business/economics"),
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
                        BookGenre = mockedGenres.Find(c => c.Name == "Science fiction"),
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
                        BookGenre = mockedGenres.Find(c => c.Name == "Action and adventure"),
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
                        BookGenre = mockedGenres.Find(c => c.Name == "Science fiction"),
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
                        BookGenre = mockedGenres.Find(c => c.Name == "Alternate history"),
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
                        BookGenre = mockedGenres.Find(c => c.Name == "Drama"),
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
                        BookGenre = mockedGenres.Find(c => c.Name == "Philosophy"),
                        Year = 2017,
                        Publisher = "Mondadori",
                        Language = "Italiano",
                        Pages = 776,
                    },
                };

                List<User> mockedUsers = new List<User>()
                {
                    new User
                    {
                        Username = "count.zero",
                        Password = "count.zero"
                    },
                    new User
                    {
                        Username = "r.mura",
                        Password = "r.mura"
                    },
                };

                mockedUsers.ForEach(u =>
                {
                    var alreadyThere = context.Users.FirstOrDefault(uu => uu.Username == u.Username);

                    if (alreadyThere == null)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"\tUSER: Adding {u.Username.ToUnderlined()}...");
                        Console.ResetColor();

                        context.Users.Add(u);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"\tUSER: {u.Username.ToUnderlined()} already in database.");
                        Console.ResetColor();
                    }
                });

                Console.WriteLine();

                mockedGenres.ForEach(g =>
                {
                    var alreadyThere = context.BookGenres.FirstOrDefault(gg => gg.Name == g.Name);

                    if (alreadyThere == null)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"\tGENRE: Adding {g.Name.ToUnderlined()}...");
                        Console.ResetColor();

                        context.BookGenres.Add(g);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"\tGENRE: {g.Name.ToUnderlined()} already in database.");
                        Console.ResetColor();
                    }
                });

                Console.WriteLine();

                mockedBooks.ForEach(b =>
                {
                    var alreadyThere = context.Books.Find(b.ISBN);

                    if (alreadyThere == null)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"\tBOOK: Adding {b.Title.ToUnderlined()}...");
                        Console.ResetColor();

                        context.Books.Add(b);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"\tBOOK: {b.Title.ToUnderlined()} already in database.");
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