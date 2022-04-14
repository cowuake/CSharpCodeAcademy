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

        public MainBusinessLogic(IBookRepository repository)
            => _repository = repository;

        public bool AddBook(Book book)
            => _repository.Add(book);

        public IList<Book> GetAllBooks(Func<Book, bool> filter = null)
            => _repository.GetAll(filter) as IList<Book>;

        public Book GetBook(string isbn)
            => _repository.Get(isbn);

        public bool RemoveBook(Book book)
            => _repository.Remove(book);

        public bool RemoveBookByISBN(string isbn)
            => _repository.RemoveByKey(isbn);

        public bool UpdateBook(Book book)
            => _repository.Update(book);
    }
}