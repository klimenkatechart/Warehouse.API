using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Domain.Entities;

namespace Warehouse.Application.Models.Create
{
    public class ProductInputModel
    {
        [Required(ErrorMessage = "Please provide a product name")]
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Ammount { get; set; }

        [Required (ErrorMessage = "Product should contain a cathegory")]
        public string? CategoryId { get; set; }
    }
}
