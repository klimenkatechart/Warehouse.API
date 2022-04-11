using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.MediatR.Commands.ProductCommands
{
    public class AddItemsForproductCommand : IRequest<Product>
    {
        public string ProductId { get; set; } = Guid.NewGuid().ToString();
        public int Ammount { get; set; }
    }
}
