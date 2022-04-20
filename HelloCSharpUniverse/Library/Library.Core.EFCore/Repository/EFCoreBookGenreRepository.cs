using Library.Core.Entities;
using Library.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Core.EFCore.Repository
{
    public class EFCoreBookGenreRepository : IBookGenreRepository
    {
        private readonly LibraryContext _context;

        public EFCoreBookGenreRepository(LibraryContext context)
        {
            // Context must be injected
            _context = context;
        }

        public bool Add(BookGenre category)
        {
            try
            {
                _context.BookGenres.Add(category);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoveByKey(object id)
        {
            var category = _context.BookGenres.Find(id);

            try
            {
                _context.BookGenres.Remove(category);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Remove(BookGenre category)
        {
            try
            {
                _context.BookGenres.Remove(category as BookGenre);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public BookGenre Get(object id)
        {
            if ((int)id <= 0)
                return null;

            try
            {
                return _context.BookGenres.Find(id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<BookGenre> GetAll(Func<BookGenre, bool> filter = null)
        {
            try
            {
                if (filter != null)
                    return _context.BookGenres.Where(filter).ToList();

                return _context.BookGenres.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool Update(BookGenre category)
        {
            if (category == null)
                return false;

            try
            {
                _context.Update(category); // This is good if working in disconnected mode

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