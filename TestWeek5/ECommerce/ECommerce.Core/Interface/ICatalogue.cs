using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Core.Interface
{
    public interface ICatalogue
    {
        /// <summary>
        /// Inserts a new product
        /// </summary>
        void InsertProduct();

        /// <summary>
        /// Changes the category of a product
        /// </summary>
        void ChangeProductCategory();

        /// <summary>
        /// Changes the details of a product
        /// </summary>
        void ChangeProductDetails();

        /// <summary>
        /// Lists all visible products
        /// </summary>
        void ListVisibleProducts();

        /// <summary>
        /// Lists all orders by descending date
        /// </summary>
        void ListOrdersByDescendingDate();

        /// <summary>
        /// Lists totals sale for each product category
        /// </summary>
        void ListTotalSalesByCategory();

        /// <summary>
        /// Lists products filtered by price
        /// </summary>
        void ListProductsFilteredByPrice();

        /// <summary>
        /// Lists products filtered by name
        /// </summary>
        void ListProductsFilteredByName();

        /// <summary>
        /// Lists products filtered by category
        /// </summary>
        void ListProductsFilteredByCategory();

        /// <summary>
        /// Lists products with multiple filters
        /// </summary>
        void ListProductsFiltered();

        /// <summary>
        /// Changes visibility of a product
        /// </summary>
        void ChangeProductVisibility();
    }
}