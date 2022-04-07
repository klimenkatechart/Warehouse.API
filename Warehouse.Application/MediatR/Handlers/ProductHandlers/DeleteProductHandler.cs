using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Application.Interfaces.Repositories;
using Warehouse.Application.MediatR.Commands.ProductCommands;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.MediatR.Handlers.ProductHandlers
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, Unit>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public DeleteProductHandler(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository= productRepository;
            _categoryRepository= categoryRepository;
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.Get(request.id, cancellationToken);

            await _productRepository.Delete(request.id, cancellationToken);
            await _categoryRepository.RemoveProductFromCategory(product, cancellationToken);
            return Unit.Value;
        }
    }
}
