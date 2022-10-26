using IoMI.Client.Utils;
using IoMI.Shared.Models.ApplicationModels;
using IoMI.Shared.Models.InstrumentModels;
using IoMI.Shared.Models.ServerResponseModels;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace IoMI.Client.Pages.User;

public class MyApplicationsCode : ComponentBase
{
    [Inject] HttpClient Http { get; set; } = default!;
    [Inject] ModalManager Modal { get; set; } = default!;
    protected List<ScaleInspectionApplicationModel>? ScaleApplications { get; set; }
    protected List<GasMeterInspectionApplicationModel>? GasMeterApplications { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await GetScaleApplications();
        await GetGasMeterApplications();
    }

    private async Task GetScaleApplications()
    {
        ServerResponse<List<ScaleInspectionApplicationModel>>? response = await Http.GetFromJsonAsync<ServerResponse<List<ScaleInspectionApplicationModel>>>("Application/GetScaleInspectionApplications");
        if (response is not null && response.Success)
            ScaleApplications = response.Value;
    }

    private async Task GetGasMeterApplications()
    {
        ServerResponse<List<GasMeterInspectionApplicationModel>>? response = await Http.GetFromJsonAsync<ServerResponse<List<GasMeterInspectionApplicationModel>>>("Application/GetGasMeterInspectionApplications");
        if (response is not null && response.Success)
            GasMeterApplications = response.Value;
    }

    protected async Task ApplicationDetail(ScaleInspectionApplicationModel application)
    {
        ServerResponse<List<ScaleModel>>? response = await Http.GetFromJsonAsync<ServerResponse<List<ScaleModel>>>("Instrument/GetMyScales");
        if (response is not null && response.Success)
        {
            if (response.Value is not null && response.Value.Count > 0)
                await Modal.ShowScaleApplicationModalAsync(application, response.Value);
            else
                await Modal.ShowMessageModalAsync("Invalid request.", "First, you need to add at least one scale.");
        }
        else
            await Modal.ShowMessageModalAsync("Error.", response?.ErrorMessage ?? "Unknown error occured.");
    }

    protected static void SeeApplicationDetail(GasMeterInspectionApplicationModel application)
    {
        //TODO open detail dialog
    }
}
