using Microsoft.Extensions.Options;
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
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(IOptions<MongoDbConfiguration> settings) : base(settings) { }

        public async Task<IList<Order>> GetAllOrdersInReview(CancellationToken cancellationToken)
        {
           return await GetAllWhere(x => x.Status == Domain.Enums.OrderStatus.InReview, cancellationToken);
        }
        public async Task<Order> GetPendingOrder(string productId,int Ammount, CancellationToken cancellationToken)
        {
            return await GetWhere(x => x.Status == Domain.Enums.OrderStatus.Pending && x.ProductId ==productId && x.AmmountOfItems <= Ammount, cancellationToken);
        }

    }
}
