using IoMI.Shared.Models.ApplicationModels;
using IoMI.Shared.Models.ServerResponseModels;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace IoMI.Client.Pages.User;

public class MyApplicationsCode : ComponentBase
{
    [Inject] HttpClient Http { get; set; } = default!;
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

    protected static void SeeApplicationDetail(ScaleInspectionApplicationModel application)
    {
        //TODO open detail dialog
    }

    protected static void SeeApplicationDetail(GasMeterInspectionApplicationModel application)
    {
        //TODO open detail dialog
    }
}
