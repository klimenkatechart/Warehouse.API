using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Application.Interfaces.Repositories;
using Warehouse.Application.MediatR.Commands.CategoryCommands;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.MediatR.Handlers.CategoryHandlers
{
    public class InsertRangeOfCategoriesHandler : IRequestHandler<InsertRangeOfCategoriesCommand, IList<Category>>
    {

        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;
        public InsertRangeOfCategoriesHandler(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IList<Category>> Handle(InsertRangeOfCategoriesCommand request, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<IList<Category>>(request.InputModels) ?? throw new NullReferenceException();
            await _repository.InsertRange(category, cancellationToken);
            return category;
        }
    }
}
