using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Application.Interfaces.Repositories;
using Warehouse.Application.MediatR.Queries.ProductQueries;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.MediatR.Handlers.ProductHandlers
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery,Product>
    {
        private readonly IProductRepository _repository;

        public GetProductByIdHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.Get(request.Id, cancellationToken);
        }
    }
}
