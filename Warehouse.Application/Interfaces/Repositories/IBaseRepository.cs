using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Application.Interfaces.Repositories
{
    public interface IBaseRepository<TEntity>
    {
        public Task Delete(string id);
        public Task<TEntity> Get(string id);
        public Task<IList<TEntity>> GetAll(string id);
        public Task Insert(TEntity entity);
        public Task<TEntity> InsertRange(IEnumerable<TEntity> entities);
        public Task<TEntity> Update(string id, TEntity entity);
    };
}
