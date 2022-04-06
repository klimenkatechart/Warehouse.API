using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Warehouse.Domain.Helpers;

namespace Warehouse.Infrastructure
{
    public class MongoBaseContext<TEntity>
    {
        private readonly IMongoDatabase database;
        public MongoBaseContext(IOptions<MongoDbConfiguration> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            database = client.GetDatabase(settings.Value.DatabaseName);
        }
        protected IMongoCollection<TEntity> Collection => database.GetCollection<TEntity>(typeof(TEntity).Name);
    }
}
