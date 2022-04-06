using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Application.Models.Create;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.MediatR.Commands.ProductCommands
{
    public class UpdateProductCommand : IRequest<Product>
    {
        public string Id { get; set; } = string.Empty;
        public ProductInputModel ProductInput { get; set; } = new ProductInputModel();
    }
}
