using MediatR;
using Warehouse.Application.Models.Create;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.MediatR.Commands.ProductCommands
{
    public class InsertRangeOfProductsCommand : IRequest<IList<Product>>
    {
        public IList<ProductInputModel> InputModels { get; set; } = new List<ProductInputModel>();
    }
}
