using Microsoft.JSInterop;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace IoMI.Client.Utils
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly IJSRuntime _js;

        public CustomAuthStateProvider(IJSRuntime js)
        {
            _js = js;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            string token = await _js.InvokeAsync<string>("localStorage.getItem", "accessToken") ?? string.Empty;
            JwtSecurityTokenHandler handler = new();

            ClaimsIdentity claims = new();
            if (!string.IsNullOrEmpty(token.Trim()))
                try
                {
                    JwtSecurityToken securityToken = handler.ReadJwtToken(token);
                    if (securityToken.ValidTo > DateTime.UtcNow)
                        claims = new(securityToken.Claims, "jwt");
                    else
                        await _js.InvokeVoidAsync("localStorage.removeItem", "accessToken");
                }
                catch (Exception) { }

            ClaimsPrincipal user = new(claims);
            AuthenticationState state = new(user);

            NotifyAuthenticationStateChanged(Task.FromResult(state));

            return state;
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
