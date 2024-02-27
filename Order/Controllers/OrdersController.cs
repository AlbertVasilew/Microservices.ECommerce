using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderAPI.Handlers.PlaceOrder;

namespace OrderAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator mediator;

        public OrdersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] PlaceOrderRequest placeOrderRequest)
        {
            return Ok(await mediator.Send(placeOrderRequest));
        }
    }
}