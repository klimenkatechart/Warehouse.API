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
            var product = _mapper.Map<IList<Product>>(request.InputModels) ?? throw new NullReferenceException();
            await _repository.InsertRange(product,cancellationToken);
            return product;
        }
    }
}
