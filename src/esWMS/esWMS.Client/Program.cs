using esWMS.Client.Pages.OnePage;
using esWMS.Client.Services;
using esWMS.Components.Alert;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

Console.WriteLine($"Blazor build start - baseAddress: {builder.HostEnvironment.BaseAddress}");

builder.Services.AddTransient(http => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

builder.Services.AddSingleton<OnePageState>();
builder.Services.AddSingleton<IAlertService, AlertService>();
builder.Services.AddTransient(typeof(IDataService<>), typeof(DataService<>));
builder.Services.AddTransient<IDocumentDataService, DocumentDataService>();
builder.Services.AddTransient<IConfirmDialogService, ConfirmDialogService>();
builder.Services.AddMudServices();

builder.Services.AddMudBlazorSnackbar(config =>
{
    config.PositionClass = Defaults.Classes.Position.BottomEnd;
});

Console.WriteLine($"BaseAddress: {builder.HostEnvironment.BaseAddress}");
await builder.Build().RunAsync();
