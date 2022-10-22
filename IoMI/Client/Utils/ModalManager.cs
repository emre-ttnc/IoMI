using Blazored.Modal;
using Blazored.Modal.Services;
using IoMI.Client.Components.ModalComponents;

namespace IoMI.Client.Utils;

public class ModalManager
{
    private readonly IModalService _modalService;

    public ModalManager(IModalService modalService)
    {
        _modalService = modalService;
    }

    public async Task ShowMessageModalAsync(string title, string message)
    {
        ModalOptions options = new() { UseCustomLayout = true, AnimationType = ModalAnimationType.None };
        ModalParameters parameters = new() { { "Message", message }, { "Title", title } };
        await _modalService.Show<MessageModalComponent>(title, parameters, options).Result;
    }

    public async Task<bool> ShowConfirmModalAsync(string title, string message)
    {
        ModalOptions options = new() { UseCustomLayout = true, AnimationType = ModalAnimationType.None };
        ModalParameters mParams = new() { { "Message", message } };
        var result = await _modalService.Show<ConfirmModalComponent>(title, mParams, options).Result;
        return result.Confirmed;
    }

    public async Task<bool> ShowNewScaleModalAsync()
    {
        ModalOptions options = new() { UseCustomLayout = true, AnimationType = ModalAnimationType.None };
        ModalResult result = await _modalService.Show<AddNewScaleModalComponent>(title: "", options: options).Result;
        return result.Confirmed;
    }

    public async Task<bool> ShowNewGasMeterModalAsync()
    {
        ModalOptions options = new() { UseCustomLayout = true, AnimationType = ModalAnimationType.None };
        ModalResult result = await _modalService.Show<AddNewGasMeterModalComponent>(title: "", options: options).Result;
        return result.Confirmed;
    }
}
