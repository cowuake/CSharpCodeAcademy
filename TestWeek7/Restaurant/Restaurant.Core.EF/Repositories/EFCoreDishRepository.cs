using Restaurant.Core.Entities;
using Restaurant.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Restaurant.Core.EF.Repositories
{
    public class EFCoreDishRepository : IDishRepository
    {
        private readonly RestaurantContext _context;

        public EFCoreDishRepository(RestaurantContext context)
        {
            _context = context;
        }

        public bool Add(Dish dish)
        {
            try
            {
                _context.Dishes.Add(dish);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Remove(Dish dish)
        {
            try
            {
                _context.Dishes.Remove(dish);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoveByKey(object key)
        {
            if ((int)key <= 0)
                return false;

            var book = _context.Dishes.Find((int)key);

            try
            {
                _context.Dishes.Remove(book);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Dish Get(object key)
        {
            if ((int) key <= 0)
                return null;

            try
            {
                return _context.Dishes.Find((int) key);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<Dish> GetAll(Func<Dish, bool> filter = null)
        {
            try
            {
                //if (filter != null)
                //    return _context.Books.Where(filter).ToList();

                //return _context.Books.ToList();

                // ========================= IMPORTANT! =========================
                // If using Entity Framework Core 3.1.24, the ToList method is MANDATORY!
                // Otherwise, a null object will be passed to the business logic!

                if (filter != null)
                    return _context.Dishes.Where(filter).ToList();

                return _context.Dishes.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool Update(Dish dish)
        {
            if (dish == null)
                return false;

            try
            {
                _context.Update(dish);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}