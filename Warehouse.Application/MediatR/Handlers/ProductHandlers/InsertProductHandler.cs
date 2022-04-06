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
    public class InsertProductHandler : IRequestHandler<InsertProductCommand, Product>
    {

        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        public InsertProductHandler(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Product> Handle(InsertProductCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(request.InputModel) ?? throw new NullReferenceException();
            await _repository.Insert(product, cancellationToken);
            return product;
        }
    }
}
