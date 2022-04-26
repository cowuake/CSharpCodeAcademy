using Restaurant.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Core.Interface
{
    public interface IAccountRepository : IRepository<Account>
    {
        Account GetByEmail(string email);
    }
}