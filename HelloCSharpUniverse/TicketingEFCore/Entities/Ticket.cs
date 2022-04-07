using System;

namespace TicketingEFCore.Entities
{
    public class Ticket
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Customer { get; set; }

        public string State { get; set; }

        public Category Category { get; set; } // One-to-Many

        public int CategoryId { get; set; }
    }
}

//public enum TicketState
//{
//    New,
//    OnGoing,
//    Resolved
//}