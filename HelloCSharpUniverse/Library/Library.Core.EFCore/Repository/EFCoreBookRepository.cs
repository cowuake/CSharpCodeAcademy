using Library.Core.Entities;
using Library.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Core.EFCore.Repository
{
    public class EFCoreBookRepository : IBookRepository
    {
        private readonly LibraryContext _context;

        public EFCoreBookRepository(LibraryContext context)
        {
            // Context must be injected
            _context = context;
        }

        public bool Add(Book book)
        {
            try
            {
                _context.Books.Add(book);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(object isbn)
        {
            var book = _context.Books.Find(isbn);

            try
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Book Get(object isbn)
        {
            if (string.IsNullOrEmpty(isbn as string))
                return null;

            try
            {
                return _context.Books.Find(isbn);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<Book> GetAll(Func<Book, bool> filter = null)
        {
            try
            {
                if (filter != null)
                    return _context.Books.Where(filter).ToList();

                return _context.Books.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool Update(Book book)
        {
            if (book == null)
                return false;

            try
            {
                _context.Update(book); // This is good if working in disconnected mode
                //_context.Books.Update(book);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}