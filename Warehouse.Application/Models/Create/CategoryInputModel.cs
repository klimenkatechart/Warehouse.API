using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Application.Models.Create
{
    public class CategoryInputModel
    {
        [Required(ErrorMessage = "Please provide a product name")]
        public string? Name { get; set; }
        public string? Description { get; set; }

        [Required(ErrorMessage = "Please provide a Stock Threshold")]
        public int StockThreshold { get; set; }


    }
}
