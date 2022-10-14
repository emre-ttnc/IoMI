using IoMI.Shared.Models.ServerResponseModels;
using IoMI.Shared.Models.UserModels;

namespace IoMI.Application.Services;

public interface IUserService
{
    Task<ServerResponse<bool>> CreateUserAsync(UserModel user);
    Task<ServerResponse<bool>> ConfirmEmailAsync(Guid userId, string token);
    Task<bool> UpdateUserAsync(UserModel user);
    bool DeleteUser(UserModel user);
    Task<bool> DeleteUserAsync(Guid id);
    Task<bool> UpdatePasswordAsync(Guid id, string password, string resetToken);

    Task<bool> CreateInspectorAsync(UserModel inspector);
    Task<bool> UpdateInspectorAsync(UserModel inspector);
    Task<bool> DeleteInspectorAsync(UserModel inspector);
    bool DeleteInspector(Guid id);
}