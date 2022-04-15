using CustomerOrderManagement.Core.EF.DataContext;
using CustomerOrderManagement.Core.Entities;
using CustomerOrderManagement.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomerOrderManagement.Core.EF.Repositories
{
    public class EFCoreOrderRepository : IOrderRepository
    {
        private readonly CustomerOrderManagementContext _context;

        public EFCoreOrderRepository(CustomerOrderManagementContext context)
        {
            _context = context;
        }

        public bool Add(Order entity)
        {
            try
            {
                _context.Orders.Add(entity);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<Order> Fetch(Func<Order, bool> filter = null)
        {
            try
            {
                if (filter != null)
                    return _context.Orders.Where(filter).ToList();

                return _context.Orders.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Order Get(object key)
        {
            if (key == null)
                return null;

            try
            {
                return _context.Orders.Find(key);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool Remove(Order entity)
        {
            try
            {
                _context.Orders.Remove(entity);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(Order entity)
        {
            throw new NotImplementedException();
        }
    }
}