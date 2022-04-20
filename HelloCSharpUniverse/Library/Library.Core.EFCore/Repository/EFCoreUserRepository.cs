using Library.Core.Entities;
using Library.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Core.EFCore.Repository
{
    public class EFCoreUserRepository : IUserRepository
    {
        private readonly LibraryContext _context;

        public EFCoreUserRepository(LibraryContext context)
        {
            _context = context;
        }

        public bool Add(User user)
        {
            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public User Get(object id)
        {
            if ((int)id <= 0)
                return null;

            try
            {
                return _context.Users.Find(id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<User> GetAll(Func<User, bool> filter = null)
        {
            try
            {
                if (filter != null)
                    return _context.Users.Where(filter).ToList();

                return _context.Users.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public User GetByUsername(string username)
        {
            if (string.IsNullOrEmpty(username))
                return null;

            return _context.Users.FirstOrDefault(x => x.Username.Equals(username));
        }

        public bool Remove(User user)
        {
            try
            {
                _context.Users.Remove(user as User);
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
            var user = _context.Users.Find(id);

            try
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(User user)
        {
            if (user == null)
                return false;

            try
            {
                _context.Update(user); // This is good if working in disconnected mode
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
