using Library.Core.Entities;
using Library.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Core.EFCore.Repository
{
    public class EFCoreAccountRepository : IAccountRepository
    {
        private readonly LibraryContext _context;

        public EFCoreAccountRepository(LibraryContext context)
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

        public Account Get(object id)
        {
            if ((int)id <= 0)
                return null;

            try
            {
                return _context.Accounts.Find(id);
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

        public Account GetByUsername(string username)
        {
            if (string.IsNullOrEmpty(username))
                return null;

            return _context.Accounts.FirstOrDefault(x => x.Username.Equals(username));
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

        public bool RemoveByKey(object id)
        {
            var account = _context.Accounts.Find(id);

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
                _context.Update(account); // This is good if working in disconnected mode
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
