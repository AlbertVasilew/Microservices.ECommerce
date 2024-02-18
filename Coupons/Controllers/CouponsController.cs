using Coupons.Handlers.Delete;
using Coupons.Handlers.Get;
using Coupons.Handlers.GetById;
using Coupons.Handlers.Upsert;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Coupons.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponsController : ControllerBase
    {
        private readonly IMediator mediator;

        public CouponsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
            => Ok(await mediator.Send(new GetRequest()));

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
            => Ok(await mediator.Send(new GetByIdRequest { Id = id }));

        [HttpPost]
        public async Task<IActionResult> Upsert(UpsertRequest upsertRequest)
            => Ok(await mediator.Send(upsertRequest));

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
            => Ok(await mediator.Send(new DeleteRequest { Id = id }));
    }
}
