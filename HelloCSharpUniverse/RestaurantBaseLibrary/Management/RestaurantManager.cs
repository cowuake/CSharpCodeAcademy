using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Restaurant.BaseLibrary
{
    public static class RestaurantManager
    {
        public static bool AllTableClosed(DataContext state)
            => state.Context.Tables.All(table => table.Available);

        private static bool SearchDomain<T>(DataContext state, Type type, out IList<T> domain)
        {
            domain = null;

            if (type == typeof(MenuItem))
                domain = state.Context.MenuItems as IList<T>;

            if (type == typeof(MenuItemCategory))
                domain = state.Context.MenuCategories as IList<T>;

            return true;
        }

        private static bool FindInContext<T>(string id, DataContext state, out T element)
        {
            IList<T> domain;
            element = default(T);

            if (!SearchDomain(state, typeof(T), out domain))
                throw new Exception("Searching for the wrong type of data in data context!");

            element = domain.FirstOrDefault(x => x.GetType().GetMember("UniqueID").Equals(id));

            return true;
        }

        public static void UpdateMenu(DataContext state, MenuChanges change, params Object[] args)
        {
            if (!AllTableClosed(state))
                throw new Exception("Cannot modify the menu, some tables still open.");

            switch (change)
            {
                case MenuChanges.NewItem:

                    if (!(args[0] is MenuItem))
                        throw new Exception("Impossible to add invalid item to menu.");

                    state.Context.MenuItems.Add(args[0] as MenuItem);
                    break;

                case MenuChanges.NewCategory:

                    if (!(args[0] is MenuItemCategory))
                        throw new Exception("Impossibile to add invalid category to menu");

                    state.Context.MenuCategories.Add(args[0] as MenuItemCategory);    
                    break;

                case MenuChanges.ChangeItem:

                    if (!(args[0] is Tuple))
                        throw new Exception("Impossible to modify menu item with the given parameters!");

                    break;
                case MenuChanges.ChangeCategory:
                    break;
            }
        }
    }
}