using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Library.Core.Entities
{
    [DataContract]
    public class BookGenre
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Family { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public IList<Book> Books { get; set; }
    }
}