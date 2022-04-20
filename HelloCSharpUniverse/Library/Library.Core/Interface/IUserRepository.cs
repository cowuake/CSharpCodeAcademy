using Library.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core.Interface
{
    public interface IUserRepository : IRepository<User>
    {
        User GetByUsername(string username);
    }
}