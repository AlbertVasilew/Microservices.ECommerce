using Auth.Data;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Auth.Handlers.Register
{
    public class RegisterHandler : IRequestHandler<RegisterRequest, IdentityResult>
    {
        private readonly AppDbContext dbContext;
        private readonly UserManager<IdentityUser> userManager;

        public RegisterHandler(AppDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        public async Task<IdentityResult> Handle(RegisterRequest request, CancellationToken cancellationToken)
        {
            return await userManager.CreateAsync(
                new IdentityUser { UserName = request.Username, Email = request.Email }, request.Password);
        }
    }
}