using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Core.EF.Entities
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IList<Product> Products { get; set; }
    }
}
