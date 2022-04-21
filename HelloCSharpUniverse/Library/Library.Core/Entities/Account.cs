using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Library.Core.Entities
{
    [DataContract]
    public class Account
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Username { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public Role Role { get; set; }

        [DataMember]
        public IList<BookLoan> BookLoans { get; set; }
    }

    public enum Role : byte
    {
        User,
        Administrator,
    }
}