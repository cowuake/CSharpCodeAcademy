using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerOrderManagement.Core.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public IList<Order> Orders { get; set; }
    }
}