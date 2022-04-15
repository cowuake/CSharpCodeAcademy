using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerOrderManagement.Core.Interface
{
    public interface IRepository <TEntity>
    {
        TEntity Get(object key);

        IEnumerable<TEntity> Fetch(Func<TEntity, bool> filter = null);

        bool Add(TEntity entity);

        bool Update(TEntity entity);

        bool Remove(TEntity entity);
    }
}