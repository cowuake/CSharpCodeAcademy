using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Core.EF.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public bool Visible { get; set; }

        public IList<OrderLine> OrderLines { get; set; }
    }
}
