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
        {
            _repository = repository;
        }

        public bool AddBook(Book book)
        {
            return _repository.Add(book);
        }

        public IList<Book> GetAllBooks(Func<Book, bool> filter = null)
        {
            return _repository.GetAll(filter) as IList<Book>;
        }

        public Book GetBook(string isbn)
        {
            return _repository.Get(isbn);
        }

        public bool RemoveBook(Book book)
        {
            return _repository.Delete(book);
        }

        public bool UpdateBook(Book book)
        {
            return _repository.Update(book);
        }
    }
}