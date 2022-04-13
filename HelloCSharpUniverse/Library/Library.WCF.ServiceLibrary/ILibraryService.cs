using Library.Core.Entities;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Library.WCF.ServiceLibrary
{
    [ServiceContract]
    public interface ILibraryService
    {
        [OperationContract]
        Book GetBook(string isbn);

        [OperationContract]
        IList<Book> GetAllBooks(Func<Book, bool> filter);

        [OperationContract]
        bool AddBook(Book book);

        [OperationContract]
        bool UpdateBook(Book book);

        [OperationContract]
        bool RemoveBook(Book book);
    }
}
