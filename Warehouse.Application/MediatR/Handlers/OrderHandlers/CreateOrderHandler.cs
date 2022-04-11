using AutoMapper;
using MassTransit;
using MediatR;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Application.Interfaces.Repositories;
using Warehouse.Application.MediatR.Commands.OrderCommands;
using Warehouse.Application.MediatR.Commands.ProductCommands;
using Warehouse.Application.Models;
using Warehouse.Application.Models.View;
using Warehouse.Application.SAGA.Commands;
using Warehouse.Application.SAGA.StateMachine;
using Warehouse.Domain.Entities;
using Warehouse.Domain.Enums;

namespace Warehouse.Application.MediatR.Handlers.OrderHandlers
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, StatusResult>
    {

        private readonly IProductRepository _productRepository;
        readonly IPublishEndpoint _publishEndpoint;
        private readonly IRequestClient<InitiateOrderCommand> _client;

        public CreateOrderHandler(IProductRepository productRepository, IPublishEndpoint publishEndpoint, IRequestClient<InitiateOrderCommand> client)
        {
            _productRepository = productRepository;
            _client=client;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<StatusResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.Get(request.inputModel.ProductId, cancellationToken);

            if (request.inputModel.AmmountOfItems > product.Ammount) throw new Exception("Can't reserve more than in stock");
            
            OrderStatus status;
            
           
            if (product.StockStatus == ProductStockStatus.InStock)
            {
                status = OrderStatus.Approved;
            }
            else if (product.StockStatus == ProductStockStatus.LowStock)
            {
                status = OrderStatus.InReview;
            }
            else
            {
                if (!request.isWaitForStock)
                {
                    status = OrderStatus.Declined;
                }
                else
                {
                    status = OrderStatus.Pending;
                }                   
            }

 
            var result = await _client.GetResponse<StatusResult>(new InitiateOrderCommand()
            {
                CorrelationId = NewId.NextGuid(),
                ProductId = product.Id,
                Amount = request.inputModel.AmmountOfItems,
                Status = status
            });
            
            return result.Message;


        }
    }
}
