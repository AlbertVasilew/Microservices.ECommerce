using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace Auth.Handlers.Login
{
    public class LoginRequest : IRequest<string>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}