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
    public class InsertProductCommand : IRequest<Product>
    {
        public ProductInputModel InputModel { get; set; } = new ProductInputModel();
    }
}
