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
    }
}