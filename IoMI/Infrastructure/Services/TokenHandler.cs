using IoMI.Application.Abstractions.AuthToken;
using IoMI.Domain.Entities.UserEntities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IoMI.Infrastructure.Services;

public class TokenHandler : ITokenHandler
{
    private readonly IConfiguration _configuration;

    public TokenHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string CreateAccessToken(int tokenLifeTimeSecond, AppUser user, ICollection<Claim> roles)
    {
        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));
        SigningCredentials credentials = new(securityKey, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken token = new(
            audience: _configuration["Token:Audience"],
            issuer: _configuration["Token:Issuer"],
            expires: DateTime.UtcNow.AddSeconds(tokenLifeTimeSecond),
            notBefore: DateTime.UtcNow,
            signingCredentials: credentials,
            claims: roles
            );
        JwtSecurityTokenHandler tokenHandler = new();
        return tokenHandler.WriteToken(token);
    }
}