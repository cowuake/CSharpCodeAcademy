using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Library.Core.Entities
{
    [DataContract]
    public class Book
    {
        [DataMember]
        public string ISBN { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Summary { get; set; }

        [DataMember]
        public string Author { get; set; }

        [DataMember]
        public int? Pages { get; set; }

        [DataMember]
        public string Publisher { get; set; }

        [DataMember]
        public int? Year { get; set; }

        [DataMember]
        public int? Edition { get; set; }

        [DataMember]
        public string Note { get; set; }

        [DataMember]
        public string Language { get; set; }
    }
}