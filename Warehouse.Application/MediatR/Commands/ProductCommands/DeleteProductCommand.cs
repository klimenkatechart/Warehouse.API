using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.MediatR.Commands.ProductCommands
{
    public class DeleteProductCommand : IRequest
    {
        public string id { get; set; } = string.Empty;
    }
}
