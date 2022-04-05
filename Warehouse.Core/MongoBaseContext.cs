using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Domain.Helpers;

namespace Warehouse.Infrastructure
{
    public class MongoBaseContext<T>
    {
        private readonly IMongoCollection<T> collection;
        private readonly MongoDbConfiguration _settings;
        public MongoBaseContext(IOptions<MongoDbConfiguration> settings)
        {
            _settings = settings.Value;
            var client = new MongoClient(_settings.ConnectionString);
            var database = client.GetDatabase(_settings.DatabaseName);
            collection = database.GetCollection<T>(nameof(T));
        }
    }
}
