using Library.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core.Interface
{
    public interface IMainBusinessLogic
    {
        #region ========================= BOOKS =========================

        IList<Book> GetAllBooks(Func<Book, bool> filter = null);

        Book GetBook(string isbn);

        Result AddBook(Book book);

        Result UpdateBook(Book book);

        Result RemoveBook(Book book);

        Result RemoveBookByISBN(string isbn);

        #endregion ========================= BOOKS =========================

        #region ========================= BOOK GENRES =========================

        IList<BookGenre> GetAllBookGenres(Func<BookGenre, bool> filter = null);

        BookGenre GetBookGenre(int id);

        Result AddBookGenre(BookGenre genre);

        Result UpdateBookGenre(BookGenre genre);

        Result RemoveBookGenre(BookGenre genre);

        Result RemoveBookGenreByID(int id);

        #endregion ========================= BOOK GENRES =========================

        #region ========================= ACCOUNTS =========================

        Account GetUser(string username);

        //Result CheckLogin(string username, string passoword);

        #endregion ========================= ACCOUNT =========================
    }
}