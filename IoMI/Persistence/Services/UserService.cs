using IoMI.Application.Helpers;
using IoMI.Application.Services;
using IoMI.Domain.Entities.UserEntities;
using IoMI.Shared.Models.ServerResponseModels;
using IoMI.Shared.Models.UserModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace IoMI.Persistence.Services;

public class UserService : IUserService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IEmailService _emailService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly RoleManager<UserRole> _roleManager;

    public UserService(UserManager<AppUser> userManager, IEmailService emailService, IHttpContextAccessor httpContextAccessor, RoleManager<UserRole> roleManager)
    {
        _userManager = userManager;
        _emailService = emailService;
        _httpContextAccessor = httpContextAccessor;
        _roleManager = roleManager;
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

        if (!await AddRoleAsync(await _userManager.FindByNameAsync(user.Username), "User"))
            return FailedResponse("Role adding failed.");

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

        user.IsActive = true;
        await _userManager.UpdateAsync(user);
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

    public async Task<ServerResponse<bool>> UpdatePasswordAsync(string password, string newPassword)
    {
        string username = _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "";
        if (string.IsNullOrEmpty(username.Trim()))
            return FailedResponse("Unauthorized request. Denied!");

        AppUser user = await _userManager.FindByNameAsync(username);
        if (user is null)
            return FailedResponse("User not found.");

        IdentityResult result = await _userManager.ChangePasswordAsync(user, password, newPassword);
        if (!result.Succeeded || result.Errors.Any())
            return FailedResponse(result.Errors?.FirstOrDefault()?.Description ?? "Unknown error.");

        return new() { Success = result.Succeeded, Value = result.Succeeded };
    }

    public async Task<UserModel[]> GetAllUserOfInstrument()
    {
        UserModel[] users = await GetAllUsers();
        return users.Where(user => user.Role == "User").ToArray();
    }

    public async Task<UserModel[]> GetAllInspectors()
    {
        UserModel[] users = await GetAllUsers();
        return users.Where(user => user.Role == "Inspector").ToArray();
    }

    public async Task<ServerResponse<bool>> ChangeStatus(string id)
    {
        AppUser user = await _userManager.FindByIdAsync(id);
        if (user is null)
            return FailedResponse("User not found.");

        user.IsActive = !user.IsActive;
        
        return await UpdateUserManagerAsync(user);
    }

    public async Task<ServerResponse<UpdateUserProfileModel>> GetUserInfo()
    {
        AppUser? user = await GetAuthUser();
        if (user is null)
            return new() { ErrorMessage = "Bad request. You're not authorized.", Success = false };

        return new() { Success = true, Value = new() { Id = user.Id, Name = user.Name, Surname = user.Surname, Address = user.Address ?? "", CompanyName = user.CompanyName ?? "" } };
    }

    public async Task<ServerResponse<bool>> UpdateUserAsync(UpdateUserProfileModel updateUser)
    {
        AppUser? user = await _userManager.FindByIdAsync(updateUser.Id);
        if (user is null)
            return FailedResponse("User not found!");

        AppUser? loggedUser = await GetAuthUser();
        if (loggedUser is null || loggedUser.Id != user.Id)
            return FailedResponse("You're not authorized to this.");

        user.Name = updateUser.Name;
        user.Surname = updateUser.Surname;
        user.CompanyName = updateUser.CompanyName;
        user.Address = updateUser.Address;

        return await UpdateUserManagerAsync(user);
    }





    public Task<bool> DeleteUserAsync(Guid id)
    {
        throw new NotImplementedException();
    }


    public bool DeleteUser(UserRegisterModel user)
    {
        throw new NotImplementedException();
    }

    public ServerResponse<bool> FailedResponse(string error = "Bad request.") =>
        new() { ErrorMessage = error, Success = false, Value = false };

    private async Task<AppUser?> GetAuthUser()
    {
        string? username = _httpContextAccessor.HttpContext?.User?.Identity?.Name;
        if (string.IsNullOrEmpty(username?.Trim()))
            return null;

        AppUser user = await _userManager.FindByNameAsync(username);
        if (user is null)
            return null;

        return user;
    }

    private async Task<UserModel[]> GetAllUsers()
    {
        UserModel[] users = _userManager.Users.Select(user => new UserModel()
        {
            Id = user.Id,
            Username = user.ToString(),
            Name = user.Name ?? "",
            Surname = user.Surname ?? "",
            Email = user.Email,
            IsActive = user.IsActive,
            Address = user.Address ?? "",
            RegistryCode = user.RegistryCode,
            CompanyName = user.CompanyName ?? ""
        }).ToArray();
        foreach (UserModel user in users)
        {
            ICollection<string> roles = await _userManager.GetRolesAsync(await _userManager.FindByIdAsync(user.Id));
            user.Role = roles.FirstOrDefault();
        }

        return users;
    }

    private async Task<ServerResponse<bool>> UpdateUserManagerAsync(AppUser user)
    {
        IdentityResult result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded || result.Errors.Any())
            return FailedResponse(result.Errors?.FirstOrDefault()?.Description ?? "Unknown error.");

        return new() { Success = result.Succeeded, Value = result.Succeeded };
    }

    private async Task<bool> AddRoleAsync(AppUser user, string role)
    {
        if (!await _roleManager.RoleExistsAsync(role))
            await _roleManager.CreateAsync(new() { Name = role });

        IdentityResult result = await _userManager.AddToRoleAsync(user, role);
        return result.Succeeded;
    }
}