using Auth.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Auth.Services
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly IConfigurationSection jwtOptions;

        public JwtTokenGenerator(IConfiguration configuration)
        {
            jwtOptions = configuration.GetSection("JwtOptions");
        }

        public string Generate(IdentityUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id)
            };

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Audience = jwtOptions.GetValue<string>("Audience"),
                Issuer = jwtOptions.GetValue<string>("Issuer"),
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtOptions.GetValue<string>("Secret"))),
                    SecurityAlgorithms.HmacSha256Signature)
            });

            return tokenHandler.WriteToken(token);
        }
    }
}