using SolarMonitor.Application.Repositories;
using SolarMonitor.Application.UseCases;
using SolarMonitor.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using SolarMonitor.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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