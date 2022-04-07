using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Application.Interfaces.Repositories;
using Warehouse.Application.Models.Create;
using Warehouse.Domain.Entities;
using Warehouse.Domain.Enums;
using Warehouse.Domain.Helpers;

namespace Warehouse.Infrastructure.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(IOptions<MongoDbConfiguration> settings) : base(settings) { }

        public async Task AddNewProduct(Product entity, CancellationToken cancellationToken)
        {
            var categories = GetCollection<Category>();
            var category = await categories.Find(GetFilterForId<Category>(entity.CategoryId)).FirstOrDefaultAsync(cancellationToken);

            if (category is null) throw new Exception("Category not found");

            using var session = await Client.StartSessionAsync();
            session.StartTransaction();
            try
            {
                await Collection.InsertOneAsync(entity, null, cancellationToken);

                var update = Builders<Category>.Update.Push(x => x.ProductsIds, entity.Id);
                await categories.UpdateOneAsync(GetFilterForId<Category>(entity.CategoryId), update, null, cancellationToken);
                
                await session.CommitTransactionAsync(cancellationToken);
            }
            catch (Exception e)
            {
                await session.AbortTransactionAsync(cancellationToken);
                throw new Exception($"Transaction failed: {e}");
            }
        }
        public async Task DeleteProduct(string id, CancellationToken cancellationToken)
        {
            var product = await Collection.Find(GetFilterForId<Product>(id)).FirstOrDefaultAsync(cancellationToken);
            var categories = GetCollection<Category>();
            var category = categories.Find(GetFilterForId<Category>(product.CategoryId)).FirstOrDefaultAsync(cancellationToken);


            using var session = await Client.StartSessionAsync();
            session.StartTransaction();
            try
            {
                await Collection.DeleteOneAsync(GetFilterForId<Product>(id), null, cancellationToken);

                var update = Builders<Category>.Update.Pull(x => x.ProductsIds, id);
                await categories.UpdateOneAsync(GetFilterForId<Category>(product.CategoryId), update, null, cancellationToken);
             
                await session.CommitTransactionAsync(cancellationToken);
            }
            catch (Exception e)
            {
                await session.AbortTransactionAsync(cancellationToken);
                throw new Exception($"Transaction failed: {e}");
            }
        }
        public async Task UpdateProductWithReferences(string productId, string oldCategoryId, Product updatedModel, CancellationToken cancellationToken)
        {           
            var categories = GetCollection<Category>();
            var oldCategory = categories.Find(GetFilterForId<Category>(oldCategoryId)).FirstOrDefaultAsync(cancellationToken);
            var newCategory = categories.Find(GetFilterForId<Category>(updatedModel.CategoryId)).FirstOrDefaultAsync(cancellationToken) ?? throw new Exception("Category does not exists");


            using var session = await Client.StartSessionAsync();
            session.StartTransaction();
            try
            {
                var updateOld = Builders<Category>.Update.Pull(x => x.ProductsIds, updatedModel.Id);
                var updateNew = Builders<Category>.Update.Push(x => x.ProductsIds, updatedModel.Id);

                await categories.UpdateOneAsync(GetFilterForId<Category>(oldCategoryId), updateOld, null, cancellationToken);
                await categories.UpdateOneAsync(GetFilterForId<Category>(updatedModel.CategoryId), updateNew, null, cancellationToken);

                await Replace(productId, updatedModel, cancellationToken);

                await session.CommitTransactionAsync(cancellationToken);
            }
            catch (Exception e)
            {
                await session.AbortTransactionAsync(cancellationToken);
                throw new Exception($"Transaction failed: {e}");
            }
        }
        public ProductStockStatus GetStockStatus(Product product, Category category)
        {
            if (product.Ammount == 0) return ProductStockStatus.OutOfStock;
            if (product.Ammount < category.StockThreshold) return ProductStockStatus.LowStock;

            return ProductStockStatus.InStock;
        }
    }
}
