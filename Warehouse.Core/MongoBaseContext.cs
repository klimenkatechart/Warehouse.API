using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Warehouse.Domain.Helpers;

namespace Warehouse.Infrastructure
{
    public class MongoBaseContext<TEntity>
    {
        private readonly IMongoDatabase database;
        private readonly IMongoClient client;
        public MongoBaseContext(IOptions<MongoDbConfiguration> settings)
        {
            client = new MongoClient(settings.Value.ConnectionString);
            database = client.GetDatabase(settings.Value.DatabaseName);
        }
        protected IMongoCollection<TEntity> Collection => database.GetCollection<TEntity>(typeof(TEntity).Name);
        protected IMongoDatabase Database => database;
        protected IMongoClient Client => client;

        protected IMongoCollection<T> GetCollection<T>()
        {
            return database.GetCollection<T>(typeof(T).Name);
        }
    }
}
