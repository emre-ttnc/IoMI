using IoMI.Shared.Models.ServerResponseModels;
using IoMI.Shared.Models.UserModels;

namespace IoMI.Application.Services;

public interface IAuthService
{
    Task<ServerResponse<string>> LoginAsync(UserLoginModel request);

    ServerResponse<string> FailedResponse(string error = "Bad request.");
}