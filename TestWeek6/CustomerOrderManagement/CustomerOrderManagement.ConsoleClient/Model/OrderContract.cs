using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerOrderManagement.ConsoleClient.Model
{
    internal class OrderContract
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Code { get; set; }
        public string ProductCode { get; set; }
        public decimal Total { get; set; }
        public int CustomerId { get; set; }
    }
}