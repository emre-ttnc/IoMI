using IoMI.Application.Abstractions.AuthToken;
using IoMI.Application.Services;
using IoMI.Domain.Entities.UserEntities;
using IoMI.Shared.Models.ServerResponseModels;
using IoMI.Shared.Models.UserModels;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace IoMI.Persistence.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly RoleManager<UserRole> _roleManager;
    private readonly ITokenHandler _tokenHandler;

    public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenHandler tokenHandler, RoleManager<UserRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenHandler = tokenHandler;
        _roleManager = roleManager;
    }

    public async Task<ServerResponse<string>> LoginAsync(UserLoginModel request)
    {
        AppUser user = await _userManager.FindByNameAsync(request.Username);
        if (user is null)
            return FailedResponse("Username or password is wrong.");

        if (!user.EmailConfirmed)
            return FailedResponse("Email is not confirmed yet. Please confirm your e-mail.");

        SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        if (result is null || !result.Succeeded)
            return FailedResponse("Username or password is invalid.");

        ICollection<string> roles = await _userManager.GetRolesAsync(user);
        ICollection<Claim> claims = await _userManager.GetClaimsAsync(user);
        if (roles.Any())
            claims.Add(new Claim(ClaimTypes.Role, roles.First()));
        claims.Add(new(ClaimTypes.Name, user.UserName));
        claims.Add(new(ClaimTypes.UserData, $"{user.Name} {user.Surname}"));

        return new() { Success = true, Value = _tokenHandler.CreateAccessToken(60 * 15, user, claims) };
    }

    public ServerResponse<string> FailedResponse(string error = "Bad request.") =>
        new() { ErrorMessage = error, Success = false, Value = " " };
}