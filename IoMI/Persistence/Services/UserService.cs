using IoMI.Application.Helpers;
using IoMI.Application.Services;
using IoMI.Domain.Entities.UserEntities;
using IoMI.Shared.Models.ServerResponseModels;
using IoMI.Shared.Models.UserModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace IoMI.Persistence.Services;

public class UserService : IUserService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IEmailService _emailService;

    public UserService(UserManager<AppUser> userManager, IEmailService emailService)
    {
        _userManager = userManager;
        _emailService = emailService;
    }


    public async Task<ServerResponse<bool>> CreateUserAsync(UserRegisterModel user)
    {
        AppUser? duplicateEmailUser = await _userManager.FindByEmailAsync(user.Email);
        if (duplicateEmailUser is not null)
            return new ServerResponse<bool>() { ErrorMessage = "An account with this email already exists.", Success = false };

        IdentityResult result = await _userManager.CreateAsync(new AppUser()
        {
            Id = user.Id.ToString() ?? Guid.NewGuid().ToString(),
            UserName = user.Username,
            Name = user.Name,
            Surname = user.Surname,
            Email = user.Email,
            IsActive = user.IsActive,
            Address = user.Address,
            CompanyName = user.CompanyName

        }, user.Password);
        if (!result.Succeeded || result.Errors.Any())
            return new ServerResponse<bool>() { ErrorMessage = result.Errors.FirstOrDefault()?.Code ?? "Unknown error.", Success = false };

        if (!string.IsNullOrEmpty(user.Email))
            return await SendEmailConfirmationTokenAsync(user.Email);

        return new ServerResponse<bool>() { ErrorMessage = "Something went wrong! Unknown error.", Success = false, Value = false };
    }

    public async Task<ServerResponse<bool>> SendEmailConfirmationTokenAsync(string email)
    {
        AppUser registeredUser = await _userManager.FindByEmailAsync(email);
        if (registeredUser is null || await _userManager.IsEmailConfirmedAsync(registeredUser))
            return new ServerResponse<bool>() { ErrorMessage = "This email not registered or already confirmed!", Success = false, Value = false };

        string token = await _userManager.GenerateEmailConfirmationTokenAsync(registeredUser);
        if (string.IsNullOrEmpty(token))
            return new ServerResponse<bool>() { ErrorMessage = "Something went wrong! Invalid confirmation token.", Success = false, Value = false };

        token = token.EncodeForUrl();
        await _emailService.SendEmailConfirmationTokenAsync(email, registeredUser.Id.ToString(), token);
        return new ServerResponse<bool>() { Success = true, Value = true };
    }

    public async Task<ServerResponse<bool>> ConfirmEmailAsync(Guid userId, string token)
    {
        AppUser user = await _userManager.FindByIdAsync(userId.ToString());
        if (user is null)
            return new() { Success = false, ErrorMessage = "This user not registered. Please check your link.", Value = false };

        token = token.DecodeFromUrl();

        IdentityResult result = await _userManager.ConfirmEmailAsync(user, token);
        if (!result.Succeeded || result.Errors.Any())
            return new() { Success = false, ErrorMessage = result.Errors.FirstOrDefault()?.Code ?? "This token is invalid.", Value = false };

        IdentityResult stampResult = await _userManager.UpdateSecurityStampAsync(user);
        if (!stampResult.Succeeded || stampResult.Errors.Any())
            return new ServerResponse<bool>() { Success = false, ErrorMessage = stampResult.Errors.FirstOrDefault()?.Code ?? "Something went wrong.", Value = false };

        return new() { Success = true, Value = true };
    }

    public async Task<ServerResponse<bool>> SendResetPasswordTokenAsync(string email)
    {
        AppUser user = await _userManager.FindByEmailAsync(email);
        if (user is null)
            return new ServerResponse<bool>() { Success = false, ErrorMessage = "This email not registered", Value = false };

        string token = await _userManager.GeneratePasswordResetTokenAsync(user);
        if (string.IsNullOrEmpty(token))
            return new ServerResponse<bool>() { ErrorMessage = "Something went wrong! Invalid reset token.", Success = false, Value = false };

        token = token.EncodeForUrl();
        await _emailService.SendResetPasswordTokenAsync(email, user.Id.ToString(), token);
        return new ServerResponse<bool>() { Success = true, Value = true };
    }

    public async Task<ServerResponse<bool>> VerifyResetTokenAsync(Guid userId, string token)
    {
        AppUser user = await _userManager.FindByIdAsync(userId.ToString());
        if (user is null)
            return new ServerResponse<bool>() { Success = false, ErrorMessage = "This link is broken. Please send new request.", Value = false };

        token = token.DecodeFromUrl();

        bool result = await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", token);
        return new ServerResponse<bool>() { Success = result, Value = result, ErrorMessage = !result ? "Invalid token!" : string.Empty };
    }

    public async Task<ServerResponse<bool>> ResetPasswordAsync(Guid userId, string token, string newPassword, string newPasswordConfirm)
    {
        AppUser user = await _userManager.FindByIdAsync(userId.ToString());
        if (user is null || userId == Guid.Empty || string.IsNullOrEmpty(token.Trim()) || string.IsNullOrEmpty(newPassword.Trim()) || !newPassword.Equals(newPasswordConfirm))
            return FailedResponse();

        token = token.DecodeFromUrl();

        IdentityResult result = await _userManager.ResetPasswordAsync(user, token, newPassword);

        if (!result.Succeeded || result.Errors.Any())
            return FailedResponse(result.Errors.ToString() ?? "Invalid token. Please request new one.");

        await _userManager.UpdateSecurityStampAsync(user);
        return new() { Success = result.Succeeded, Value = result.Succeeded };
    }





    public bool DeleteUser(UserRegisterModel user)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteUserAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdatePasswordAsync(Guid id, string password, string resetToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateUserAsync(UserRegisterModel user)
    {
        throw new NotImplementedException();
    }

    public ServerResponse<bool> FailedResponse(string error = "Bad request.") =>
        new() { ErrorMessage = error, Success = false, Value = false };
}