using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Service.Monitor.Interfaces;
using Service.Monitor.Middleware;
using Service.Monitor.Repositories;

var builder = WebApplication.CreateBuilder(args);

bool running = true;

// Add services to the container.
builder.Services.AddHealthChecks()
    .AddCheck("self", () => running ? HealthCheckResult.Healthy() : HealthCheckResult.Unhealthy());
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IEventRepository, EventsRepository>();


// ================================================================================================================== //


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

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