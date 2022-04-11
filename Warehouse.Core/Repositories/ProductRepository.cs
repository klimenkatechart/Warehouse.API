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

        public async Task<Product> SetItemsForProduct(Product product,Category category, int Ammount, CancellationToken cancellationToken)
        {
            if (Ammount < 0) throw new Exception("Ammount can not be less then 0 (zero)");

            product.Ammount = Ammount;
            product.StockStatus = GetStockStatus(product, category);

            var update = Builders<Product>.Update.Set(x => x.Ammount, product.Ammount).Set(x => x.StockStatus, product.StockStatus);
            await Update(product.Id, update, cancellationToken);
            return product;
        }

        public async Task<Product> AddItemsForProduct(Product product, Category category, int Ammount, CancellationToken cancellationToken)
        {
            if (Ammount < 0) throw new Exception("Ammount can not be less then 0 (zero)");

            product.Ammount = product.Ammount + Ammount;
            product.StockStatus = GetStockStatus(product, category);

            var update = Builders<Product>.Update.Set(x => x.Ammount, product.Ammount).Set(x => x.StockStatus, product.StockStatus);
            await Update(product.Id, update, cancellationToken);
            return product;
        }

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

        public ProductStockStatus GetStockStatus(Product product, Category category)
        {
            if (product.Ammount <= category.OutOfStock) return ProductStockStatus.OutOfStock;
            if (product.Ammount <= category.LowStock && product.Ammount > category.OutOfStock) return ProductStockStatus.LowStock;

            return ProductStockStatus.InStock;
        }
    }
}
