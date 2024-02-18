using Auth.Handlers.Login;
using Auth.Handlers.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator mediator;

        public AuthController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            var result = await mediator.Send(registerRequest);
            return result.Errors.Any() ? BadRequest(result.Errors) : Ok();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            var token = await mediator.Send(loginRequest);
            return !string.IsNullOrEmpty(token) ? Ok(token) : BadRequest();
        }
    }
}
