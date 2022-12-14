using IoMI.Application.Services;
using IoMI.Shared.Models.ServerResponseModels;
using IoMI.Shared.Models.UserModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
    public async Task<ServerResponse<bool>> CreateUserAsync([FromBody] UserRegisterModel request)
    {
        if (request is null)
            return _userService.FailedResponse("Bad request!");

        return await _userService.CreateUserAsync(request);
    }

    [HttpPost("confirmemail")]
    public async Task<ServerResponse<bool>> ConfirmEmail([FromBody] ConfirmEmailModel request)
    {
        if (request is null || request.UserId == Guid.Empty || string.IsNullOrEmpty(request.Token))
            return _userService.FailedResponse("Bad request!");

        return await _userService.ConfirmEmailAsync(request.UserId, request.Token);
    }

    [HttpPost("sendconfirmemail")]
    public async Task<ServerResponse<bool>> SendConfirmEmail([FromBody] EmailModel request)
    {
        if (request is null || string.IsNullOrEmpty(request.Email))
            return _userService.FailedResponse("Bad request!");

        return await _userService.SendEmailConfirmationTokenAsync(request.Email);
    }

    [HttpPost("sendresetpasswordtoken")]
    public async Task<ServerResponse<bool>> SendResetPasswordToken([FromBody] EmailModel request)
    {
        if (request is null || string.IsNullOrEmpty(request.Email))
            return _userService.FailedResponse("Bad request!");
        return await _userService.SendResetPasswordTokenAsync(request.Email);
    }

    [HttpPost("verifyresetpasswordtoken")]
    public async Task<ServerResponse<bool>> VerifyResetPasswordToken([FromBody] VerifyResetTokenModel request)
    {
        if (request is null || string.IsNullOrEmpty(request.Token) || request.UserId == Guid.Empty)
            return _userService.FailedResponse("Bad request!");
        return await _userService.VerifyResetTokenAsync(request.UserId, request.Token);
    }

    [HttpPost("resetpassword")]
    public async Task<ServerResponse<bool>> ResetPassword([FromBody] ResetPasswordModel request)
    {
        if (request is null || string.IsNullOrEmpty(request.Password) || !request.Password.Equals(request.ConfirmPassword) || request.UserId == Guid.Empty || string.IsNullOrEmpty(request?.Token?.Trim()))
            return _userService.FailedResponse("Bad request or password does not match!");
        return await _userService.ResetPasswordAsync(request.UserId, request.Token, request.Password, request.ConfirmPassword);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost("changepassword")]
    public async Task<ServerResponse<bool>> ChangePassword([FromBody] ChangePasswordModel request)
    {
        if (request is null || string.IsNullOrEmpty(request.OldPassword?.Trim()) || string.IsNullOrEmpty(request.Password?.Trim()) || !request.Password.Equals(request.ConfirmPassword))
            return _userService.FailedResponse("Bad request or password does not match!");
        return await _userService.UpdatePasswordAsync(password: request.OldPassword, newPassword: request.Password);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "SystemAdmin")]
    [HttpGet("GetUsers")]
    public async Task<UserModel[]> GetUsers() => await _userService.GetAllUserOfInstrument();

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "SystemAdmin")]
    [HttpGet("GetInspectors")]
    public async Task<UserModel[]> GetInspectors() => await _userService.GetAllInspectors();

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "SystemAdmin")]
    [HttpPost("ChangeStatus")]
    public async Task<ServerResponse<bool>> ChangeStatus([FromBody] string id) => await _userService.ChangeStatus(id);

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet("GetUserInfo")]
    public async Task<ServerResponse<UpdateUserProfileModel>> GetUserInfo() => await _userService.GetUserInfo();

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost("UpdateProfile")]
    public async Task<ServerResponse<bool>> UpdateProfile([FromBody] UpdateUserProfileModel request)
    {
        if (request is null || string.IsNullOrEmpty(request.Name?.Trim()) || string.IsNullOrEmpty(request.Surname?.Trim()) || string.IsNullOrEmpty(request.Address?.Trim()) || string.IsNullOrEmpty(request.CompanyName?.Trim()) || string.IsNullOrEmpty(request.Id?.Trim()))
            return _userService.FailedResponse();
        return await _userService.UpdateUserAsync(request);
    }
}