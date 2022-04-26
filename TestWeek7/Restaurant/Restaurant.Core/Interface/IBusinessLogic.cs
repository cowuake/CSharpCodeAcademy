using Restaurant.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Core.Interface
{
    public interface IBusinessLogic
    {
        IList<Dish> GetAllDishes(Func<Dish, bool> predicate = null);

        Dish GetDish(int id);

        Result AddDish(Dish dish);

        Result UpdateDish(Dish dish);

        Result RemoveDish(Dish dish);

        Result RemoveDishById(int id);

        Account GetAccount(string email);

        Result AddAccount(Account account);

        Result RegisterAccount(string email, string password);

        Result CheckAccount(string email, string password);
    }
}