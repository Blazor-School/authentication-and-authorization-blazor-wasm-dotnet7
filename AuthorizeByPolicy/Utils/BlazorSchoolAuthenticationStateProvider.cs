using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace AuthorizeByPolicy.Utils;

public class BlazorSchoolAuthenticationStateProvider : AuthenticationStateProvider
{
    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var claims = new List<Claim>
        {
            new (ClaimTypes.NameIdentifier,"Blazor School"),
            new (ClaimTypes.Role,"User"),
            new (ClaimTypes.Role,"PremiumUser"),
            new ("Age", 17.ToString())
            //new ("Age", 18.ToString())
        };
        var identity = new ClaimsIdentity(claims, "Blazor School");
        var claimsPrincipal = new ClaimsPrincipal(identity);
        var authenticationState = new AuthenticationState(claimsPrincipal);
        var authenticationTask = Task.FromResult(authenticationState);

        return authenticationTask;
    }
}