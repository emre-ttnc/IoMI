using IoMI.Application.Repositories.InstrumentRepositories;
using IoMI.Application.Services;
using IoMI.Domain.Entities.InstrumentEntities;
using IoMI.Domain.Entities.UserEntities;
using IoMI.Shared.Models.InstrumentModels;
using IoMI.Shared.Models.ServerResponseModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IoMI.Persistence.Services;

public class InstrumentService : IInstrumentService
{
    private readonly IScaleReadRepository _scaleReadRepository;
    private readonly IScaleWriteRepository _scaleWriteRepository;
    private readonly IGasMeterReadRepository _gasMeterReadRepository;
    private readonly IGasMeterWriteRepository _gasMeterWriteRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<AppUser> _userManager;

    public InstrumentService(IScaleReadRepository scaleReadRepository,
                             IScaleWriteRepository scaleWriteRepository,
                             IGasMeterReadRepository gasMeterReadRepository,
                             IGasMeterWriteRepository gasMeterWriteRepository,
                             IHttpContextAccessor httpContextAccessor,
                             UserManager<AppUser> userManager)
    {
        _scaleReadRepository = scaleReadRepository;
        _scaleWriteRepository = scaleWriteRepository;
        _gasMeterReadRepository = gasMeterReadRepository;
        _gasMeterWriteRepository = gasMeterWriteRepository;
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
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

    public async Task<ServerResponse<bool>> CreateGasMeter(AddNewGasMeterModel gasMeter)
    {
        GasMeter? resultGasMeter = await _gasMeterReadRepository.Table.AsNoTracking().FirstOrDefaultAsync(gm => gm.Brand == gasMeter.Brand && gm.TypeOrModel == gasMeter.TypeOrModel && gm.SerialNumber == gasMeter.SerialNumber);
        if (resultGasMeter is not null)
            return FailedResponse("This gas meter already registered.");

        AppUser? user = await GetAuthUser();
        if (user is null)
            return FailedResponse("Unauthroized request.");

        bool result = await _gasMeterWriteRepository.AddAsync(new()
        {
            Id = gasMeter.Id,
            Brand = gasMeter.Brand,
            TypeOrModel = gasMeter.TypeOrModel,
            SerialNumber = gasMeter.SerialNumber,
            IsActive = true,
            LastInspectionYear = Convert.ToInt32(gasMeter.LastInspectionYear),
            UserOfInstrument = user,
            MaxFlowRate = gasMeter.MaxFlowRate,
        });
        await _gasMeterWriteRepository.SaveAsync();
        return new() { Success = result, Value = result };
    }

    public async Task<ServerResponse<bool>> CreateScale(AddNewScaleModel scale)
    {
        Scale? resulScale = await _scaleReadRepository.Table.AsNoTracking().FirstOrDefaultAsync(s => s.Brand == scale.Brand && s.TypeOrModel == scale.TypeOrModel && s.SerialNumber == scale.SerialNumber);
        if (resulScale is not null)
            return FailedResponse("This scale already registered.");

        AppUser? user = await GetAuthUser();
        if (user is null)
            return FailedResponse("Unauthroized request.");

        bool result = await _scaleWriteRepository.AddAsync(new()
        {
            Id = scale.Id,
            Brand = scale.Brand,
            TypeOrModel = scale.TypeOrModel,
            SerialNumber = scale.SerialNumber,
            IsActive = true,
            LastInspectionYear = Convert.ToInt32(scale.LastInspectionYear),
            MaximumCapacity = scale.MaximumCapacity,
            AccuracyClass = scale.AccuracyClass,
            UserOfInstrument = user
        });
        await _scaleWriteRepository.SaveAsync();
        if (!result)
            return FailedResponse("Unknown error.");
        return new() { Success = result, Value = result };
    }

    public async Task<ServerResponse<bool>> DeleteGasMeter(string id)
    {
        bool result = await _gasMeterWriteRepository.DeleteAsync(Guid.Parse(id));
        if (!result)
            return FailedResponse("Unknown error.");
        return new() { Success = result, Value = result };
    }

    public async Task<ServerResponse<bool>> DeleteScale(string id)
    {
        bool result = await _scaleWriteRepository.DeleteAsync(Guid.Parse(id));
        if (!result)
            return FailedResponse("Unknown error.");
        return new() { Success = result, Value = result };
    }

    public async Task<ServerResponse<bool>> UpdateGasMeter(GasMeterModel gasMeter)
    {
        GasMeter? resultGasMeter = await _gasMeterReadRepository.Table.Include(gm => gm.UserOfInstrument).FirstOrDefaultAsync(gm => gm.Id == gasMeter.Id);
        if (resultGasMeter is null)
            return FailedResponse("Gas meter not found.");

        AppUser? user = await GetAuthUser();
        if (user is null || resultGasMeter.UserOfInstrument?.Id.ToString() != user.Id)
            return FailedResponse("Unauthorized request.");

        GasMeter? duplicateGasMeter = await _gasMeterReadRepository.Table.FirstOrDefaultAsync(gm => gm.Id != gasMeter.Id && gm.Brand == gasMeter.Brand && gm.TypeOrModel == gasMeter.TypeOrModel && gm.SerialNumber == gasMeter.SerialNumber);
        if (duplicateGasMeter is not null)
            return FailedResponse("This gas meter already registered.");

        resultGasMeter.Brand = gasMeter.Brand;
        resultGasMeter.TypeOrModel = gasMeter.TypeOrModel;
        resultGasMeter.SerialNumber = gasMeter.SerialNumber;
        resultGasMeter.LastInspectionYear = resultGasMeter.LastInspectionYear;
        resultGasMeter.MaxFlowRate = resultGasMeter.MaxFlowRate;
        bool result = _gasMeterWriteRepository.Update(resultGasMeter);
        await _gasMeterWriteRepository.SaveAsync();
        if (!result)
            return FailedResponse("Unknown error.");
        return new() { Success = result, Value = result };
    }

    public async Task<ServerResponse<bool>> UpdateScale(ScaleModel scale)
    {
        Scale? resultScale = await _scaleReadRepository.Table.Include(s => s.UserOfInstrument).FirstOrDefaultAsync(s => s.Id == scale.Id);
        if (resultScale is null)
            return FailedResponse("Scale not found.");

        AppUser? user = await GetAuthUser();
        if (user is null || resultScale.UserOfInstrument?.Id.ToString() != user.Id)
            return FailedResponse("Unauthorized request.");

        Scale? duplicateScale = await _scaleReadRepository.Table.FirstOrDefaultAsync(s => s.Id != scale.Id && s.Brand == scale.Brand && s.TypeOrModel == scale.TypeOrModel && s.SerialNumber == scale.SerialNumber);
        if (duplicateScale is not null)
            return FailedResponse("This scale already registered.");

        resultScale.Brand = scale.Brand;
        resultScale.TypeOrModel = scale.TypeOrModel;
        resultScale.SerialNumber = scale.SerialNumber;
        resultScale.LastInspectionYear = scale.LastInspectionYear;
        resultScale.MaximumCapacity = scale.MaximumCapacity;
        resultScale.AccuracyClass = scale.AccuracyClass;
        bool result = _scaleWriteRepository.Update(resultScale);
        await _scaleWriteRepository.SaveAsync();
        if (!result)
            return FailedResponse("Unknown error.");
        return new() { Success = result, Value = result };
    }

    public async Task<ServerResponse<List<ScaleModel>>> GetMyScales()
    {
        AppUser? appUser = await GetAuthUser();
        if (appUser is null)
            return new() { ErrorMessage = "Unauthorized request.", Success = false };

        List<ScaleModel> scales = await _scaleReadRepository.Table.Where(s => s.UserOfInstrument == appUser).Select(scale => new ScaleModel()
        {
            Id = scale.Id,
            Brand = scale.Brand,
            TypeOrModel = scale.TypeOrModel,
            SerialNumber = scale.SerialNumber,
            AccuracyClass = scale.AccuracyClass,
            MaximumCapacity = scale.MaximumCapacity,
            LastInspectionYear = scale.LastInspectionYear,
            IsActive = scale.IsActive
        }).ToListAsync();

        return new() { Success = true, Value = scales };
    }

    public async Task<ServerResponse<List<GasMeterModel>>> GetMyGasMeters()
    {
        AppUser? appUser = await GetAuthUser();
        if (appUser is null)
            return new() { ErrorMessage = "Unauthorized request.", Success = false };

        List<GasMeterModel> gasMeters = await _gasMeterReadRepository.Table.AsNoTracking().Where(gm => gm.UserOfInstrument == appUser).Select(gasmeter => new GasMeterModel()
        {
            Id = gasmeter.Id,
            Brand = gasmeter.Brand,
            TypeOrModel = gasmeter.TypeOrModel,
            SerialNumber = gasmeter.SerialNumber,
            MaxFlowRate = gasmeter.MaxFlowRate,
            LastInspectionYear = gasmeter.LastInspectionYear,
            IsActive = gasmeter.IsActive
        }).ToListAsync();

        return new() { Success = true, Value = gasMeters };
    }

    public ServerResponse<bool> FailedResponse(string error = "Bad request.") =>
    new() { ErrorMessage = error, Success = false, Value = false };
}