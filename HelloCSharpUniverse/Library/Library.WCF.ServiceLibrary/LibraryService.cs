using Library.Core.BusinessLogic;
using Library.Core.EFCore;
using Library.Core.EFCore.Repository;
using Library.Core.Entities;
using Library.Core.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Library.WCF.ServiceLibrary
{
    public class LibraryService : ILibraryService
    {
        private readonly IMainBusinessLogic _logic;

        public LibraryService()
        {
            var serviceProvider = new ServiceCollection()
                .AddTransient<LibraryContext>()
                .AddTransient<IBookRepository, EFCoreBookRepository>()
                .AddTransient<IMainBusinessLogic, MainBusinessLogic>()
                .BuildServiceProvider();

            _logic = serviceProvider.GetService<IMainBusinessLogic>();
        }

        public Book GetBook(string isbn)
            => _logic.GetBook(isbn);

        public IList<Book> GetAllBooks(Func<Book, bool> filter)
            => _logic.GetAllBooks(filter);

        public Result AddBook(Book book)
            => _logic.AddBook(book);

        public Result UpdateBook(Book book)
            => _logic.UpdateBook(book);

        public Result RemoveBook(Book book)
            => _logic.RemoveBook(book);
    }
}