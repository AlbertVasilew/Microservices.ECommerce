using MediatR;
using MessageBus;
using Microsoft.AspNetCore.Identity;

namespace Auth.Handlers.Register
{
    public class RegisterHandler : IRequestHandler<RegisterRequest, IdentityResult>
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IMessageBusSender messageBusSender;
        private readonly IConfiguration configuration;

        public RegisterHandler(
            UserManager<IdentityUser> userManager, IMessageBusSender messageBusSender, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.messageBusSender = messageBusSender;
            this.configuration = configuration;
        }

        public async Task<IdentityResult> Handle(RegisterRequest request, CancellationToken cancellationToken)
        {
            var identityResult = await userManager.CreateAsync(
                new IdentityUser { UserName = request.Username, Email = request.Email }, request.Password);

            if (identityResult.Succeeded)
            {
                messageBusSender.SendMessage(
                    request.Email, configuration.GetValue<string>("MessageBusQueues:AuthRegistration"));
            }

            return identityResult;
        }
    }
}