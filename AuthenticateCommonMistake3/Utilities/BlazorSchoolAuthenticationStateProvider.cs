using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace AuthenticateCommonMistake3.Utilities;

public class BlazorSchoolAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly BlazorSchoolUserService _blazorSchoolUserService;
    public int GetAuthenticationStateAsyncRan { get; set; } = 0;

    public BlazorSchoolAuthenticationStateProvider(BlazorSchoolUserService blazorSchoolUserService)
    {
        _blazorSchoolUserService = blazorSchoolUserService;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        GetAuthenticationStateAsyncRan++;
        var principal = new ClaimsPrincipal();
        var user = _blazorSchoolUserService.FetchUserFromBrowser();

        if (user is not null)
        {
            var authenticatedUser = await _blazorSchoolUserService.SendAuthenticateRequestAsync(user.Username, user.Password);

            if (authenticatedUser is not null)
            {
                principal = authenticatedUser.ToClaimsPrincipal();
            }
        }

        return new(principal);
    }

    public async Task LoginAsync(string username, string password)
    {
        var principal = new ClaimsPrincipal();
        var user = await _blazorSchoolUserService.SendAuthenticateRequestAsync(username, password);

        if (user is not null)
        {
            principal = user.ToClaimsPrincipal();
        }

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
    }

    public void Logout()
    {
        _blazorSchoolUserService.ClearBrowserUserData();
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new())));
    }
}
