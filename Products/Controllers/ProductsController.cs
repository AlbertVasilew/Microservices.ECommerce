using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Products.Handlers.Delete;
using Products.Handlers.Get;
using Products.Handlers.GetById;
using Products.Handlers.Upsert;

namespace Products.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator mediator;

        public ProductsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await mediator.Send(new GetRequest()));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await mediator.Send(new GetByIdRequest { Id = id }));
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Upsert(UpsertRequest upsertRequest)
            => Ok(await mediator.Send(upsertRequest));

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int id)
            => Ok(await mediator.Send(new DeleteRequest { Id = id }));
    }
}
