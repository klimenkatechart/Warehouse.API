using AutoMapper;
using MediatR;
using Warehouse.Application.Interfaces.Repositories;
using Warehouse.Application.MediatR.Commands.ProductCommands;
using Warehouse.Application.Models.Create;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.MediatR.Handlers.ProductHandlers
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, Product>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        public UpdateProductHandler(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Product> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _repository.Get(request.Id, cancellationToken);
            product = _mapper.Map<ProductInputModel,Product>(request.ProductInput, product) ?? throw new NullReferenceException();
            await _repository.Update(request.Id, product, cancellationToken);
            return product;
        }
    }
}
