using IoMI.Shared.Models.ServerResponseModels;
using IoMI.Shared.Models.UserModels;

namespace IoMI.Application.Services;

public interface IUserService
{
    Task<ServerResponse<bool>> CreateUserAsync(UserRegisterModel user);
    Task<ServerResponse<bool>> ConfirmEmailAsync(Guid userId, string token);
    Task<ServerResponse<bool>> SendEmailConfirmationTokenAsync(string email);
    Task<ServerResponse<bool>> SendResetPasswordTokenAsync(string email);
    Task<ServerResponse<bool>> VerifyResetTokenAsync(Guid userId, string token);
    Task<ServerResponse<bool>> ResetPasswordAsync(Guid userId, string token, string newPassword, string newPasswordConfirm);
    Task<bool> UpdateUserAsync(UserRegisterModel user);
    bool DeleteUser(UserRegisterModel user);
    Task<bool> DeleteUserAsync(Guid id);
    Task<ServerResponse<bool>> UpdatePasswordAsync(string password, string newPassword);

    ServerResponse<bool> FailedResponse(string error = "Bad request.");
}