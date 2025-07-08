using Microsoft.EntityFrameworkCore;
using TaskManager.Data.Repositories;
using TaskManager.Infra;
using TaskManager.Infra.Repositories;
using TaskManager.Logic.Services;

var builder = WebApplication.CreateBuilder(args);

// Dependency injection configuration
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();

//dotnet ef migrations add InitialCreate --project TaskManager.Infra --startup-project TaskManager.Api
//dotnet ef database update --project TaskManager.Infra --startup-project TaskManager.Api