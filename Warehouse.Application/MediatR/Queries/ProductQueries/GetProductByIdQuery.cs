using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.MediatR.Queries.ProductQueries
{
    public class GetProductByIdQuery : IRequest<Product>
    {
        public string Id { get; set; } = string.Empty;
    }
}
