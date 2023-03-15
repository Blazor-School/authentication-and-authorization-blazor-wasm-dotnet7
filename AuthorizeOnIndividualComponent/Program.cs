using AuthorizeOnIndividualComponent;
using AuthorizeOnIndividualComponent.Requirements;
using AuthorizeOnIndividualComponent.Utilities;
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
builder.Services.AddScoped<IAuthorizationHandler, EsrbRequirementHandler>();
builder.Services.AddAuthorizationCore(config => config.AddPolicy("EsrbPolicy", policy => policy.AddRequirements(new EsrbRequirement())));
builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();
