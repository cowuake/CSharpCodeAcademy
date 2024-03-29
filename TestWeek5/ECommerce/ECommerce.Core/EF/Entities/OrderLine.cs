﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Core.EF.Entities
{
    public class OrderLine
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}