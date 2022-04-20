using Library.Core.Interface;
using Library.Core.Entities;
using Library.MVC.Helpers;
using Library.MVC.Models.BookGenres;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Library.MVC.Controllers
{
    [Authorize(Roles="Administrator")]
    public class BookGenresController : Controller
    {
        private IMainBusinessLogic _logic;

        public BookGenresController(IMainBusinessLogic logic)
            => _logic = logic;

        public IActionResult Index()
        {
            var genres = _logic.GetAllBookGenres();

            var model = genres.ToEnumerableListBookCategoryViewModel();

            return View(model);
        }

        public IActionResult Create()
        {
            var model = new CreateEditBookGenreViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(CreateEditBookGenreViewModel model)
        {
            if(model == null)
            {
                ModelState.AddModelError(string.Empty, "Errore generating model (model binding fails)");
            }
            else
            {
                bool existing = _logic.GetAllBookGenres(b => b.Name.Equals(model.Name)).Any();

                if (existing)
                    ModelState.AddModelError(string.Empty, $"Esiste già una categoria con nome '{model.Name}'");
            }

            if (ModelState.IsValid)
            {
                BookGenre genre = model.ToBookGenre();

                var result = _logic.AddBookGenre(genre);

                if (!result.Success)
                    ModelState.AddModelError(string.Empty, result.Message);
                else
                    return RedirectToAction("Index");
            }

            return View(model);
        }

        public IActionResult Details(int id)
        {
            if (id <= 0)
                return View();

            var genre = _logic.GetBookGenre(id);

            var books = _logic.GetAllBooks(b => b.BookGenreId == id);

            DetailsBookGenreViewModel model = genre.ToDetailsBookCategoryViewModel(books);

            return View(model);
        }

        public IActionResult Edit(int id)
        {
            if (id <= 0)
                return RedirectToAction(nameof(Index));

            BookGenre genre = _logic.GetBookGenre(id);

            var model = genre.ToCreateEditBookGenreViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(int id, CreateEditBookGenreViewModel model)
        {
            if (model == null)
                ModelState.AddModelError(string.Empty, "Errore generating model (model binding fails)");
            else
                if (id != model.Id)
                    ModelState.AddModelError(string.Empty, "Id values do not match");

            if (ModelState.IsValid)
            {
                BookGenre genre = model.ToBookGenre();

                var result = _logic.UpdateBookGenre(genre);

                if (!result.Success)
                    ModelState.AddModelError(string.Empty, result.Message);
                else
                    return RedirectToAction("Index");
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete()
        {
            return View();
        }
    }
}