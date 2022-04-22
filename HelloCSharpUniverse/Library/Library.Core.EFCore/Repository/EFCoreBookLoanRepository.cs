using Library.Core.Entities;
using Library.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Core.EFCore.Repository
{
    public class EFCoreBookLoanRepository : IBookLoanRepository
    {
        private readonly LibraryContext _context;

        public EFCoreBookLoanRepository(LibraryContext context)
        {
            
        }

        public bool Add(BookLoan loan)
        {
            try
            {
                _context.BookLoans.Add(loan);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public BookLoan Get(object key)
        {
            BookLoanCompositeKey compositeKey = (BookLoanCompositeKey)key;

            if (compositeKey.accountId <= 0 || string.IsNullOrEmpty(compositeKey.bookIsbn))
                return null;

            try
            {
                return _context.BookLoans.Find(compositeKey.accountId, compositeKey.bookIsbn);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<BookLoan> GetAll(Func<BookLoan, bool> filter = null)
        {
            try
            {
                if (filter != null)
                    return _context.BookLoans.Where(filter).ToList();

                return _context.BookLoans.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool Remove(BookLoan loan)
        {
            try
            {
                _context.BookLoans.Remove(loan);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoveByKey(object key)
        {
            BookLoanCompositeKey compositeKey = (BookLoanCompositeKey)key;

            var loan = _context.BookLoans.Find(compositeKey.accountId, compositeKey.bookIsbn);

            try
            {
                _context.BookLoans.Remove(loan);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(BookLoan loan)
        {
            if (loan == null)
                return false;

            try
            {
                _context.Update(loan); // This is good if working in disconnected mode
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