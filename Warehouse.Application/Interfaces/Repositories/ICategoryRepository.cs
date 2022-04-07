using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.Interfaces.Repositories
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Task AddProductToCategory(Product product, string categoryId, CancellationToken cancellationToken);
        Task RemoveProductFromCategory(Product product, CancellationToken cancellationToken);
    }
}
