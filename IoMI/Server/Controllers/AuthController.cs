using IoMI.Application.Services;
using IoMI.Shared.Models.ServerResponseModels;
using IoMI.Shared.Models.UserModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IoMI.Server.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<ServerResponse<string>> Login(UserLoginModel request)
    {
        if (string.IsNullOrEmpty(request?.Username?.Trim()) || string.IsNullOrEmpty(request?.Password?.Trim()))
            return _authService.FailedResponse("Username or password cannot be empty.");
        return await _authService.LoginAsync(request);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "SystemAdmin")]
    [HttpPost("ChangeRole")]
    public async Task<ServerResponse<bool>> ChangeRole(ChangeRoleModel request)
    {
        if (string.IsNullOrEmpty(request.Id.Trim()) || string.IsNullOrEmpty(request.RoleName.Trim()))
            return new() { ErrorMessage = "Bad request. Id or role can not be null or empty", Success = false };
        return await _authService.ChangeRoleAsync(request);
    }
}
