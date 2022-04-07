using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Application.Models.View
{
    public class CategoryViewModel
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public IList<string>? ProductsIds { get; set; }
        public int? StockThreshold { get; set; }
    }
}
