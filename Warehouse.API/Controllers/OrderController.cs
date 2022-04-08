using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Warehouse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public OrderController()
        {

        }

        [HttpPost]
        public IActionResult CreateOrder()
        {
            // It must validate if product exists and validate stock
            // (if all good -> instantly return created product and order) 
            // (if lowstock -> return created order with status pending ; send message to rabbitMq and orderProcess service)
            // (if out of stock -> rerurn order rejected (out of stock) or return order pending and send message to orderProcess service so it will add items)

            return Ok();
        }
    }
}
