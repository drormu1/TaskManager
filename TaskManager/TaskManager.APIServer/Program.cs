using Microsoft.EntityFrameworkCore;
using TaskManager.Data.Repositories;
using TaskManager.Infra;
using TaskManager.Infra.Repositories;
using TaskManager.Logic;
using TaskManager.Logic.TaskTypes;

var builder = WebApplication.CreateBuilder(args);

// Configure logging
ConfigureLogger(builder);


// Configure CORS for all origins
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

RegisterService(builder);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    //options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
});

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
    builder.Services.AddScoped<IStatusService, StatusService>();
    builder.Services.AddScoped<IManagedTaskRepository, ManagedTaskRepository>();
    builder.Services.AddScoped<IAdminLogic, AdminLogic>();
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<ITaskTypeRepository, TaskTypeRepository>();
    builder.Services.AddScoped<ITaskStatusRepository, TaskStatusRepository>();
    builder.Services.AddScoped<ITaskLogic, TaskLogic>();


    builder.Services.AddScoped<ProcurementTaskValidator>();
    builder.Services.AddScoped<DevelopmentTaskValidator>();

    builder.Services.AddScoped<ITaskTypeValidator, ProcurementTaskValidator>();
    builder.Services.AddScoped<ITaskTypeValidator, DevelopmentTaskValidator>();
    builder.Services.AddScoped<ITaskTypeValidatorFactory, TaskTypeValidatorFactory>();
    builder.Services.AddScoped<ITaskStatusHistoryRepository, TaskStatusHistoryRepository>();
}

static void ConfigureLogger(WebApplicationBuilder builder)
{
    builder.Logging.ClearProviders();
    builder.Logging.AddConsole();
    builder.Logging.AddDebug();
}
