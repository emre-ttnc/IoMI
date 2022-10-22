using IoMI.Application.Services;
using IoMI.Shared.Models.InstrumentModels;
using IoMI.Shared.Models.ServerResponseModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IoMI.Server.Controllers;

[Route("[controller]")]
[ApiController]
public class InstrumentController : ControllerBase
{
    private readonly IInstrumentService _instrumentService;

    public InstrumentController(IInstrumentService instrumentService)
    {
        _instrumentService = instrumentService;
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User, SystemAdmin")]
    [HttpPost("AddNewScale")]
    public async Task<ServerResponse<bool>> AddNewScale(AddNewScaleModel request)
    {
        if (request is null ||
            request.Id == Guid.Empty ||
            string.IsNullOrEmpty(request.Brand?.Trim()) ||
            string.IsNullOrEmpty(request.TypeOrModel?.Trim()) ||
            string.IsNullOrEmpty(request.SerialNumber?.Trim()) ||
            string.IsNullOrEmpty(request.LastInspectionYear?.Trim()) ||
            string.IsNullOrEmpty(request.AccuracyClass?.Trim()) ||
            string.IsNullOrEmpty(request.MaximumCapacity?.Trim()))
            return new() { ErrorMessage = "Please fill all field", Success = false };

        return await _instrumentService.CreateScale(request);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User, SystemAdmin")]
    [HttpPost("AddNewGasMeter")]
    public async Task<ServerResponse<bool>> AddNewGasMeter(AddNewGasMeterModel request)
    {
        if (request is null ||
            request.Id == Guid.Empty ||
            string.IsNullOrEmpty(request.Brand?.Trim()) ||
            string.IsNullOrEmpty(request.TypeOrModel?.Trim()) ||
            string.IsNullOrEmpty(request.SerialNumber?.Trim()) ||
            string.IsNullOrEmpty(request.LastInspectionYear?.Trim()) ||
            string.IsNullOrEmpty(request.MaxFlowRate?.Trim()))
            return new() { ErrorMessage = "Please fill all field", Success = false };

        return await _instrumentService.CreateGasMeter(request);
    }

    
}
