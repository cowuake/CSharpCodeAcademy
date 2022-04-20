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

        IList<BookGenre> GetAllBookCategories(Func<BookGenre, bool> filter = null);

        Book GetBookCategory(int id);

        Result AddBookCategory(BookGenre category);

        Result UpdateBookCategory(BookGenre category);

        Result RemoveBookCategory(BookGenre category);

        Result RemoveBookCategoryByID(int id);
    }
}