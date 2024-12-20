using esWMS.Client.Services.Api;
using esWMS.Client.Services.Auth;
using esWMS.Client.Services.Dialog;
using esWMS.Client.Services.LocalStorage;
using esWMS.Client.Services.Notification;
using esWMS.Client.States;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

Console.WriteLine($"ApiAddress: {builder.Configuration["ApiUrl"]}");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration["ApiUrl"]) });

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddAuthorizationCore();
builder.Services.AddSingleton<OnePageState>();
builder.Services.AddSingleton<IAlertService, AlertService>();
builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();
builder.Services.AddTransient(typeof(IDataService<>), typeof(DataService<>));
builder.Services.AddTransient<IDataService, DataService>();
builder.Services.AddTransient<IDocumentDataService, DocumentDataService>();
builder.Services.AddTransient<IConfirmDialogService, ConfirmDialogService>();
builder.Services.AddMudServices();
builder.Services.AddMudBlazorSnackbar(config =>
{
    config.PositionClass = Defaults.Classes.Position.BottomEnd;
});

await builder.Build().RunAsync();