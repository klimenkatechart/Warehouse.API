using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Application.Interfaces.Repositories;
using Warehouse.Application.MediatR.Commands.CategoryCommands;

namespace Warehouse.Application.MediatR.Handlers.CategoryHandlers
{
    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, Unit>
    {
        private readonly ICategoryRepository _repository;

        public DeleteCategoryHandler(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _repository.Get(request.id,cancellationToken);
            if (category.ProductsIds.Any()) throw new Exception("Unable to delete category that contains products");

            await _repository.Delete(request.id, cancellationToken);
            return Unit.Value;
        }
    }
}
