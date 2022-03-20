using System;
using System.Collections.Generic;
using System.Text;

namespace BankAccount.Model
{
    public interface IBankAccountDataContext
    {
        bool Save(string path, out string message);
        bool Load(string path, out string message);
    }
}