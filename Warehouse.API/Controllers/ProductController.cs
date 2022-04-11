using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Application.MediatR.Commands.ProductCommands;
using Warehouse.Application.MediatR.Queries.ProductQueries;
using Warehouse.Application.Models.Create;


namespace Warehouse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(string id)
        {
            return Ok(await _mediator.Send(new GetProductByIdQuery() { Id = id }));
        }

        [HttpGet("all")]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllProductsQuery()));
        }

        [HttpPost("items/{id}")]
        public async Task<ActionResult> Insert(string id, [FromBody] int Ammount)
        {
            return Ok(await _mediator.Send(new SetItemsForProduct() { ProductId = id, Ammount = Ammount}));
        }
        [HttpPost("add-items/{id}")]
        public async Task<ActionResult> AddItems(string id, [FromBody] int Ammount)
        {
            return Ok(await _mediator.Send(new AddItemsForproductCommand() { ProductId = id, Ammount = Ammount }));
        }
        [HttpPost("insert")]
        public async Task<ActionResult> Insert([FromBody] ProductInputModel inputModel)
        {           
          return Ok(await _mediator.Send(new InsertProductCommand() { InputModel = inputModel }));
        }
      
        [HttpPut("update/{id}")]
        public async Task<ActionResult> Update(string id, [FromBody] ProductInputModel inputModel)
        {
            return Ok(await _mediator.Send(new UpdateProductCommand() { Id = id, ProductInput = inputModel }));
        }
    }
}
