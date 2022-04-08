using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Domain.Enums;

namespace Warehouse.Application.Models.View
{
    public class OrderViewModel
    {
        public string? ProductId { get; set; }
        public int AmmountOfItems { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; }
    }
}
