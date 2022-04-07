using MediatR;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Application.MediatR.Commands.CategoryCommands;
using Warehouse.Application.MediatR.Queries.CategoryQueries;
using Warehouse.Application.Models.Create;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Warehouse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(string id)
        {
            return Ok(await _mediator.Send(new GetCategoryByIdQuery() { Id = id }));
        }

        [HttpGet("all")]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllCategoriesQuery()));
        }

        [HttpPost("insert")]
        public async Task<ActionResult> Insert([FromBody] CategoryInputModel inputModel)
        {
            return Ok(await _mediator.Send(new InsertCategoryCommand() { InputModel = inputModel }));
        }

        [HttpPost("insert-range")]
        public async Task<ActionResult> InsertRange([FromBody] IList<CategoryInputModel> inputModel)
        {
            return Ok(await _mediator.Send(new InsertRangeOfCategoriesCommand() { InputModels = inputModel }));
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult> Update(string id, [FromBody] CategoryInputModel inputModel)
        {
            return Ok(await _mediator.Send(new UpdateCategoryCommand() { Id = id, ProductInput = inputModel }));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await _mediator.Send(new DeleteCategoryCommand() { id = id });
            return NoContent();
        }
    }
}
