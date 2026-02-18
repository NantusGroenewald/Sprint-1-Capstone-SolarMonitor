using SolarMonitor.Application.Repositories;
using SolarMonitor.Application.UseCases;
using SolarMonitor.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IPanelRepository, InMemoryPanelRepository>();
builder.Services.AddScoped<RecordReadingCommandHandler>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();