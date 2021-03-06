using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Domain.Entities;
using Warehouse.Domain.Enums;

namespace Warehouse.Application.Interfaces.Repositories
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task AddNewProduct(Product entity, CancellationToken cancellationToken);
        ProductStockStatus GetStockStatus(Product product, Category category);
        Task<Product> SetItemsForProduct(Product product, Category category, int Ammount, CancellationToken cancellationToken);
        Task<Product> AddItemsForProduct(Product product, Category category, int Ammount, CancellationToken cancellationToken);
    }
}
