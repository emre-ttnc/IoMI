using IoMI.Application.Services;
using IoMI.Shared.Models.ApplicationModels;
using IoMI.Shared.Models.ServerResponseModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IoMI.Server.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User, SystemAdmin")]
public class ApplicationController : ControllerBase
{
    private readonly IApplicationService _applicationService;

    public ApplicationController(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    [HttpGet("GetScaleInspectionApplications")]
    public async Task<ServerResponse<List<ScaleInspectionApplicationModel>>> GetScaleInspectionApplications() => await _applicationService.GetScaleInspectionApplications();

    [HttpGet("GetGasMeterInspectionApplications")]
    public async Task<ServerResponse<List<GasMeterInspectionApplicationModel>>> GetGasMeterInspectionApplications() => await _applicationService.GetGasMeterInspectionApplications();
}
