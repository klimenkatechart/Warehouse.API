using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Domain.Entities
{
    public class Category : BaseEntity<Category>
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public IList<string> ProductsIds { get; set; } = new List<string>();
        public int? LowStock { get; set; }
        public int? OutOfStock { get; set; }
    }
}
