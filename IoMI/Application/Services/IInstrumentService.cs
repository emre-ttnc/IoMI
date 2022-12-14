using IoMI.Shared.Models.InstrumentModels;
using IoMI.Shared.Models.ServerResponseModels;

namespace IoMI.Application.Services;

public interface IInstrumentService
{
    Task<ServerResponse<bool>> CreateScale(AddNewScaleModel scale);
    Task<ServerResponse<bool>> CreateGasMeter(AddNewGasMeterModel gasMeter);
    Task<ServerResponse<bool>> UpdateScale(ScaleModel scale);
    Task<ServerResponse<bool>> UpdateGasMeter(GasMeterModel gasMeter);
    Task<ServerResponse<bool>> DeleteScale(string id);
    Task<ServerResponse<bool>> DeleteGasMeter(string id);
    Task<ServerResponse<List<ScaleModel>>> GetMyScales();
    Task<ServerResponse<List<GasMeterModel>>> GetMyGasMeters();
    ServerResponse<bool> FailedResponse(string error = "Bad request.");
}