using IoMI.Application.Repositories.ApplicationRepositories;
using IoMI.Application.Repositories.InstrumentRepositories;
using IoMI.Application.Services;
using IoMI.Domain.Entities.InstrumentEntities;
using IoMI.Domain.Entities.UserEntities;
using IoMI.Shared.Models.ApplicationModels;
using IoMI.Shared.Models.InstrumentModels;
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
    private readonly IScaleInspectionApplicationWriteRepository _scaleInspectionApplicationWriteRepository;
    private readonly IGasMeterInspectionApplicationWriteRepository _gasMeterInspectionApplicationWriteRepository;
    private readonly IScaleReadRepository _scaleReadRepository;
    private readonly IGasMeterReadRepository _gasMeterReadRepository;

    public ApplicationService(IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager, IScaleInspectionApplicationReadRepository scaleInspectionApplicationReadRepository, IGasMeterInspectionApplicationReadRepository gasMeterInspectionApplicationReadRepository, IScaleInspectionApplicationWriteRepository scaleInspectionApplicationWriteRepository, IScaleReadRepository scaleReadRepository, IGasMeterReadRepository gasMeterReadRepository, IGasMeterInspectionApplicationWriteRepository gasMeterInspectionApplicationWriteRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
        _scaleInspectionApplicationReadRepository = scaleInspectionApplicationReadRepository;
        _gasMeterInspectionApplicationReadRepository = gasMeterInspectionApplicationReadRepository;
        _scaleInspectionApplicationWriteRepository = scaleInspectionApplicationWriteRepository;
        _scaleReadRepository = scaleReadRepository;
        _gasMeterReadRepository = gasMeterReadRepository;
        _gasMeterInspectionApplicationWriteRepository = gasMeterInspectionApplicationWriteRepository;
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
                Applicant = new() { Name = user.Name, Surname = user.Surname },
                ApplicationDate = DateTime.Parse(app.ApplicationDate.ToString()),
                IsAccepted = app.IsAccepted,
                IsCompleted = app.IsCompleted
            }).ToListAsync();

        return new() { Success = true, Value = scaleInspectionApplications };
    }

    public async Task<ServerResponse<bool>> AddNewScaleInspectionApplication(List<ScaleModel> scales)
    {
        AppUser? user = await GetAuthUser();
        if (user is null)
            return FailedResponse("Unauthorized request.");

        List<Scale> scaleList = new();
        foreach (var s in scales)
        {
            Scale? scale = await _scaleReadRepository.GetByIdAsync(s.Id);
            if (scale is not null)
                scaleList.Add(scale);
        }

        if (!scaleList.Any())
            return FailedResponse();

        bool result = await _scaleInspectionApplicationWriteRepository.AddAsync(new()
        {
            Id = Guid.NewGuid(),
            Applicant = user,
            Scales = scaleList,
            ApplicationDate = DateOnly.FromDateTime(DateTime.UtcNow),
            IsAccepted = false,
            IsCompleted = false
        });

        await _scaleInspectionApplicationWriteRepository.SaveAsync();

        return new() { Success = result, Value = result };
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
                Applicant = new() { Name = user.Name, Surname = user.Surname},
                ApplicationDate = DateTime.Parse(app.ApplicationDate.ToString()),
                IsAccepted = app.IsAccepted,
                IsCompleted = app.IsCompleted
            }).ToListAsync();
        return new() { Success = true, Value = gasMeterInspectionApplications };
    }

    public async Task<ServerResponse<bool>> AddNewGasMeterInspectionApplication(List<GasMeterModel> gasMeters)
    {
        AppUser? user = await GetAuthUser();
        if (user is null)
            return FailedResponse("Unauthorized request.");

        List<GasMeter> gasMeterList = new();
        foreach (var g in gasMeters)
        {
            GasMeter? gasMeter = await _gasMeterReadRepository.GetByIdAsync(g.Id);
            if (gasMeter is not null)
                gasMeterList.Add(gasMeter);
        }

        if (!gasMeterList.Any())
            return FailedResponse();

        bool result = await _gasMeterInspectionApplicationWriteRepository.AddAsync(new()
        {
            Id = Guid.NewGuid(),
            Applicant = user,
            GasMeters = gasMeterList,
            ApplicationDate = DateOnly.FromDateTime(DateTime.UtcNow),
            IsAccepted = false,
            IsCompleted = false
        });

        await _gasMeterInspectionApplicationWriteRepository.SaveAsync();

        return new() { Success = result, Value = result };
    }

    public Task<ServerResponse<bool>> AcceptGasMeterInspectionApplication()
    {
        throw new NotImplementedException();
    }

    public Task<ServerResponse<bool>> CompleteGasMeterInspectionApplication()
    {
        throw new NotImplementedException();
    }

    public ServerResponse<bool> FailedResponse(string error = "Bad request.") =>
        new() { ErrorMessage = error, Success = false, Value = false };

}
