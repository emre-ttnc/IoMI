using Blazored.Modal;
using Blazored.Modal.Services;
using IoMI.Client.Components.ModalComponents;
using IoMI.Shared.Models.ApplicationModels;
using IoMI.Shared.Models.InstrumentModels;

namespace IoMI.Client.Utils;

public class ModalManager
{
    private readonly IModalService _modalService;

    public ModalManager(IModalService modalService)
    {
        _modalService = modalService;
    }

    private ModalOptions options = new() { UseCustomLayout = true, AnimationType = ModalAnimationType.None };

    public async Task ShowMessageModalAsync(string title, string message)
    {
        ModalParameters mParams = new() { { "Message", message }, { "Title", title } };
        await _modalService.Show<MessageModalComponent>(title, mParams, options).Result;
    }

    public async Task<bool> ShowConfirmModalAsync(string title, string message)
    {
        ModalParameters mParams = new() { { "Message", message } };
        var result = await _modalService.Show<ConfirmModalComponent>(title, mParams, options).Result;
        return result.Confirmed;
    }

    public async Task<bool> ShowNewScaleModalAsync()
    {
        ModalResult result = await _modalService.Show<AddNewScaleModalComponent>(title: "", options: options).Result;
        return result.Confirmed;
    }

    public async Task<bool> ShowNewGasMeterModalAsync()
    {
        ModalResult result = await _modalService.Show<AddNewGasMeterModalComponent>(title: "", options: options).Result;
        return result.Confirmed;
    }

    public async Task<bool> ShowUpdateScaleModalAsync(AddNewScaleModel scale)
    {
        ModalParameters mParams = new() { { "Scale", scale } };
        ModalResult result = await _modalService.Show<UpdateScaleModalComponent>(title: "", parameters: mParams, options: options).Result;
        return result.Confirmed;
    }

    public async Task<bool> ShowUpdateGasMeterModalAsync(AddNewGasMeterModel gasMeter)
    {
        ModalParameters mParams = new() { { "GasMeter", gasMeter } };
        ModalResult result = await _modalService.Show<UpdateGasMeterModalComponent>(title: "", parameters: mParams, options: options).Result;
        return result.Confirmed;
    }

    public async Task<bool> ShowNewScaleApplicationModalAsync(List<ScaleModel> scales)
    {
        ModalParameters mParams = new() { { "Scales", scales } };
        ModalResult result = await _modalService.Show<NewScaleApplicationModalComponent>(title: "", parameters: mParams, options: options).Result;
        return result.Confirmed;
    }

    public async Task<bool> ShowNewGasMeterApplicationModalAsync(List<GasMeterModel> gasMeters)
    {
        ModalParameters mParams = new() { { "GasMeters", gasMeters } };
        ModalResult result = await _modalService.Show<NewGasMeterApplicationModalComponent>(title: "", parameters: mParams, options: options).Result;
        return result.Confirmed;
    }

    public async Task<bool> ShowApplicationDetailAsync(ScaleInspectionApplicationModel application, List<ScaleModel> Scales)
    {
        ModalParameters mParams = new() { { "Application", application }, { "Scales", Scales } };
        ModalResult result = await _modalService.Show<ScaleApplicationDetailModalComponent>(title: "", parameters: mParams, options: options).Result;
        return result.Confirmed;
    }

    public async Task<bool> ShowApplicationDetailAsync(GasMeterInspectionApplicationModel application, List<GasMeterModel> GasMeters)
    {
        ModalParameters mParams = new() { { "Application", application }, { "GasMeters", GasMeters } };
        ModalResult result = await _modalService.Show<GasMeterApplicationDetailModalComponent>(title: "", parameters: mParams, options: options).Result;
        return result.Confirmed;
    }

}
