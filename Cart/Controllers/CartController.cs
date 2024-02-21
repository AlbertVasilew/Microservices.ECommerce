using Cart.Handlers.Upsert;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IMediator mediator;

        public CartController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Upsert([FromBody] UpsertRequest upsertRequest)
        {
            return Ok(await mediator.Send(upsertRequest));
        }
    }
}
