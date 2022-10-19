using IoMI.Client.Utils;
using IoMI.Shared.Models.ServerResponseModels;
using IoMI.Shared.Models.UserModels;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace IoMI.Client.Pages.Admin;

public class UsersCode : ComponentBase
{
    [Inject] HttpClient Http { get; set; } = default!;
    protected UserList[]? UserList { get; set; }
    protected bool IsBusy { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await GetUserList();
    }

    protected async Task ChangeStatus(string id)
    {
        IsBusy = true;
        ServerResponse<bool> response = await Http.PostAndGetServerResponseAsync<bool, string>("User/ChangeStatus", id);
        if (response is not null && response.Success)
            await GetUserList();
        IsBusy = false;
    }

    private async Task GetUserList()
    {
        UserList = await Http.GetFromJsonAsync<UserList[]>("User/GetUsers");
    }

}
