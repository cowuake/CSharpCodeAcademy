using Library.Core.Entities;
using Library.Core.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core.EFCore.Repository
{
    public class EFCoreBookRepository : IBookRepository
    {
        public bool Add(Book entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(object key)
        {
            throw new NotImplementedException();
        }

        public Book Get(object key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Book> GetAll(Func<Book, bool> filter = null)
        {
            throw new NotImplementedException();
        }

        public bool Update(Book entity)
        {
            throw new NotImplementedException();
        }
    }
}