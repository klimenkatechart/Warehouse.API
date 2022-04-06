using AutoMapper;
using Warehouse.Application.Models.Create;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductInputModel, Product>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<CategoryInputModel, Category>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        } 
    }
}
