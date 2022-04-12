using Employees.Core.Entities;
using Employees.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Employees.Core.EF.Repository
{
    public class EmployeeRepositoryEF : IEmployeeRepository
    {
        private readonly EmployeeContext _context;

        public EmployeeRepositoryEF()
        {
            // Not the best, we should rather employ Dependency Injection!
            _context = new EmployeeContext();
        }

        public EmployeeRepositoryEF(EmployeeContext context)
        {
            // Context must be injected
            _context = context;
        }

        public bool Add(Employee entity)
        {
            if (entity == null)
                return false;

            _context.Employees.Add(entity);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public IEnumerable<Employee> Fetch(Func<Employee, bool> filter = null)
        {
            if (filter != null)
                return _context.Employees.Where(filter);

            return _context.Employees;
        }

        public Employee GetByCode(string code)
        {
            if (string.IsNullOrEmpty(code))
                return null;

            return _context.Employees.SingleOrDefault(x => x.Code == code);
        }

        public Employee GetById(object id)
        {
            return _context.Employees.Find(id);
        }

        public bool DeleteById(object id)
        {
            var employee = GetById(id);

            if (employee == null)
                return false;

            try
            {
                _context.Remove(employee);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool Update(Employee entity)
        {
            if (entity == null)
                return false;

            try
            {
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}