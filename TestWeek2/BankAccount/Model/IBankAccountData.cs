using System;
using System.Collections.Generic;
using System.Text;

namespace BankAccount.Model
{
    public interface IBankAccountDataContext
    {
        bool Save(out string message);
        bool Load(out string message);
    }
}
