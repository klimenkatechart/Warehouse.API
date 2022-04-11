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
    public class OrderApproveConsumer : IConsumer<OrderApproveCommand>
    {
        private readonly IOrderRepository _orderRepository;

        public OrderApproveConsumer(IOrderRepository orderRepository)
        {
            _orderRepository=orderRepository;
        }
        public async Task Consume(ConsumeContext<OrderApproveCommand> context)
        {
            await Task.Delay(1000 * 1);
            var order = await _orderRepository.Get(context.Message.OrderId, context.CancellationToken);
            if (order.Status == OrderStatus.Approved || order.Status == OrderStatus.Declined)
            {
                await context.RespondAsync<StatusResult>(new StatusResult()
                {
                   Status = "Already resolved."
                });
                return;
            }
                

            if (!context.Message.isApproved)
            {
                order.Status = OrderStatus.Declined;
                await context.Publish(new OrderDeclined
                {
                    CorrelationId = context.Message.CorrelationId,
                    ProductId = order.ProductId,
                    Amount = order.AmmountOfItems
                }, context.CancellationToken);
            }
            else
            {
                order.Status = OrderStatus.Approved;
                await context.Publish(new OrderApproved
                {
                    CorrelationId = context.Message.CorrelationId,
                    ProductId = order.ProductId,
                    Amount = order.AmmountOfItems
                }, context.CancellationToken);
            }
            await _orderRepository.Replace(context.Message.OrderId, order, context.CancellationToken);

            await context.RespondAsync<StatusResult>(new StatusResult()
            {
                Status = order.Status.ToString()
            });

        }
    }
}
