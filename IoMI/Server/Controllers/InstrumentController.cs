using IoMI.Application.Services;
using IoMI.Shared.Models.InstrumentModels;
using IoMI.Shared.Models.ServerResponseModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IoMI.Server.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User, SystemAdmin")]
public class InstrumentController : ControllerBase
{
    private readonly IInstrumentService _instrumentService;

    public InstrumentController(IInstrumentService instrumentService)
    {
        _instrumentService = instrumentService;
    }

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


    [HttpPost("UpdateScale")]
    public async Task<ServerResponse<bool>> UpdateScale(AddNewScaleModel request)
    {
        if(request is null || request.Id == Guid.Empty ||         
            string.IsNullOrEmpty(request.Brand?.Trim()) ||
            string.IsNullOrEmpty(request.TypeOrModel?.Trim()) ||
            string.IsNullOrEmpty(request.SerialNumber?.Trim()) ||
            string.IsNullOrEmpty(request.LastInspectionYear?.Trim()) ||
            string.IsNullOrEmpty(request.AccuracyClass?.Trim()) ||
            string.IsNullOrEmpty(request.MaximumCapacity?.Trim()))
            return new() { ErrorMessage = "Please fill all field", Success = false };

        return await _instrumentService.UpdateScale(new() {
            Id = request.Id,
            Brand = request.Brand,
            TypeOrModel = request.TypeOrModel,
            SerialNumber = request.SerialNumber,
            LastInspectionYear = Convert.ToInt32(request.LastInspectionYear),
            AccuracyClass = request.AccuracyClass,
            MaximumCapacity = request.MaximumCapacity
        });
    }


    [HttpPost("UpdateGasMeter")]
    public async Task<ServerResponse<bool>> UpdateGasMeter(AddNewGasMeterModel request)
    {
        if(request is null || request.Id == Guid.Empty ||         
            string.IsNullOrEmpty(request.Brand?.Trim()) ||
            string.IsNullOrEmpty(request.TypeOrModel?.Trim()) ||
            string.IsNullOrEmpty(request.SerialNumber?.Trim()) ||
            string.IsNullOrEmpty(request.LastInspectionYear?.Trim()) ||
            string.IsNullOrEmpty(request.MaxFlowRate?.Trim()))
            return new() { ErrorMessage = "Please fill all field", Success = false };

        return await _instrumentService.UpdateGasMeter(new() {
            Id = request.Id,
            Brand = request.Brand,
            TypeOrModel = request.TypeOrModel,
            SerialNumber = request.SerialNumber,
            LastInspectionYear = Convert.ToInt32(request.LastInspectionYear),
            MaxFlowRate = request.MaxFlowRate
        });
    }

    [HttpGet("GetMyGasMeters")]
    public async Task<ServerResponse<List<GasMeterModel>>> GetMyGasMeters() => await _instrumentService.GetMyGasMeters();

    [HttpGet("GetMyScales")]
    public async Task<ServerResponse<List<ScaleModel>>> GetMyScales() => await _instrumentService.GetMyScales();
}
