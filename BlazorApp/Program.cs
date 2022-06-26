using BlazorApp;
using BlazorApp.Services;
using BlazorApp.WellKnownNames;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Polly;
using Polly.Extensions.Http;
using Toolbelt.Blazor.Extensions.DependencyInjection;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var policy = HttpPolicyExtensions
    .HandleTransientHttpError()
    .WaitAndRetryAsync(3, retryAttempt =>
    {
        var nextTimeInterval = TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));
        Console.WriteLine($"Polly: error during call, retry for {retryAttempt} time in {TimeSpan.FromSeconds(nextTimeInterval.TotalSeconds)} second(s).");
        return nextTimeInterval;
    });

var apiBaseUrl = builder.Configuration[AppSettings.ApiBaseUrl] ?? "";
builder.Services.AddHttpClient(AppSettings.HttpClientApi, (services, client) =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
    client.EnableIntercept(services);
})
    .AddPolicyHandler(policy);


builder.Services.AddTransient(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient(AppSettings.HttpClientApi));

builder.Services.AddHttpClientInterceptor();
builder.Services.AddScoped<HttpClientInterceptorService>();

builder.Services.AddLocalization();
builder.Services.AddMudServices();

await builder.Build().RunAsync();