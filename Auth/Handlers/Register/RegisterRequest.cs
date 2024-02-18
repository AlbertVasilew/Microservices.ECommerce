using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Auth.Handlers.Register
{
    public class RegisterRequest : IRequest<IdentityResult>
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}