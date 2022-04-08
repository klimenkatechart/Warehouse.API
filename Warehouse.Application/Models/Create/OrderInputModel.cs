using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Application.Models.Create
{
    public class OrderInputModel
    {
        public string? ProductId { get; set; }
        public int AmmountOfItems { get; set; }
    }
}
