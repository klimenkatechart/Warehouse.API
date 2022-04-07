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
    public class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesQuery, IList<Category>>
    {

        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;
        public GetAllCategoriesHandler(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IList<Category>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAll(cancellationToken);
        }
    }
}
