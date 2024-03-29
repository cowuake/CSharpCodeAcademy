﻿using Library.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core.Interface
{
    public interface IAccountRepository : IRepository<Account>
    {
        Account GetByUsername(string username);
    }
}