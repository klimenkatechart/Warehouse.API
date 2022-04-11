using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Application.Interfaces.Repositories;
using Warehouse.Application.MediatR.Commands.OrderCommands;
using Warehouse.Application.Models;
using Warehouse.Application.Models.Create;
using Warehouse.Application.SAGA.Commands;
using Warehouse.Application.SAGA.Consumers;
using Warehouse.Domain.Entities;

namespace Warehouse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IOrderRepository _orderRepository;
        private readonly IRequestClient<OrderApproveCommand> _client;
        private readonly IPublishEndpoint _publishEndpoint;


        public OrderController(IMediator mediator, IOrderRepository orderRepository, IRequestClient<OrderApproveCommand> client, IPublishEndpoint publishEndpoint)
        {
            _mediator = mediator;
            _client = client;
            _orderRepository = orderRepository;
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost("Submit/{isWaitForStock}")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderInputModel inputModel, bool isWaitForStock)
        {
            var status = await _mediator.Send(new CreateOrderCommand() { inputModel = inputModel, isWaitForStock = isWaitForStock });
            
            return Ok(status);
        }

        [HttpPost("Accept/{id}")]
        public async Task<IActionResult> AcceptOrder(string id, [FromForm] bool accept)
        {
           var status =  await _client.GetResponse<StatusResult>(new OrderApproveCommand() { OrderId = id, isApproved = accept, CorrelationId = NewId.NextGuid() });
           return Ok(status.Message);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetOrders()
        {
            return Ok(await _orderRepository.GetAll(CancellationToken.None));
        }

        [HttpGet("in-review")]
        public async Task<IActionResult> GetInReviewOrders()
        {
            return Ok(await _orderRepository.GetAllOrdersInReview(CancellationToken.None));
        }
    }
}
