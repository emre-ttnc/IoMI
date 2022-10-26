using IoMI.Shared.Models.ApplicationModels;
using IoMI.Shared.Models.ServerResponseModels;

namespace IoMI.Application.Services
{
    public interface IApplicationService
    {
        Task<ServerResponse<bool>> AcceptGasMeterInspectionApplication();
        Task<ServerResponse<bool>> AcceptScaleInspectionApplication();
        Task<ServerResponse<bool>> AddNewGasMeterInspectionApplication();
        Task<ServerResponse<bool>> AddNewScaleInspectionApplication();
        Task<ServerResponse<bool>> CompleteGasMeterInspectionApplication();
        Task<ServerResponse<bool>> CompleteScaleInspectionApplication();
        Task<ServerResponse<List<GasMeterInspectionApplicationModel>>> GetGasMeterInspectionApplications();
        Task<ServerResponse<List<ScaleInspectionApplicationModel>>> GetScaleInspectionApplications();
    }
}