using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerOrderManagement.Core.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Code { get; set; }
        public string ProductCode { get; set; }
        public decimal Total { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}