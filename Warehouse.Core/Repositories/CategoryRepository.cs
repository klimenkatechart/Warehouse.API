using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Application.Interfaces.Repositories;
using Warehouse.Domain.Entities;
using Warehouse.Domain.Helpers;

namespace Warehouse.Infrastructure.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(IOptions<MongoDbConfiguration> settings) : base(settings) { }

        public async Task AddProductToCategory(Product product,string categoryId, CancellationToken cancellationToken)
        {
            var currentCategory = await GetWhere(x => x.ProductsIds.Contains(product.Id),cancellationToken);
            if(currentCategory is not null) await RemoveProductFromCategory(product, cancellationToken);
          
            var updateNew = Builders<Category>.Update.Push(x => x.ProductsIds, product.Id);
            await Collection.UpdateOneAsync(GetFilterForId<Category>(categoryId), updateNew, null, cancellationToken);

        }

        public async Task RemoveProductFromCategory(Product product, CancellationToken cancellationToken)
        {
           
            var updateOld = Builders<Category>.Update.Pull(x => x.ProductsIds, product.Id);
            await Collection.UpdateOneAsync(GetFilterForId<Category>(product.CategoryId), updateOld, null, cancellationToken);
          

        }
    }
}
