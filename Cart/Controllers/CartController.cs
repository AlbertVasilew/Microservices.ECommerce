using Cart.Handlers.Get;
using Cart.Handlers.SetCoupon;
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

        [HttpGet]
        [Route("{userId}")]
        public async Task<IActionResult> GetCart(string userId)
        {
            return Ok(await mediator.Send(new GetCartRequest { UserId = userId }));
        }

        [HttpPost]
        [Route("upsert-cart")]
        public async Task<IActionResult> Upsert([FromBody] UpsertRequest upsertRequest)
        {
            return Ok(await mediator.Send(upsertRequest));
        }

        [HttpPost]
        [Route("set-coupon")]
        public async Task<IActionResult> SetCoupon([FromBody] SetCouponRequest setCouponRequest)
        {
            return Ok(await mediator.Send(setCouponRequest));
        }
    }
}