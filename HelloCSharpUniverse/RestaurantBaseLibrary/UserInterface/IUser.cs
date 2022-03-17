using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.BaseLibrary
{
    public interface IUser
    {
        public string Username { get; }
        public string Password { get; }

    }
}
