using Library.Core.Entities;
using Library.Core.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core.BusinessLogic
{
    public class MainBusinessLogic : IMainBusinessLogic
    {
        private readonly IBookRepository _bookRepository;
        private readonly IBookGenreRepository _bookCategoryRepository;

        // Constructor
        public MainBusinessLogic(IBookRepository bookRepository, IBookGenreRepository bookCategoryRepository)
        {
            _bookRepository = bookRepository;
            _bookCategoryRepository = bookCategoryRepository;
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

        public Result AddBookCategory(BookGenre category)
        {
            if (category == null)
                return new Result(false, "Invalid category data");

            var result = _bookCategoryRepository.Add(category);

            return new Result(result, result ? null : "Cannot add category");
        }

        public IList<BookGenre> GetAllBookCategories(Func<BookGenre, bool> filter = null)
            => _bookCategoryRepository.GetAll(filter) as IList<BookGenre>;

        public IList<Book> GetAllBooks(Func<Book, bool> filter = null)
            => _bookRepository.GetAll(filter) as IList<Book>;

        public Book GetBook(string isbn)
            => _bookRepository.Get(isbn);

        public BookGenre GetBookCategory(int id)
            => _bookCategoryRepository.Get(id);

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

        public Result RemoveBookCategory(BookGenre category)
        {
            if (category == null)
                return new Result(false, "Invalid category data");

            var result = _bookCategoryRepository.Remove(category);

            return new Result(result, result ? null : "Cannot remove category");
        }

        public Result RemoveBookCategoryByID(int id)
        {
            if (id <= 0)
                return new Result(false, "Invalid ID");

            var result = _bookCategoryRepository.RemoveByKey(id);

            return new Result(result, result ? null : "Cannot remove category");
        }

        public Result UpdateBook(Book book)
        {
            if (book == null)
                return new Result(false, "Invalid book data");

            var result = _bookRepository.Update(book);

            return new Result(result, result ? null : "Cannot update book");
        }

        public Result UpdateBookCategory(BookGenre category)
        {
            if (category == null)
                return new Result(false, "Invalid category data");

            var result = _bookCategoryRepository.Update(category);

            return new Result(result, result ? null : "Cannot update category");
        }

        Book IMainBusinessLogic.GetBookCategory(int id)
        {
            throw new NotImplementedException();
        }
    }
}