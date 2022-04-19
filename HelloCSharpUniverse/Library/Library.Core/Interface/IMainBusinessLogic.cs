using Library.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core.Interface
{
    public interface IMainBusinessLogic
    {
        IList<Book> GetAllBooks(Func<Book, bool> filter = null);

        Book GetBook(string isbn);

        Result AddBook(Book book);

        Result UpdateBook(Book book);

        Result RemoveBook(Book book);

        Result RemoveBookByISBN(string isbn);
    }
}