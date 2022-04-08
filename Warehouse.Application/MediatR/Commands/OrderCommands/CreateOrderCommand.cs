using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Application.Models.Create;
using Warehouse.Application.Models.View;

namespace Warehouse.Application.MediatR.Commands.OrderCommands
{
    public class CreateOrderCommand : IRequest<OrderViewModel>
    {
        public OrderInputModel inputModel { get; set; } = new OrderInputModel();
    }
}
