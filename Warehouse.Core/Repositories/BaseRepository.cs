using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Application.Interfaces.Repositories;
using Warehouse.Domain.Entities;
using Warehouse.Domain.Helpers;

namespace Warehouse.Infrastructure.Repositories
{
    public class BaseRepository<TEntity> : MongoBaseContext<TEntity>, IBaseRepository<TEntity>
    {
        public BaseRepository(IOptions<MongoDbConfiguration> settings) : base(settings) { }

        public async Task Delete(string id, CancellationToken cancellationToken)
        {
            await Collection.DeleteOneAsync(GetFilterForId(id), cancellationToken);
        }

        public async Task<TEntity> Get(string id, CancellationToken cancellationToken)
        {
           return await Collection.Find(GetFilterForId(id)).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IList<TEntity>> GetAll(CancellationToken cancellationToken)
        {
            return await Collection.Find(FilterDefinition<TEntity>.Empty).ToListAsync(cancellationToken);
        }

        public async Task<TEntity> GetWhere(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken) 
        {
            return await GetFiltered(predicate).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task Insert(TEntity entity, CancellationToken cancellationToken)
        {
            await Collection.InsertOneAsync(entity, null, cancellationToken);
        }

        public async Task InsertRange(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
        {
            await Collection.InsertManyAsync(entities, null, cancellationToken);
        }

        public async Task Update(string id, TEntity entity, CancellationToken cancellationToken)
        {
            await Collection.ReplaceOneAsync(GetFilterForId(id), entity, new ReplaceOptions { IsUpsert = true }, cancellationToken);            
        }
        private IMongoQueryable<TEntity> GetFiltered(Expression<Func<TEntity, bool>> predicate)
        {
            return Collection.AsQueryable()
                .Where(predicate);
        }

        private FilterDefinition<TEntity> GetFilterForId(string id)
        {
            return Builders<TEntity>.Filter.Eq("Id", id);
        }
    }
}
