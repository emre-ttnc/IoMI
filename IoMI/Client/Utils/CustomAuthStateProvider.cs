using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace IoMI.Client.Utils
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly IJSRuntime _js;
        private readonly HttpClient _httpClient;

        public CustomAuthStateProvider(IJSRuntime js, HttpClient httpClient)
        {
            _js = js;
            _httpClient = httpClient;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            string token = await GetTokenAsync();
            if (!string.IsNullOrEmpty(token.Trim()))
            {
                AuthenticationState state;
                ClaimsIdentity claims = new();
                JwtSecurityTokenHandler handler = new();
                JwtSecurityToken securityToken = new();
                try
                {
                    securityToken = handler.ReadJwtToken(token);
                }
                catch (Exception)
                {
                    await SetTokenAsync(string.Empty);
                }
                if (securityToken.ValidTo > DateTime.UtcNow)
                {
                    claims = new(securityToken.Claims, "jwt");
                    if (!claims.IsAuthenticated)
                        await SetTokenAsync(string.Empty);
                    _httpClient.DefaultRequestHeaders.Authorization = new("Bearer", token);
                }
                else
                {
                    await SetTokenAsync(string.Empty);
                }
                ClaimsPrincipal user = new(claims);
                state = new(user);
                NotifyAuthenticationStateChanged(Task.FromResult(state));
                return state;
            }
            return new(new());
        }

        public async Task SetTokenAsync(string token)
        {
            if(string.IsNullOrEmpty(token.Trim()))
                await _js.InvokeVoidAsync("localStorage.removeItem", "accessToken");
            else
                await _js.InvokeVoidAsync("localStorage.setItem", "accessToken", token);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task<string> GetTokenAsync() => await _js.InvokeAsync<string>("localStorage.getItem", "accessToken") ?? string.Empty;



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
