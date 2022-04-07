using AutoMapper;
using MediatR;
using Warehouse.Application.Interfaces.Repositories;
using Warehouse.Application.MediatR.Commands.ProductCommands;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.MediatR.Handlers.ProductHandlers
{
    public class InsertProductHandler : IRequestHandler<InsertProductCommand, Product>
    {

        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public InsertProductHandler(IProductRepository productRepository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<Product> Handle(InsertProductCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(request.InputModel) ?? throw new NullReferenceException();
            var category = await _categoryRepository.Get(product.CategoryId,cancellationToken);

            product.StockStatus = _productRepository.GetStockStatus(product, category);
            await _productRepository.Insert(product, cancellationToken);
            await _categoryRepository.AddProductToCategory(product, product.CategoryId, cancellationToken);
            return product;
        }
    }
}
