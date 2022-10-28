using IoMI.Shared.Models.ApplicationModels;
using IoMI.Shared.Models.InstrumentModels;
using IoMI.Shared.Models.ServerResponseModels;

namespace IoMI.Application.Services
{
    public interface IApplicationService
    {
        Task<ServerResponse<bool>> AcceptGasMeterInspectionApplication();
        Task<ServerResponse<bool>> AcceptScaleInspectionApplication();
        Task<ServerResponse<bool>> AddNewGasMeterInspectionApplication(List<GasMeterModel> gasMeters);
        Task<ServerResponse<bool>> AddNewScaleInspectionApplication(List<ScaleModel> scales);
        Task<ServerResponse<bool>> CompleteGasMeterInspectionApplication();
        Task<ServerResponse<bool>> CompleteScaleInspectionApplication();
        Task<ServerResponse<List<ScaleModel>>> GetScaleInspectionApplicationDetails(Guid id);
        Task<ServerResponse<List<GasMeterModel>>> GetGasMeterInspectionApplicationDetails(Guid id);
        Task<ServerResponse<List<GasMeterInspectionApplicationModel>>> GetGasMeterInspectionApplications();
        Task<ServerResponse<List<ScaleInspectionApplicationModel>>> GetScaleInspectionApplications();
        ServerResponse<T> FailedResponse<T>(string error = "Bad request.");
    }
}