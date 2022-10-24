using IoMI.Client.Utils;
using IoMI.Shared.Models.InstrumentModels;
using IoMI.Shared.Models.ServerResponseModels;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace IoMI.Client.Pages.User;

public class MyInstrumentsCode : ComponentBase
{
    [Inject] HttpClient Http { get; set; } = default!;
    [Inject] ModalManager Modal { get; set; } = default!;

    public List<ScaleModel>? Scales = new();
    public List<GasMeterModel>? GasMeters = new();

    protected override async Task OnInitializedAsync()
    {
        await GetScales();
        await GetGasMeters();
    }

    protected async Task OpenNewScaleModal()
    {
        bool result = await Modal.ShowNewScaleModalAsync();
        if (result)
            await GetScales();
    }

    protected async Task OpenNewGasMeterModal()
    {
        bool result = await Modal.ShowNewGasMeterModalAsync();
        if (result)
            await GetGasMeters();
    }

    private async Task GetScales()
    {
        ServerResponse<List<ScaleModel>>? response = await Http.GetFromJsonAsync<ServerResponse<List<ScaleModel>>>("Instrument/GetMyScales");
        if (response is not null && response.Success)
            Scales = response.Value;
        else
            await Modal.ShowMessageModalAsync("Error.", response?.ErrorMessage ?? "Unknown error occured.");
    }

    private async Task GetGasMeters()
    {
        ServerResponse<List<GasMeterModel>>? response = await Http.GetFromJsonAsync<ServerResponse<List<GasMeterModel>>>("Instrument/GetMyGasMeters");
        if (response is not null && response.Success)
            GasMeters = response.Value;
        else
            await Modal.ShowMessageModalAsync("Error.", response?.ErrorMessage ?? "Unknown error occured.");
    }

    protected async Task EditScale(ScaleModel scale)
    {
        bool result = await Modal.ShowUpdateScaleModalAsync(new() { Id = scale.Id, Brand = scale.Brand, TypeOrModel = scale.TypeOrModel, SerialNumber = scale.SerialNumber, LastInspectionYear = scale.LastInspectionYear.ToString(), AccuracyClass = scale.AccuracyClass, MaximumCapacity = scale.MaximumCapacity });
        if (result)
            await GetScales();
    }

    protected async Task EditGasMeter(GasMeterModel gasMeter)
    {
        bool result = await Modal.ShowUpdateGasMeterModalAsync(new() { Id = gasMeter.Id, Brand = gasMeter.Brand, TypeOrModel = gasMeter.TypeOrModel, SerialNumber = gasMeter.SerialNumber, LastInspectionYear = gasMeter.LastInspectionYear.ToString(), MaxFlowRate = gasMeter.MaxFlowRate });
        if (result)
            await GetGasMeters();
    }
}
