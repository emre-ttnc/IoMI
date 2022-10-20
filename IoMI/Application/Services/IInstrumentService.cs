using IoMI.Shared.Models.InstrumentModels;
using IoMI.Shared.Models.ServerResponseModels;

namespace IoMI.Application.Services;

public interface IInstrumentService
{
    Task<ServerResponse<bool>> CreateScale(ScaleModel scale);
    Task<ServerResponse<bool>> CreateGasMeter(GasMeterModel gasMeter);
    Task<ServerResponse<bool>> UpdateScale(ScaleModel scale);
    Task<ServerResponse<bool>> UpdateGasMeter(GasMeterModel gasMeter);
    Task<ServerResponse<bool>> DeleteScale(string id);
    Task<ServerResponse<bool>> DeleteGasMeter(string id);
    ServerResponse<bool> FailedResponse(string error = "Bad request.");
}