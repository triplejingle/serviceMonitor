using Framework.Application.Authorization.CORS;
using Framework.Persistence;
using Framework.Persistence.Interfaces;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using Serilog;
using Service.Monitor.Middleware;
using Service.Monitor.Repositories;
using Service.Monitor.Repositories.Interfaces;
using Service.Monitor.Services;
using Service.Monitor.Services.Interfaces;
using Swashbuckle.AspNetCore.SwaggerUI;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

bool _running = true;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSerilog((services, lc) => lc
    .ReadFrom.Configuration(builder.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext());

// Add services to the container.
builder.Services.AddHealthChecks()
    .AddCheck("self", () => _running ? HealthCheckResult.Healthy() : HealthCheckResult.Unhealthy());
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Appset Monitor API",
        Version = "v1",
        Description =
            "The API for the monitor components for the apps.",
        Contact = new OpenApiContact
        {
            Name = "Appset",
            Email = "info@appset.nl",
            Url = new Uri("https://appset.nl")
        }
    });

    c.CustomSchemaIds(x => x.FullName);
    c.IgnoreObsoleteProperties();
});

#region Cors

builder.Services.AddTransient<ICorsPolicyProvider, AppCorsPolicyProvider>();
builder.Services.AddScoped<IFrameworkSettingsQueryCollection, FrameworkSettingsQueryCollection>();

builder.Services.AddTransient<IFrameworkCoreDbConnection, FrameworkCoreDbConnection>();

#endregion

#region services
builder.Services.AddTransient<IViewService, ViewService>();
builder.Services.AddTransient<IEventService, EventService>();
#endregion

#region repositories
builder.Services.AddTransient<IEventRepository, EventsRepository>();
builder.Services.AddTransient<IViewsRepository, ViewsRepository>();
#endregion


// ================================================================================================================== //

var app = builder.Build();

app.UseCors("MyPolicy");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Core API V1");
        c.RoutePrefix = string.Empty;

        c.DisplayOperationId();
        c.DefaultModelRendering(ModelRendering.Model);
        c.DocExpansion(DocExpansion.None);
        c.EnableFilter();
        c.EnableValidator();
    });
}

app.UseAuthorization();

app.UseExceptionHandler(exceptionHandlerApp =>
{
    exceptionHandlerApp.Run(ExceptionHandlerMiddleware.HandleExceptionsAsync);
});

app.UseHealthChecks("/self", new HealthCheckOptions
{
    Predicate = r => r.Name.Contains("self")
});
app.UseHealthChecks("/ready", new HealthCheckOptions
{
    Predicate = r => r.Tags.Contains("services")
});
app.MapControllers();

app.Run();