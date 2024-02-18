using Auth.Contracts;
using Auth.Data;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Auth.Handlers.Login
{
    public class LoginHandler : IRequestHandler<LoginRequest, string>
    {
        private readonly AppDbContext dbContext;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IJwtTokenGenerator jwtTokenGenerator;

        public LoginHandler(
            AppDbContext dbContext, UserManager<IdentityUser> userManager, IJwtTokenGenerator jwtTokenGenerator)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<string> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            var user = await dbContext.Users
                .FirstOrDefaultAsync(x => x.UserName.ToLower() == request.Username.ToLower(), cancellationToken);

            return await userManager.CheckPasswordAsync(user, request.Password)
                ? jwtTokenGenerator.Generate(user) : string.Empty;
        }
    }
}