using Library.Core.Entities;
using Library.Core.Interface;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Library.Core.BusinessLogic
{
    public class MainBusinessLogic : IMainBusinessLogic
    {
        private readonly IBookRepository _bookRepository;
        private readonly IBookGenreRepository _bookGenreRepository;
        private readonly IAccountRepository _accountRepository;
        private const string HashAlg = "SHA256";

        // Constructor
        public MainBusinessLogic(
            IBookRepository bookRepository,
            IBookGenreRepository bookGenreRepository,
            IAccountRepository accountRepository
            )
        {
            _bookRepository = bookRepository;
            _bookGenreRepository = bookGenreRepository;
            _accountRepository = accountRepository;
        }

        public Result AddBook(Book book)
        {
            if (book == null)
                return new Result(false, "Invalid book data");

            var result = _bookRepository.Add(book);

            //if (result)
            //    return new Result();

            //return new Result(false, "Cannot add book");

            return new Result(result, result ? null : "Cannot add book");
        }

        public Result AddBookGenre(BookGenre genre)
        {
            if (genre == null)
                return new Result(false, "Invalid genre data");

            var result = _bookGenreRepository.Add(genre);

            return new Result(result, result ? null : "Cannot add genre");
        }

        public IList<BookGenre> GetAllBookGenres(Func<BookGenre, bool> filter = null)
            => _bookGenreRepository.GetAll(filter) as IList<BookGenre>;

        public IList<Book> GetAllBooks(Func<Book, bool> filter = null)
            => _bookRepository.GetAll(filter) as IList<Book>;

        public Book GetBook(string isbn)
            => _bookRepository.Get(isbn);

        public BookGenre GetBookGenre(int id)
            => _bookGenreRepository.Get(id);

        public Account GetAccount(string username)
        {
            if (string.IsNullOrEmpty(username))
                return null;

            return _accountRepository.GetByUsername(username);
        }

        public Result RemoveBook(Book book)
        {
            if (book == null)
                return new Result(false, "Invalid book data");

            var result = _bookRepository.Remove(book);

            return new Result(result, result ? null : "Cannot remove book");
        }

        public Result RemoveBookByISBN(string isbn)
        {
            if (string.IsNullOrEmpty(isbn))
                return new Result(false, "Invalid ISBN");

            var result = _bookRepository.RemoveByKey(isbn);

            return new Result(result, result ? null : "Cannot remove book");
        }

        public Result RemoveBookGenre(BookGenre genre)
        {
            if (genre == null)
                return new Result(false, "Invalid genre data");

            var result = _bookGenreRepository.Remove(genre);

            return new Result(result, result ? null : "Cannot remove genre");
        }

        public Result RemoveBookGenreByID(int id)
        {
            if (id <= 0)
                return new Result(false, "Invalid ID");

            var result = _bookGenreRepository.RemoveByKey(id);

            return new Result(result, result ? null : "Cannot remove genre");
        }

        public Result UpdateBook(Book book)
        {
            if (book == null)
                return new Result(false, "Invalid book data");

            var result = _bookRepository.Update(book);

            return new Result(result, result ? null : "Cannot update book");
        }

        public Result UpdateBookGenre(BookGenre genre)
        {
            if (genre == null)
                return new Result(false, "Invalid genre data");

            var result = _bookGenreRepository.Update(genre);

            return new Result(result, result ? null : "Cannot update category");
        }

        public Result AddAccount(Account account)
        {
            if (account == null)
                return new Result(false, "Invalid account data");

            var result = _accountRepository.Add(account);

            return new Result(result, result ? null : "Cannot add account");
        }

        public Result RegisterAccount(string username, string password)
        {
            var account = new Account
            {
                Username = username,
                Role = Role.User,
                Password = Utils.AccountUtils.Hash(password, HashAlg)
            };

            try
            {
                bool result = _accountRepository.Add(account);

                return new Result(result, result ? null : "Cannot register account");
            }
            catch (Exception ex)
            {
                return new Result(false, "Cannot add account", ex);
            }
        }

        public Result CheckAccount(string username, string password)
        {
            var account = GetAccount(username);

            if (account == null)
                return new Result(false, "Account not found)");

            string hashedPassword = Utils.AccountUtils.Hash(password, HashAlg);

            if (!account.Password.Equals(hashedPassword))
                return new Result(false, "Password for the account does not match");

            return new Result();
        }
    }
}