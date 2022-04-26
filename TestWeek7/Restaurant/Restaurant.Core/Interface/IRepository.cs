using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Core.Interface
{
    public interface IRepository<TEntity>
    {
        TEntity Get(object key);

        IEnumerable<TEntity> GetAll(Func<TEntity, bool> filter = null);

        // CUD
        bool Add(TEntity entity);
        bool Update(TEntity entity);
        bool Remove(TEntity entity);
        bool RemoveByKey(object key);
    }
}