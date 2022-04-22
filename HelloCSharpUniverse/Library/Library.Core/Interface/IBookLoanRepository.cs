using Library.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core.Interface
{
    public interface IBookLoanRepository : IRepository<BookLoan>
    {

    }

    public struct BookLoanCompositeKey
    {
        public int accountId;
        public string bookIsbn;

        public BookLoanCompositeKey(int accountId, string bookIsbn)
        {
            this.accountId = accountId;
            this.bookIsbn = bookIsbn;
        }
    }
}