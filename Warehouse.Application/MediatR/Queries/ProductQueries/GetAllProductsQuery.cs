using MediatR;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.MediatR.Queries.ProductQueries
{
    public class GetAllProductsQuery : IRequest<IList<Product>>
    {
    }
}
