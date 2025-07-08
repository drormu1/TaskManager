using Microsoft.EntityFrameworkCore;
using TaskManager.Data.Repositories;
using TaskManager.Infra;
using TaskManager.Infra.Repositories;
using TaskManager.Logic.Services;

var builder = WebApplication.CreateBuilder(args);

// Configure logging
ConfigureLogger(builder);


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

RegisterService(builder);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
var app = builder.Build();

var logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("Application starting up.");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    logger.LogInformation("Running in Development environment. Enabling Swagger UI.");
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();
//app.UseHttpsRedirection();

logger.LogInformation("Application is ready to handle requests.");
app.Run();

static void RegisterService(WebApplicationBuilder builder)
{
    builder.Services.AddScoped<IManagedTaskRepository, ManagedTaskRepository>();
    builder.Services.AddScoped<IAdminService, AdminService>();
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<ITaskTypeRepository, TaskTypeRepository>();
    builder.Services.AddScoped<ITaskStatusRepository, TaskStatusRepository>();
}

static void ConfigureLogger(WebApplicationBuilder builder)
{
    builder.Logging.ClearProviders();
    builder.Logging.AddConsole();
    builder.Logging.AddDebug();
}

//dotnet ef migrations add InitialCreate --project TaskManager.Infra --startup-project TaskManager.Api
//dotnet ef database update --project TaskManager.Infra --startup-project TaskManager.Api