using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Application.MediatR.Commands.CategoryCommands
{
    public class DeleteCategoryCommand : IRequest
    {
        public string id { get; set; } = string.Empty;
    }
}
