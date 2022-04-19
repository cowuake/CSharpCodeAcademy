﻿using Library.Core.Entities;
using Library.Core.Interface;
using Library.MVC.Models.Library;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Library.MVC.Controllers
{
    public class LibraryController : Controller
    {
        // Inject business logic
        private IMainBusinessLogic _logic;
        public LibraryController(IMainBusinessLogic logic)
        {
            _logic = logic;
        }

        // ... localhost.../Library/Index
        public IActionResult Index()
        {
            IEnumerable<Book> data = _logic.GetAllBooks();

            IEnumerable<LibraryViewModel> model = data.Select(book => new LibraryViewModel
            {
                Author = book.Author,
                ISBN = book.ISBN,
                Title = book.Title,
            });

            return View(model);
        }

        public IActionResult BookDetails(string isbn)
        {
            var book = _logic.GetBook(isbn);

            BookDetailsViewModel model = new BookDetailsViewModel
            {
                Author = book.Author,
                ISBN = book.ISBN,
                Summary = book.Summary,
                Title = book.Title,
            };

            return View(model);
        }

        public IActionResult InsertBook()
        {
            var model = new InsertBookViewModel();

            return View();
        }

        [HttpPost]
        public IActionResult InsertBook(InsertBookViewModel model) // We exploit ASP.NET Core's MODEL BINDING
        {
            Book book = new Book
            {
                Author = model.Author,
                Title = model.Title,
                ISBN = model.ISBN,
                Summary = model.Summary,
            };

            var result = _logic.AddBook(book);
            
            return RedirectToAction(nameof(Index));
        }
    }
}