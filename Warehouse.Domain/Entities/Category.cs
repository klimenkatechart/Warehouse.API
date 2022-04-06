using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public IList<Product>? Products { get; set; }
        public int? StockThreshold { get; set; }
    }
}
