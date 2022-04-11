using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.Interfaces.Repositories
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        Task<IList<Order>> GetAllOrdersInReview(CancellationToken cancellationToken);
        Task<Order> GetPendingOrder(string productId, int Ammount, CancellationToken cancellationToken);
    }
}