using AutoMapper;
using MassTransit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Application.Interfaces.Repositories;
using Warehouse.Application.MediatR.Commands.ProductCommands;
using Warehouse.Application.SAGA.Commands;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.MediatR.Handlers.ProductHandlers
{
    public class AddItemsForProductHandler : IRequestHandler<AddItemsForproductCommand, Product>
    {

        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        readonly IPublishEndpoint _publishEndpoint;

        public AddItemsForProductHandler(IProductRepository productRepository, ICategoryRepository categoryRepository, IPublishEndpoint publishEndpoint)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Product> Handle(AddItemsForproductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.Get(request.ProductId, cancellationToken) ?? throw new NullReferenceException();
            var category = await _categoryRepository.Get(product.CategoryId, cancellationToken);

            await _productRepository.AddItemsForProduct(product, category, request.Ammount, cancellationToken);

            if(product.StockStatus != Domain.Enums.ProductStockStatus.OutOfStock)
            {
                await _publishEndpoint.Publish(new ResolveAwaitingOrdersCommand()
                {
                    CorrelationId = NewId.NextGuid(),
                    ProductId = product.Id,
                    Amount = product.Ammount,
                });
            }
            return product;
        }
    }
}
