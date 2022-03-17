using System;
using Xunit;
using Restaurant.BaseLibrary;

namespace Restaurant.Test
{
    public class TestRestaurant
    {
        private static Func<MenuItemCategory> DefaultMenuItemCategory =
            () => new MenuItemCategory("IDX", "Sea side dishes", "Sea side dishes", 0, 10);

        private static Func<MenuItem> DefaultMenuItem =
            () => new MenuItem("CODE", "Sea mixed fry", DefaultMenuItemCategory(),
                                15.6m, "description");

        private static Func<OrderEntry> defaultOrderEntry =
            () => new OrderEntry(DefaultMenuItem(), 5);

        private static Func<Table> DefaultTable =
            () => new Table(5, 4, 0, DateTime.Now);

        [Fact]
        public void MenuItemCategoryInstantiate()
        {
            MenuItemCategory category = DefaultMenuItemCategory();
            Assert.NotNull(category);
        }

        [Fact]
        public void MenuItemInstantiate()
        {
            MenuItem item = DefaultMenuItem();
            Assert.NotNull(item);
        }

        [Fact]
        public void OrderEntryInstantiate()
        {
            OrderEntry entry = defaultOrderEntry();
            Assert.NotNull(entry);
        }

        [Fact]
        public void TableInstantiate()
        {
            Table table = DefaultTable();
            Assert.NotNull(table);
        }

        [Fact]
        public void TableOpenAndClose()
        {
            Table table = DefaultTable();
            Assert.True(table.Available);

            Assert.False(table.Open(10));

            Assert.True(table.Open(2));
            Assert.False(table.Available);
            Assert.True(table.Close());
        }

        [Fact]
        public void TableAddCustomer()
        {
            Table table = DefaultTable();

            Assert.True(table.Open(2));
            Assert.True(table.AddCustomers(2));
            Assert.False(table.AddCustomers(1));
        }

        [Fact]
        public void TableAddAndRemoveOrderEntries()
        {
            Table table = DefaultTable();

            Assert.True(table.Open(4));
            Assert.True(table.AddOrderEntry(DefaultMenuItem(), 3));
        }
    }
}