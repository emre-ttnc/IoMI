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
    protected bool IsLoading { get; set; } = true;

    protected override async Task OnInitializedAsync()
    {
        await GetScaleApplications();
        await GetGasMeterApplications();
        IsLoading = false;
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

    protected async Task CreateNewScaleInspectionApplication()
    {
        IsLoading = true;
        ServerResponse<List<ScaleModel>>? response = await Http.GetFromJsonAsync<ServerResponse<List<ScaleModel>>>("Instrument/GetMyScales");
        if (response is not null && response.Success)
        {
            if (response.Value is not null && response.Value.Any())
            {
                bool result = await Modal.ShowNewScaleApplicationModalAsync(response.Value);
                if (result)
                    await GetScaleApplications();
                IsLoading = false;
            }
            else
                await Modal.ShowMessageModalAsync("Invalid request.", "First, you need to add at least one scale.");
        }
        else
            await Modal.ShowMessageModalAsync("Error.", response?.ErrorMessage ?? "Unknown error occured.");
    }


    protected async Task CreateNewGasMeterInspectionApplication()
    {
        IsLoading = true;
        ServerResponse<List<GasMeterModel>>? response = await Http.GetFromJsonAsync<ServerResponse<List<GasMeterModel>>>("Instrument/GetMyGasMeters");
        if (response is not null && response.Success)
        {
            if (response.Value is not null && response.Value.Any())
            {
                bool result = await Modal.ShowNewGasMeterApplicationModalAsync(response.Value);
                if (result)
                    await GetGasMeterApplications();
                IsLoading = false;
            }
            else
                await Modal.ShowMessageModalAsync("Invalid request.", "First, you need to add at least one gas meter.");
        }
        else
            await Modal.ShowMessageModalAsync("Error.", response?.ErrorMessage ?? "Unknown error occured.");
    }

    protected async Task SeeApplicationDetail(GasMeterInspectionApplicationModel application)
    {
        IsLoading = true;
        ServerResponse<List<GasMeterModel>>? response = await Http.PostAndGetServerResponseAsync<List<GasMeterModel>, string>("Application/GetGasMeterInspectionApplicationDetails", application.Id.ToString());
        if (response is not null && response.Success)
        {
            if (response.Value is not null && response.Value.Any())
                await Modal.ShowApplicationDetailAsync(application, response.Value);
        }
        else
            await Modal.ShowMessageModalAsync("Error", response?.ErrorMessage ?? "Unknown error occured.");
        IsLoading = false;
    }

    protected async Task SeeApplicationDetail(ScaleInspectionApplicationModel application)
    {
        IsLoading = true;
        ServerResponse<List<ScaleModel>>? response = await Http.PostAndGetServerResponseAsync<List<ScaleModel>, string>("Application/GetScaleInspectionApplicationDetails", application.Id.ToString());
        if (response is not null && response.Success)
        {
            if (response.Value is not null && response.Value.Any())
                await Modal.ShowApplicationDetailAsync(application, response.Value);
        }
        else
            await Modal.ShowMessageModalAsync("Error", response?.ErrorMessage ?? "Unknown error occured.");
        IsLoading = false;
    }
}
