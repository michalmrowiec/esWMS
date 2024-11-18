using esWMS.Client.Services.Api;
using esWMS.Client.Services.Auth;
using esWMS.Client.Services.Dialog;
using esWMS.Client.Services.LocalStorage;
using esWMS.Client.Services.Notification;
using esWMS.Client.States;
using esWMS.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor.Services;
using NLog;
using NLog.Web;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Logging.ClearProviders();
    builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
    builder.Host.UseNLog();

    builder.Services.AddScoped<IAuthService, AuthService>();
    builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
    builder.Services.AddAuthorization();
    builder.Services.AddCascadingAuthenticationState();
    builder.Services.AddSingleton<IAlertService, AlertService>();
    builder.Services.AddSingleton<OnePageState>();
    builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();
    builder.Services.AddTransient(typeof(IDataService<>), typeof(DataService<>));
    builder.Services.AddTransient<IDocumentDataService, DocumentDataService>();
    builder.Services.AddTransient<IConfirmDialogService, ConfirmDialogService>();
    builder.Services.AddHttpClient();

    builder.Services.AddMudServices();

    builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseWebAssemblyDebugging();
    }
    else
    {
        app.UseExceptionHandler("/Error", createScopeForErrors: true);
        app.UseHsts();
    }

    app.UseHttpsRedirection();

    app.UseStaticFiles();
    app.UseAntiforgery();

    app.MapRazorComponents<App>()
        .AddInteractiveServerRenderMode()
        .AddInteractiveWebAssemblyRenderMode()
        .AddAdditionalAssemblies(typeof(esWMS.Client._Imports).Assembly);

    app.Run();
}
catch (Exception exception)
{
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    NLog.LogManager.Shutdown();
}

public partial class Program { }