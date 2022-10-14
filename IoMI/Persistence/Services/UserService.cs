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


    public async Task<ServerResponse<bool>> CreateUserAsync(UserModel user)
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
            await SendEmailConfirmationTokenAsync(user.Email);
        return new ServerResponse<bool>() { Success = true };
    }

    public async Task SendEmailConfirmationTokenAsync(string email)
    {
        AppUser registeredUser = await _userManager.FindByEmailAsync(email);
        string token = await _userManager.GenerateEmailConfirmationTokenAsync(registeredUser);
        if (!string.IsNullOrEmpty(token))
        {
            byte[] bytes = Encoding.UTF8.GetBytes(token);
            token = WebEncoders.Base64UrlEncode(bytes);
            await _emailService.SendEmailConfirmationTokenAsync(email, registeredUser.Id.ToString(), token);
        }
    }

    public async Task<ServerResponse<bool>> ConfirmEmailAsync(Guid userId, string token)
    {
        AppUser user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
            return new ServerResponse<bool>() { Success = false, ErrorMessage = "This user not registered. Please check your link.", Value = false };

        byte[] bytes = WebEncoders.Base64UrlDecode(token);
        token = Encoding.UTF8.GetString(bytes);

        IdentityResult result = await _userManager.ConfirmEmailAsync(user, token);
        if (!result.Succeeded || result.Errors.Any())
            return new ServerResponse<bool> { Success = false, ErrorMessage = result.Errors.FirstOrDefault()?.Code ?? "This token is invalid.", Value = false };

        IdentityResult stampResult = await _userManager.UpdateSecurityStampAsync(user);
        if (!stampResult.Succeeded || stampResult.Errors.Any())
            return new ServerResponse<bool>() { Success = false, ErrorMessage = stampResult.Errors.FirstOrDefault()?.Code ?? "Something went wrong.", Value = false };

        return new ServerResponse<bool>() { Success = true, Value = true };
    }






    public Task<bool> CreateInspectorAsync(UserModel inspector)
    {
        throw new NotImplementedException();
    }

    public bool DeleteInspector(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteInspectorAsync(UserModel inspector)
    {
        throw new NotImplementedException();
    }

    public bool DeleteUser(UserModel user)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteUserAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateInspectorAsync(UserModel inspector)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdatePasswordAsync(Guid id, string password, string resetToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateUserAsync(UserModel user)
    {
        throw new NotImplementedException();
    }
}