using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Core.EF.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public decimal Total { get; set; }

        public IList<OrderLine> OrderLines { get; set; }
    }
}
