using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Application.Interfaces.Repositories;
using Warehouse.Application.SAGA.Commands;
using Warehouse.Application.SAGA.StateMachine;
using Warehouse.Domain.Enums;

namespace Warehouse.Application.SAGA.Consumers
{
    public class ResolveAwaitingOrdersConsumer : IConsumer<ResolveAwaitingOrdersCommand>
    {
        private readonly IOrderRepository _orderRepository;

        public ResolveAwaitingOrdersConsumer(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task Consume(ConsumeContext<ResolveAwaitingOrdersCommand> context)
        {
            var order = await _orderRepository.GetPendingOrder(context.Message.ProductId,context.Message.Amount,context.CancellationToken);

            if (order is null) return;

            await context.Publish<OrderApproveCommand>(new OrderApproveCommand() { 
                OrderId = order.Id,
                isApproved = true,
                CorrelationId = context.Message.CorrelationId,             
            });

        } 
    }
}
