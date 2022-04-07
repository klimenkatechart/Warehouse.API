using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Application.Models.Create;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.MediatR.Commands.CategoryCommands
{
    public class InsertCategoryCommand : IRequest<Category>
    {
        public CategoryInputModel InputModel { get; set; } = new CategoryInputModel();
    }
}
