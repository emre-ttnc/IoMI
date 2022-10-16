using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace IoMI.Client.Utils
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly IJSRuntime _js;
        private readonly NavigationManager _navigationManager;

        public CustomAuthStateProvider(IJSRuntime js, NavigationManager navigationManager)
        {
            _js = js;
            _navigationManager = navigationManager;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            string token = await _js.InvokeAsync<string>("localStorage.getItem", "accessToken") ?? string.Empty;
            if (!string.IsNullOrEmpty(token.Trim()))
            {
                AuthenticationState state;
                ClaimsIdentity claims = new();
                JwtSecurityTokenHandler handler = new();
                try
                {
                    JwtSecurityToken securityToken = handler.ReadJwtToken(token);
                    if (securityToken.ValidTo > DateTime.UtcNow)
                        claims = new(securityToken.Claims, "jwt");
                    else
                    {
                        await _js.InvokeVoidAsync("localStorage.removeItem", "accessToken");
                        _navigationManager.NavigateTo("/user/login");
                    }
                }
                catch (Exception)
                {
                    await _js.InvokeVoidAsync("localStorage.removeItem", "accessToken");
                    _navigationManager.NavigateTo("/user/login");
                }
                ClaimsPrincipal user = new(claims);
                state = new(user);
                NotifyAuthenticationStateChanged(Task.FromResult(state));
                return state;
            }
            else
            {
                _navigationManager.NavigateTo("/user/login");
            }
            return new(new());
        }

        //public static IEnumerable<Claim> GetClaimsFromJwt(string jwtToken)
        //{
        //    string payload = jwtToken.Split(".")[1];
        //    byte[] JsonBytes = Parse64WithoutPadding(payload);
        //    Dictionary<string, object>? keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(JsonBytes);
        //    return keyValuePairs is null ? new List<Claim>() : keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString() ?? ""));
        //}

        //private static byte[] Parse64WithoutPadding(string base64)
        //{
        //    switch (base64.Length % 4)
        //    {
        //        case 2: base64 += "=="; break;
        //        case 3: base64 += "="; break;
        //    }
        //    return Convert.FromBase64String(base64);
        //}
    }
}
