using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Application.Interfaces.Repositories;
using Warehouse.Application.MediatR.Queries.CategoryQueries;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.MediatR.Handlers.CategoryHandlers
{
    public class GetCategoryByIdHandler : IRequestHandler<GetCategoryByIdQuery, Category>
    {

        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;
        public GetCategoryByIdHandler(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Category> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.Get(request.Id, cancellationToken);
        }
    }
}
