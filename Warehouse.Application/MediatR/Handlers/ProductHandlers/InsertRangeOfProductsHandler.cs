using AutoMapper;
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
    public class InsertRangeOfProductsHandler : IRequestHandler<InsertRangeOfProductsCommand, IList<Product>>
    {

        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        public InsertRangeOfProductsHandler(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IList<Product>> Handle(InsertRangeOfProductsCommand request, CancellationToken cancellationToken)
        {
            var products = _mapper.Map<IList<Product>>(request.InputModels) ?? throw new NullReferenceException();
            foreach(var product in products)
            {
                await _repository.AddNewProduct(product, cancellationToken);
            }
            return products;
        }
    }
}
