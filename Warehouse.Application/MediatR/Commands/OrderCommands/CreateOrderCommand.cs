using MediatR;
using Warehouse.Application.Models;
using Warehouse.Application.Models.Create;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.MediatR.Commands.OrderCommands
{
    public class CreateOrderCommand : IRequest<StatusResult>
    {
        public OrderInputModel inputModel { get; set; } = new OrderInputModel();
        public bool isWaitForStock { get; set; } = false;
    }
}
