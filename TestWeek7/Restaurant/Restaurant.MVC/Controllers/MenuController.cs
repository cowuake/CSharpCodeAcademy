using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Restaurant.Core.Entities;
using Restaurant.Core.Interface;
using Restaurant.MVC.Models.Menu;
using Restaurant.MVC.Utils;
using System.Collections.Generic;
using System.Linq;

namespace Restaurant.MVC.Controllers
{
    [Authorize]
    public class MenuController : Controller
    {
        private IBusinessLogic _logic;

        public MenuController(IBusinessLogic logic)
        {
            _logic = logic;
        }

        public IActionResult Index()
        {
            IEnumerable<Dish> dishes = _logic.GetAllDishes();

            IEnumerable<MenuViewModel> model = dishes.ToEnumerableMenuViewModel();

            return View(model);
        }

        public IActionResult DishDetails(int id)
        {
            if (id <= 0)
                return View();

            var dish = _logic.GetDish(id);

            DishDetailsViewModel model = dish.ToDishDetailsViewModel();

            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult AddDish()
        {
            var model = new AddEditDishViewModel()
            {
                AllowedDishTypes = GetAllowedDishTypes(),
            };

            return View(model);
        }

        [HttpPost, Authorize(Roles = "Administrator"), ValidateAntiForgeryToken]
        public IActionResult AddDish(AddEditDishViewModel model)
        {
            if (model == null)
            {
                ModelState.AddModelError(string.Empty, "Model binding failed.");
                model.AllowedDishTypes = GetAllowedDishTypes();
                return View(model);
            }

            bool existing = _logic.GetAllDishes(d => d.Name == model.Name).Any();

            if (existing)
                ModelState.AddModelError(string.Empty, $"A dish with the specified name already exists.");

            if (!ModelState.IsValid)
                return View(model);

            Dish dish = model.ToDish();

            var result = _logic.AddDish(dish);

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult EditDish(int id)
        {
            if (id <= 0)
                return RedirectToAction(nameof(Index));

            Dish dish = _logic.GetDish(id);

            var model = dish.ToAddEditDishViewModel();
            model.AllowedDishTypes = GetAllowedDishTypes();

            return View(model);
        }

        [HttpPost, Authorize(Roles = "Administrator"), ValidateAntiForgeryToken]
        public IActionResult EditDish(int id, AddEditDishViewModel model)
        {
            if (model == null)
            {
                ModelState.AddModelError(string.Empty, "Model binding failed.");
                model.AllowedDishTypes = GetAllowedDishTypes();
                return View();
            }

            if (id != model.ID)
            {
                ModelState.AddModelError(string.Empty, "Not-matching ISBN.");
                model.AllowedDishTypes = GetAllowedDishTypes();
                return View();
            }

            if (!ModelState.IsValid)
            {
                model.AllowedDishTypes = GetAllowedDishTypes();
                return View(model);
            }

            Dish dish = model.ToDish();

            var result = _logic.UpdateDish(dish);

            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                model.AllowedDishTypes = GetAllowedDishTypes();
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult DeleteDish(int id)
        {
            if (id <= 0)
                return Json(false);

            return Json(_logic.RemoveDishById(id));
        }

        #region ========================= PRIVATE =========================

        private IEnumerable<SelectListItem> GetAllowedDishTypes()
        {
            List<string> types = new List<string>
            {
                DishType.Starter.ToString(),
                DishType.FirstCourse.ToString(),
                DishType.MainCourse.ToString(),
                DishType.SideDish.ToString(),
                DishType.Dessert.ToString(),
            };

            return types.Select(t => new SelectListItem
            {
                Value = t,
                Text = t
            });
        }

        #endregion ========================= PRIVATE =========================
    }
}