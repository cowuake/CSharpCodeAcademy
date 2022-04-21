using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Library.Core.Entities
{
    [DataContract]
    public class BookLoan
    {   
        [DataMember]
        public DateTime StartTime { get; set; }

        [DataMember]
        public DateTime? EndTime { get; set; }

        [DataMember]
        public Account Account { get; set; }

        [DataMember]
        public int AccountId { get; set; }

        [DataMember]
        public Book Book { get; set; }

        [DataMember]
        public string BookIsbn{ get; set; }
    }
}