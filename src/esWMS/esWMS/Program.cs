using esWMS.Application;
using esWMS.Client.Services;
using esWMS.Client.States;
using esWMS.Components;
using esWMS.Components.Alert;
using esWMS.Infrastructure;
using esWMS.Middleware;
using esWMS.Services;
using esWMS.Services.DataSeed;
using MediatR;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.OpenApi.Models;
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

    builder.Services.AddApplication();
    builder.Services.AddInfrastructure(builder.Configuration);

    builder.Services.AddScoped<ErrorHandlingMiddleware>();

    builder.Services.AddScoped<IUserContextService, UserContextService>();
    builder.Services.AddHttpContextAccessor();

    builder.Services.AddControllers();

    builder.Services.AddSwaggerGen(cfg =>
    {
        cfg.AddSecurityDefinition(
            "Bearer",
            new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please insert token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "bearer"
            });

        cfg.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header,
                },
                new List<string>()
            }
        });
    });

    builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

    var app = builder.Build();

    using var scope = app.Services.CreateScope();

    var dbContext = scope.ServiceProvider
        .GetRequiredService<EsWmsDbContext>();

    var mediator = scope.ServiceProvider
        .GetRequiredService<IMediator>();

    dbContext.UpdateDatabase(scope.ServiceProvider.GetRequiredService<ILogger<Program>>());

    await dbContext.SeedStartAdmin(mediator);

    if (app.Environment.IsDevelopment())
    {
        await dbContext.SeedStartData();

        app.UseWebAssemblyDebugging();

        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "esWMS"));
    }
    else
    {
        app.UseExceptionHandler("/Error", createScopeForErrors: true);
        app.UseHsts();
    }

    app.UseAuthentication();

    app.UseHttpsRedirection();

    app.UseStaticFiles();
    app.UseAntiforgery();

    app.UseAuthorization();

    app.UseMiddleware<ErrorHandlingMiddleware>();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller}/{action}/{id?}");

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