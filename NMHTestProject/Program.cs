using Microsoft.EntityFrameworkCore;
using NMHTestProject.Common;
using NMHTestProject.Data;
using NMHTestProject.Services;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

services.Configure<Configuration>(configuration.GetSection(nameof(Configuration)));
services.Configure<MessagingConfiguration>(configuration.GetSection(nameof(MessagingConfiguration)));

var connectionString = configuration.GetConnectionString("Default");
services.AddDbContext<AppDbContext>((builder) => builder.UseNpgsql(connectionString));

services.AddTransient<ICalculationService, CalculationService>();
services.AddTransient<ICalculationMessagingService, CalculationMessagingService>();
services.AddSingleton<IGlobalKeyStorageService, GlobalKeyStorageService>();
services.AddHostedService<QueueReaderBackgroundService>();
services.AddMemoryCache((options) =>
{
    options.ExpirationScanFrequency = TimeSpan.FromSeconds(1);
});

services.AddLogging((builder) =>
{
    builder.AddConsole();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
dbContext.Database.Migrate();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
