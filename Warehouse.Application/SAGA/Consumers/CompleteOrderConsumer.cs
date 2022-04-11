using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Application.Interfaces.Repositories;
using Warehouse.Application.SAGA.Commands;
using Warehouse.Domain.Enums;

namespace Warehouse.Application.SAGA.Consumers
{
    public class CompleteOrderConsumer : IConsumer<CompleteOrderCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public CompleteOrderConsumer(IProductRepository productRepository,
        ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }
        public async Task Consume(ConsumeContext<CompleteOrderCommand> context)
        {
            var product = await _productRepository.Get(context.Message.ProductId, context.CancellationToken);
            var category = await _categoryRepository.Get(product.CategoryId, context.CancellationToken);

            await _productRepository.SetItemsForProduct(product, category, product.Ammount - context.Message.Amount, context.CancellationToken);
        }
    }
}
