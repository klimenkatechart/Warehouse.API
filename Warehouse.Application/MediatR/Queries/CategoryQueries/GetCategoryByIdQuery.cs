using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.MediatR.Queries.CategoryQueries
{
    public class GetCategoryByIdQuery : IRequest<Category>
    {
        public string Id { get; set; } = string.Empty;
    }
}
