using AutoMapper;
using MediatR;
using MongoDB.Driver;
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
    public class SetItemsForProductHandler : IRequestHandler<SetItemsForProduct, Product>
    {

        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public SetItemsForProductHandler(IProductRepository productRepository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<Product> Handle(SetItemsForProduct request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.Get(request.ProductId, cancellationToken) ?? throw new NullReferenceException();
            var category = await _categoryRepository.Get(product.CategoryId, cancellationToken);

            await _productRepository.SetItemsForProduct(product, category, request.Ammount, cancellationToken);
            return product;
        }
    }
}
