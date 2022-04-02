using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDesk.Core.Interface
{
    public interface IRepository<TEntity>
    {
        // Simply... CRUD

        // Read
        IEnumerable<TEntity> Fetch();
        TEntity GetById(int id);

        // Create
        bool Add(TEntity entity);

        // Update
        bool Update(TEntity entity);

        // Delete
        bool Delete(TEntity entity);
    }
}
