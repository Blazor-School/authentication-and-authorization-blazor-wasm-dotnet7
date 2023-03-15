using AuthorizeByPolicy;
using AuthorizeByPolicy.Policies;
using AuthorizeByPolicy.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IAuthorizationHandler, AdultRequirementHandler>();
builder.Services.AddScoped<IAuthorizationHandler, UnderAgeRestrictedRequirementHandler>();
builder.Services.AddScoped<BlazorSchoolAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<BlazorSchoolAuthenticationStateProvider>());
builder.Services.AddAuthorizationCore(config =>
{
    config.AddPolicy("AdultPolicy", policy => policy.AddRequirements(new AdultRequirement()));
    config.AddPolicy("UnderAgePolicy", policy => policy.AddRequirements(new UnderAgeRestrictedRequirement()));
    config.AddPolicy("AdultAdminPolicy", policy =>
    {
        policy.AddRequirements(new AdultRequirement());
        policy.RequireRole("Admin");
    });
});

await builder.Build().RunAsync();
