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
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, Unit>
    {
        private readonly IProductRepository _repository;

        public DeleteProductHandler(IProductRepository repository)
        {
            _repository=repository;
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            await _repository.Delete(request.id, cancellationToken);
            return Unit.Value;
        }
    }
}
