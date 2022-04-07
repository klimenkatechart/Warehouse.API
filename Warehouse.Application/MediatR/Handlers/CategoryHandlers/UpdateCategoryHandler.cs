using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Application.Interfaces.Repositories;
using Warehouse.Application.MediatR.Commands.CategoryCommands;
using Warehouse.Application.Models.Create;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.MediatR.Handlers.CategoryHandlers
{
    public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, Category>
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;
        public UpdateCategoryHandler(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Category> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _repository.Get(request.Id, cancellationToken);
            category = _mapper.Map<CategoryInputModel, Category>(request.ProductInput, category) ?? throw new NullReferenceException();
            await _repository.Replace(request.Id, category, cancellationToken);
            return category;
        }
    }
}
