using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Application.Models.Create
{
    public class OrderInputModel
    {
        [Required(ErrorMessage = "Please provide a product Id")]
        public string ProductId { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please provide an  Ammount" )]
        public int AmmountOfItems { get; set; }
    }
}
