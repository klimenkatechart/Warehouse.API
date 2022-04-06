using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Application.Interfaces.Repositories;
using Warehouse.Domain.Helpers;

namespace Warehouse.Infrastructure.Repositories
{
    public class BaseRepository<TEntity> : MongoBaseContext<TEntity>
    {
        public BaseRepository(IOptions<MongoDbConfiguration> settings) : base(settings) { }

        public Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> Get(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<TEntity>> GetAll(string id)
        {
            throw new NotImplementedException();
        }

        public Task Insert(TEntity entity)
        {
            return Collection.InsertOneAsync(entity);
        }

        public Task<TEntity> InsertRange(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> Update(string id, TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
