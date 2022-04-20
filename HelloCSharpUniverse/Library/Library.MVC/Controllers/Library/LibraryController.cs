using Library.Core.Entities;
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
                Pages = book.Pages,
                Publisher = book.Publisher,
                Year = book.Year,
                Edition = book.Edition,
                Language = book.Language,
                Note = book.Note,
            };

            return View(model);
        }

        public IActionResult Create()
        {
            var model = new CreateEditBookViewModel();

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(CreateEditBookViewModel model) // We exploit ASP.NET Core's MODEL BINDING
        {
            if (!ModelState.IsValid)
                return View(model);

            bool existing = _logic.GetAllBooks(b =>
                    b.Title == model.Title &&
                    b.Author == model.Author &&
                    b.Publisher == model.Publisher &&
                    b.Language == model.Language &&
                    (b.Year == model.Year || b.Edition == model.Edition)
                ).Any();

            if (existing)
                ModelState.AddModelError(string.Empty, $"A book with provided data already exists.");

            Book book = new Book
            {
                Author = model.Author,
                Title = model.Title,
                ISBN = model.ISBN,
                Summary = model.Summary,
                Pages = model.Pages,
                Publisher = model.Publisher,
                Year = model.Year,
                Edition = model.Edition,
                Language = model.Language,
                Note = model.Note,
            };

            var result = _logic.AddBook(book);
            
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(string isbn)
        {
            if (string.IsNullOrEmpty(isbn))
                return RedirectToAction(nameof(Index));

            Book book = _logic.GetBook(isbn);

            var model = new CreateEditBookViewModel()
            {
                Author = book.Author,
                ISBN = book.ISBN,
                Title = book.Title,
                Summary = book.Summary,
                Pages = book.Pages,
                Edition = book.Edition,
                Year = book.Year,
                Note = book.Note,
                Language = book.Language,
                Publisher = book.Publisher,
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(CreateEditBookViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            Book book = new Book
            {
                Author = model.Author,
                Title = model.Title,
                ISBN = model.ISBN,
                Summary = model.Summary,
                Pages = model.Pages,
                Publisher = model.Publisher,
                Year = model.Year,
                Edition = model.Edition,
                Language = model.Language,
                Note = model.Note,
            };

            var result = _logic.UpdateBook(book);

            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}