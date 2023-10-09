using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using service_monitor.Interfaces;
using service_monitor.repository;

var builder = WebApplication.CreateBuilder(args);

bool _running = true;
// Add services to the container.
builder.Services.AddHealthChecks()
    .AddCheck("self", () => _running ? HealthCheckResult.Healthy() : HealthCheckResult.Unhealthy());
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IEventRepository, EventsRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
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