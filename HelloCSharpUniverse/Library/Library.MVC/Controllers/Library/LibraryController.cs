using Library.Core.Entities;
using Library.Core.Interface;
using Library.MVC.Helpers;
using Library.MVC.Models.Library;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            IEnumerable<Book> books = _logic.GetAllBooks();

            IEnumerable<LibraryViewModel> model = books.ToEnumerableLibraryViewModel();

            return View(model);
        }

        public IActionResult BookDetails(string isbn)
        {
            if (string.IsNullOrEmpty(isbn))
                return View();

            var book = _logic.GetBook(isbn);
            var genre = _logic.GetBookGenre((int)book.BookGenreId);

            BookDetailsViewModel model = book.ToBookDetailsViewModel(genre);

            return View(model);
        }

        public IActionResult Create()
        {
            var model = new CreateEditBookViewModel()
            {
                AvailableCategories = GetAvailableCategories(),
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(CreateEditBookViewModel model) // We exploit ASP.NET Core's MODEL BINDING
        {
            if (model == null)
            {
                ModelState.AddModelError(string.Empty, "Model binding failed.");
                model.AvailableCategories = GetAvailableCategories();
                return View(model);
            }

            bool existing = _logic.GetAllBooks(b =>
                    b.Title == model.Title &&
                    b.Author == model.Author &&
                    b.Publisher == model.Publisher &&
                    b.Language == model.Language &&
                    (b.Year == model.Year || b.Edition == model.Edition)
                ).Any();

            if (existing)
                ModelState.AddModelError(string.Empty, $"A book with provided data already exists.");

            if (!ModelState.IsValid)
                return View(model);

            Book book = model.ToBook();

            var result = _logic.AddBook(book);
            
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(string isbn)
        {
            if (string.IsNullOrEmpty(isbn))
                return RedirectToAction(nameof(Index));

            Book book = _logic.GetBook(isbn);

            var model = book.ToCreateEditBookViewModel();

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(string isbn, CreateEditBookViewModel model)
        {
            if (model == null)
            {
                ModelState.AddModelError(string.Empty, "Model binding failed.");
                model.AvailableCategories = GetAvailableCategories();
                return View();
            }

            if (isbn != model.ISBN)
            {
                ModelState.AddModelError(string.Empty, "Not-matching ISBN.");
                model.AvailableCategories = GetAvailableCategories();
                return View();
            }
               
            if (!ModelState.IsValid)
            {
                model.AvailableCategories = GetAvailableCategories();
                return View(model);
            }
                
            Book book = model.ToBook();

            var result = _logic.UpdateBook(book);

            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                model.AvailableCategories = GetAvailableCategories();
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        private IEnumerable<SelectListItem> GetAvailableCategories()
        {
            var categories = _logic.GetAllBookGenres();

            return categories.Select(g => new SelectListItem
            {
                Value = g.Id.ToString(),
                Text = g.Name
            });
        }
    }
}