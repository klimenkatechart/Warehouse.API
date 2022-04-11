using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Application.Interfaces.Repositories
{
    public interface IBaseRepository<TEntity>
    {
        public Task Delete(string id, CancellationToken cancellationToken);
        public Task<TEntity> Get(string id, CancellationToken cancellationToken);
        public Task<IList<TEntity>> GetAll(CancellationToken cancellationToken);
        public Task Insert(TEntity entity, CancellationToken cancellationToken);
        public Task InsertRange(IEnumerable<TEntity> entities, CancellationToken cancellationToken);
        public Task Replace(string id, TEntity entity, CancellationToken cancellationToken);
        Task Update(string id, UpdateDefinition<TEntity> updateDefinition, CancellationToken cancellationToken);
    };
}
