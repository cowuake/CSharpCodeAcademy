using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core.Interface
{
    public interface IRepository<TEntity>
    {
        TEntity Get(object key);

        IEnumerable<TEntity> GetAll(Func<TEntity, bool> filter = null);

        // CUD
        bool Add(TEntity entity);
        bool Update(TEntity entity);
        bool Delete(object key);
    }
}