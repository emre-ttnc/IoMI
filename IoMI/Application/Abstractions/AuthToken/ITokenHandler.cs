using IoMI.Domain.Entities.UserEntities;
using System.Security.Claims;

namespace IoMI.Application.Abstractions.AuthToken;

public interface ITokenHandler
{
    string CreateAccessToken(int tokenLifeTimeSecond, AppUser user, ICollection<Claim> roles);
}