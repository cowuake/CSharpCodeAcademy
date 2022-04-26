using Restaurant.Core.Entities;
using Restaurant.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Restaurant.Core.EF.Repositories
{
    public class EFCoreAccountRepository : IAccountRepository
    {
        private readonly RestaurantContext _context;

        public EFCoreAccountRepository(RestaurantContext context)
        {
            _context = context;
        }

        public bool Add(Account account)
        {
            try
            {
                _context.Accounts.Add(account);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Account Get(object key)
        {
            if ((int)key <= 0)
                return null;

            try
            {
                return _context.Accounts.Find(key);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<Account> GetAll(Func<Account, bool> filter = null)
        {
            try
            {
                if (filter != null)
                    return _context.Accounts.Where(filter).ToList();

                return _context.Accounts.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Account GetByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return null;

            return _context.Accounts.FirstOrDefault(a => a.Email == email);
        }

        public bool Remove(Account account)
        {
            try
            {
                _context.Accounts.Remove(account);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoveByKey(object key)
        {
            if ((int)key <= 0)
                return false;

            var account = _context.Accounts.Find(key);

            try
            {
                _context.Accounts.Remove(account);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(Account account)
        {
            if (account == null)
                return false;

            try
            {
                _context.Update(account);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}