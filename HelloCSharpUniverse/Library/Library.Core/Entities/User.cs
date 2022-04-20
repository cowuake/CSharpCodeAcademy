using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public Role Role { get; set; }
    }

    public enum Role : byte
    {
        User,
        Administrator,
    }
}