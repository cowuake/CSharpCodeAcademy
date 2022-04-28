using Library.DesktopClient.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.DesktopClient.ViewModels
{
    public class InsertBookViewModel : Notifiable
    {
        private WebApiClient _client;
        private BookModel _bookModel;

        public InsertBookViewModel()
        {
            _bookModel = new BookModel();
            _client = new WebApiClient();
        }


    }
}
