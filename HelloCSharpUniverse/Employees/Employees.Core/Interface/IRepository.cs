using System;
using System.Collections.Generic;
using System.Text;

namespace Employees.Core.Interface
{
    public interface IRepository<TEntity>
    {
        TEntity GetById(object id);

        IEnumerable<TEntity> Fetch(Func<TEntity, bool> filter = null);

        // CUD
        bool Add(TEntity entity);
        bool Update(TEntity entity);
        bool DeleteById(object id);
    }
}