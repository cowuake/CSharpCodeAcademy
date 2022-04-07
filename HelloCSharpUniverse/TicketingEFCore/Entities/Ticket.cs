using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

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
    }
}

//public enum TicketState
//{
//    New,
//    OnGoing,
//    Resolved
//}