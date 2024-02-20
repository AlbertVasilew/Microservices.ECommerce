using Microsoft.AspNetCore.Identity;

namespace Auth.Contracts
{
    public interface IJwtTokenGenerator
    {
        string Generate(IdentityUser user, IList<string> roles);
    }
}