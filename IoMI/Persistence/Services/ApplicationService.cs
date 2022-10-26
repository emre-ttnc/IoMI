using IoMI.Application.Repositories.ApplicationRepositories;
using IoMI.Application.Services;
using IoMI.Domain.Entities.UserEntities;
using IoMI.Shared.Models.ApplicationModels;
using IoMI.Shared.Models.ServerResponseModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IoMI.Persistence.Services;

public class ApplicationService : IApplicationService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<AppUser> _userManager;
    private readonly IScaleInspectionApplicationReadRepository _scaleInspectionApplicationReadRepository;
    private readonly IGasMeterInspectionApplicationReadRepository _gasMeterInspectionApplicationReadRepository;

    public ApplicationService(IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager, IScaleInspectionApplicationReadRepository scaleInspectionApplicationReadRepository, IGasMeterInspectionApplicationReadRepository gasMeterInspectionApplicationReadRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
        _scaleInspectionApplicationReadRepository = scaleInspectionApplicationReadRepository;
        _gasMeterInspectionApplicationReadRepository = gasMeterInspectionApplicationReadRepository;
    }

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

    public async Task<ServerResponse<List<ScaleInspectionApplicationModel>>> GetScaleInspectionApplications()
    {
        AppUser? user = await GetAuthUser();
        if (user is null)
            return new() { ErrorMessage = "Unathorized request", Success = false };

        List<ScaleInspectionApplicationModel> scaleInspectionApplications = await _scaleInspectionApplicationReadRepository.Table.AsNoTracking().Where(app => app.Applicant == user)
            .Select(app => new ScaleInspectionApplicationModel()
            {
                Id = app.Id,
                ApplicationDate = app.ApplicationDate,
                IsAccepted = app.IsAccepted,
                IsCompleted = app.IsCompleted
            }).ToListAsync();

        return new() { Success = true, Value = scaleInspectionApplications };
    }

    public Task<ServerResponse<bool>> AddNewScaleInspectionApplication()
    {
        throw new NotImplementedException();
    }

    public Task<ServerResponse<bool>> AcceptScaleInspectionApplication()
    {
        throw new NotImplementedException();
    }

    public Task<ServerResponse<bool>> CompleteScaleInspectionApplication()
    {
        throw new NotImplementedException();
    }
    public async Task<ServerResponse<List<GasMeterInspectionApplicationModel>>> GetGasMeterInspectionApplications()
    {
        AppUser? user = await GetAuthUser();
        if (user is null)
            return new() { ErrorMessage = "Unauthorized request.", Success = false };

        List<GasMeterInspectionApplicationModel> gasMeterInspectionApplications = await _gasMeterInspectionApplicationReadRepository.Table.AsNoTracking().Where(app => app.Applicant == user)
            .Select(app => new GasMeterInspectionApplicationModel()
            {
                Id = app.Id,
                ApplicationDate = app.ApplicationDate,
                IsAccepted = app.IsAccepted,
                IsCompleted = app.IsCompleted
            }).ToListAsync();
        return new() { Success = true, Value = gasMeterInspectionApplications };
    }

    public Task<ServerResponse<bool>> AddNewGasMeterInspectionApplication()
    {
        throw new NotImplementedException();
    }

    public Task<ServerResponse<bool>> AcceptGasMeterInspectionApplication()
    {
        throw new NotImplementedException();
    }

    public Task<ServerResponse<bool>> CompleteGasMeterInspectionApplication()
    {
        throw new NotImplementedException();
    }
}
