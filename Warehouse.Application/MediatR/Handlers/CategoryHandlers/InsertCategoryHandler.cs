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
    public class InsertCategoryHandler : IRequestHandler<InsertCategoryCommand, Category>
    {

        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;
        public InsertCategoryHandler(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Category> Handle(InsertCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<Category>(request.InputModel) ?? throw new NullReferenceException();
            await _repository.Insert(category, cancellationToken);
            return category;
        }
    }
}
