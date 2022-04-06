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
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, IList<Product>>
    {
        private readonly IProductRepository _repository;

        public GetAllProductsHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IList<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAll(cancellationToken);
        }    
    }
}
