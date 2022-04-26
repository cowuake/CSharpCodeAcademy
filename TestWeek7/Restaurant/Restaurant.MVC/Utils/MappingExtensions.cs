using Restaurant.Core.Entities;
using Restaurant.MVC.Models.Account;
using Restaurant.MVC.Models.Menu;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Restaurant.MVC.Utils
{
    public static class MappingExtensions
    {
        public static IEnumerable<MenuViewModel> ToEnumerableMenuViewModel(this IEnumerable<Dish> dishes)
        {
            return dishes.Select(dish => new MenuViewModel
            {
                ID = dish.ID,
                Name = dish.Name,
                Type = dish.Type.ToString(),
                Price = dish.Price,
            });
        }

        public static DishDetailsViewModel ToDishDetailsViewModel(this Dish dish)
        {
            return new DishDetailsViewModel
            {
                ID = dish.ID,
                Name = dish.Name,
                Type = dish.Type.ToString(),
                Price = dish.Price,
            };
        }

        public static Dish ToDish(this AddEditDishViewModel dish)
        {
            return new Dish
            {
                Name = dish.Name,
                Type = dish.Type.ToDishType() ?? DishType.Unassigned,
                Price = dish.Price,
            };
        }

        public static AddEditDishViewModel ToAddEditDishViewModel(this Dish dish)
        {
            return new AddEditDishViewModel
            {
                ID = dish.ID,
                Name = dish.Name,
                Type = dish.Type.ToString(),
                Price = dish.Price,
            };
        }

        public static Account ToUserAccount(this RegisterAccountViewModel account)
        {
            return new Account
            {
                Email = account.Email,
                Password = account.Password,
                Role = Role.User,
            };
        }

        private static DishType? ToDishType(this string key)
        {
            switch (key)
            {
                case "Starter":
                    return DishType.Starter;
                    break;
                case "FirstCourse":
                    return DishType.FirstCourse;
                    break;
                case "MainCourse":
                    return DishType.MainCourse;
                    break;
                case "SideDish":
                    return DishType.SideDish;
                case "Dessert":
                    return DishType.Dessert;
                    break;
                default:
                    return null;
                    break;
            }
        }
    }
}