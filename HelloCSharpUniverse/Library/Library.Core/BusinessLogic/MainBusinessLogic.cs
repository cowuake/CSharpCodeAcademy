using Library.Core.Entities;
using Library.Core.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core.BusinessLogic
{
    public class MainBusinessLogic : IMainBusinessLogic
    {
        private readonly IBookRepository _repository;

        // Constructor
        public MainBusinessLogic(IBookRepository repository)
            => _repository = repository;

        public Result AddBook(Book book)
        {
            if (book == null)
                return new Result(false, "Invalid book data");

            var result = _repository.Add(book);

            //if (result)
            //    return new Result();

            //return new Result(false, "Cannot add book");

            return new Result(result, result ? null : "Cannot add book");
        }

        public IList<Book> GetAllBooks(Func<Book, bool> filter = null)
            => _repository.GetAll(filter) as IList<Book>;

        public Book GetBook(string isbn)
            => _repository.Get(isbn);

        public Result RemoveBook(Book book)
        {
            if (book == null)
                return new Result(false, "Invalid book data");

            var result = _repository.Remove(book);

            return new Result(result, result ? null : "Cannot remove book");
        }

        public Result RemoveBookByISBN(string isbn)
        {
            if (string.IsNullOrEmpty(isbn))
                return new Result(false, "Invalid ISBN");

            var result = _repository.RemoveByKey(isbn);

            return new Result(result, result ? null : "Cannot remove book");
        }

        public Result UpdateBook(Book book)
        {
            if (book == null)
                return new Result(false, "Invalid book data");

            var result = _repository.Update(book);

            return new Result(result, result ? null : "Cannot update book");
        }
    }
}