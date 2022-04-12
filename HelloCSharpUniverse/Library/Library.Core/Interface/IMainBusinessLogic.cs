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

        bool AddBook(Book book);

        bool UpdateBook(Book book);

        bool RemoveBook(Book book);
    }
}