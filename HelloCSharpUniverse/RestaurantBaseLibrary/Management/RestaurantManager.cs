using System;
using System.Collections.Generic;
using System.Linq;

namespace Restaurant.BaseLibrary
{
    public static class RestaurantManager
    {
        public static bool AllTablesClosed(DataContext state)
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

        public static void UpdateMenu(DataContext state, MenuChanges change, params Object[] jolly)
        {
            if (!AllTablesClosed(state))
                throw new Exception("Cannot modify the menu, some tables still open.");

            switch (change)
            {
                case MenuChanges.NewItem:

                    if (!(jolly[0] is MenuItem))
                        throw new Exception("Impossible to add invalid item to menu.");

                    state.Context.MenuItems.Add(jolly[0] as MenuItem);

                    break;

                case MenuChanges.NewCategory:

                    if (!(jolly[0] is MenuItemCategory))
                        throw new Exception("Impossibile to add invalid category to menu");

                    state.Context.MenuCategories.Add(jolly[0] as MenuItemCategory);
                    
                    break;

                case MenuChanges.ChangeItem:

                    if (!(jolly[0] is string))
                        throw new Exception("Impossible to modify menu item with the given parameters!");

                    IList<MenuItem> items;
                    SearchDomain(state, typeof(MenuItem), out items);

                    MenuItem target, replacement;
                    FindInContext(jolly[0] as string, state, out target);

                    try
                    {
                        replacement = new MenuItem(
                            target.UniqueID,
                            !(jolly[1] is null) ? (string)jolly[1] : target.Name,
                            !(jolly[2] is null) ? (MenuItemCategory)jolly[2] : target.Category,
                            !(jolly[3] is null) ? (decimal)jolly[3] : target.Price,
                            !(jolly[4] is null) ? (string)jolly[4] : target.Description
                        );
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message + "Failed to modify menu item.");
                    }
                    

                    items.Remove(target);
                    items.Add(replacement);

                    break;
            }
        }
    }
}