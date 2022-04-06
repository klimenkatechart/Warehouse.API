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

        public async Task Delete(string id)
        {
            await Collection.DeleteOneAsync(GetFilterForId(id));
        }

        public async Task<TEntity> Get(string id)
        {
           return await Collection.Find(GetFilterForId(id)).FirstOrDefaultAsync();
        }

        public async Task<IList<TEntity>> GetAll()
        {
            return await Collection.Find(FilterDefinition<TEntity>.Empty).ToListAsync();
        }

        public async Task<TEntity> GetWhere(Expression<Func<TEntity, bool>> predicate) 
        {
            return await GetFiltered(predicate).FirstOrDefaultAsync();
        }

        public async Task Insert(TEntity entity)
        {
            await Collection.InsertOneAsync(entity);
        }

        public async Task InsertRange(IEnumerable<TEntity> entities)
        {
            await Collection.InsertManyAsync(entities);
        }

        public async Task<TEntity> Update(string id, TEntity entity)
        {       
            await Collection.ReplaceOneAsync(GetFilterForId(id), entity);
            return await Get(id);
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
