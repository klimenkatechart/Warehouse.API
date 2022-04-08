using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Domain.Enums;

namespace Warehouse.Domain.Entities
{
    public class Order : BaseEntity
    {
        public string? ProductId { get; set; }
        public int AmmountOfItems { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; }
    }
}
