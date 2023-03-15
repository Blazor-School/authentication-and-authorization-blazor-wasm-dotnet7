using AuthorizeOnRoute;
using AuthorizeOnRoute.Requirements;
using AuthorizeOnRoute.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<AuthenticationDataMemoryStorage>();
builder.Services.AddScoped<BlazorSchoolUserService>();
builder.Services.AddScoped<BlazorSchoolAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<BlazorSchoolAuthenticationStateProvider>());
builder.Services.AddScoped<IAuthorizationHandler, AdultRequirementHandler>();
builder.Services.AddAuthorizationCore(config =>
{
    config.AddPolicy("AdultOnly", policy => policy.AddRequirements(new AdultRequirement()));

    config.AddPolicy("AdultAdminOnly", policy =>
    {
        policy.AddRequirements(new AdultRequirement());
        policy.RequireRole("admin");
    });
});

await builder.Build().RunAsync();
