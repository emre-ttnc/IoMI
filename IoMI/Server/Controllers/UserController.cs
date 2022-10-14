using IoMI.Application.Services;
using IoMI.Shared.Models.ServerResponseModels;
using IoMI.Shared.Models.UserModels;
using Microsoft.AspNetCore.Mvc;

namespace IoMI.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<ServerResponse<bool>> CreateUserAsync([FromBody] UserModel request)
    {
        if (request is null)
            return new ServerResponse<bool>() { ErrorMessage = "Something went wrong!", Success = false, Value = false };

        return await _userService.CreateUserAsync(request);
    }

    [HttpPost("confirmemail")]
    public async Task<ServerResponse<bool>> ConfirmEmailAsync([FromBody] ConfirmEmailModel request)
    {
        if (request is null || request.UserId == Guid.Empty || string.IsNullOrEmpty(request.Token))
            return new ServerResponse<bool>() { ErrorMessage = "Bad request!", Success = false, Value = false};

        return await _userService.ConfirmEmailAsync(request.UserId, request.Token);
    }
}