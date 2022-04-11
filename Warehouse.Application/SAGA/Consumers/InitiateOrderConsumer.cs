using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Application.Interfaces.Repositories;
using Warehouse.Application.Models;
using Warehouse.Application.SAGA.Commands;
using Warehouse.Application.SAGA.StateMachine;
using Warehouse.Domain.Entities;
using Warehouse.Domain.Enums;

namespace Warehouse.Application.SAGA.Consumers
{
    public class InitiateOrderConsumer : IConsumer<InitiateOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;

        public InitiateOrderConsumer(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task Consume(ConsumeContext<InitiateOrderCommand> context)
        {
            var order = new Order() { Status = context.Message.Status, AmmountOfItems = context.Message.Amount, ProductId = context.Message.ProductId, OrderDate = DateTime.UtcNow };
            await _orderRepository.Insert(order, context.CancellationToken);

            if(order.Status == OrderStatus.Approved)
            {
                await context.Publish(new OrderApproved
                {
                    CorrelationId = context.Message.CorrelationId,
                    ProductId = context.Message.ProductId,
                    Amount = context.Message.Amount
                }, context.CancellationToken);
            }
            
            if(order.Status == OrderStatus.Declined)
            {
                await context.Publish(new OrderDeclined
                {
                    CorrelationId = context.Message.CorrelationId,
                    ProductId = context.Message.ProductId,
                    Amount = context.Message.Amount
                }, context.CancellationToken);
            }
            await context.RespondAsync<StatusResult>(new StatusResult()
            {
                Status = order.Status.ToString()
            });
        }
    }
}
