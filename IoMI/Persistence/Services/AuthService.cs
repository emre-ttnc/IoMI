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
    private readonly ITokenHandler _tokenHandler;
    private readonly RoleManager<UserRole> _roleManager;

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

        if (!user.IsActive)
            return FailedResponse("This account is not active.");

        ICollection<string> roles = await _userManager.GetRolesAsync(user);
        ICollection<Claim> claims = await _userManager.GetClaimsAsync(user);
        if (roles.Any())
            claims.Add(new Claim(ClaimTypes.Role, roles.First()));
        claims.Add(new(ClaimTypes.Name, user.UserName));
        claims.Add(new(ClaimTypes.UserData, $"{user.Name} {user.Surname}"));

        return new() { Success = true, Value = _tokenHandler.CreateAccessToken(60 * 15, user, claims) };
    }

    public async Task<ServerResponse<bool>> ChangeRoleAsync(ChangeRoleModel request)
    {
        AppUser user = await _userManager.FindByIdAsync(request.Id);
        if (user is null)
            return new() { ErrorMessage = "User not found", Success = false };

        if (!await _roleManager.RoleExistsAsync(request.RoleName))
            await _roleManager.CreateAsync(new() { Name = request.RoleName });

        if (await _userManager.IsInRoleAsync(user, request.RoleName))
            return new() { ErrorMessage = $"This user already has {request.RoleName} role", Success = false };

        await _userManager.RemoveFromRolesAsync(user, await _userManager.GetRolesAsync(user));
        IdentityResult result = await _userManager.AddToRoleAsync(user, request.RoleName);
        if(!result.Succeeded || result.Errors.Any())
            return new() { ErrorMessage = result.Errors.FirstOrDefault()?.ToString() ?? "Unknown adding role error.", Success = false };

        return new() { Success = result.Succeeded, Value = result.Succeeded };

    }

    public ServerResponse<string> FailedResponse(string error = "Bad request.") =>
        new() { ErrorMessage = error, Success = false, Value = " " };
}