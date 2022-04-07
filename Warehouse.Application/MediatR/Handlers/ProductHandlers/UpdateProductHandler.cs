using AutoMapper;
using MediatR;
using Warehouse.Application.Interfaces.Repositories;
using Warehouse.Application.MediatR.Commands.ProductCommands;
using Warehouse.Application.Models.Create;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.MediatR.Handlers.ProductHandlers
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, Product>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public UpdateProductHandler(IProductRepository productRepository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;            
            _mapper = mapper;
        }

        public async Task<Product> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.Get(request.Id, cancellationToken);
            if(product.CategoryId != request.ProductInput.CategoryId && !string.IsNullOrEmpty(request.ProductInput.CategoryId))
            {
                await _categoryRepository.AddProductToCategory(product, request.ProductInput.CategoryId, cancellationToken);
            }
         
            product = _mapper.Map<ProductInputModel, Product>(request.ProductInput, product) ?? throw new NullReferenceException();
            await _productRepository.Replace(request.Id, product, cancellationToken);
            return product;
        }
    }
}
