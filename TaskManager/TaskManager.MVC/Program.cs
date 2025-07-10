using Microsoft.Extensions.Options;
using TaskManager.MVC.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));

// Register HttpClient with base address from config
builder.Services.AddHttpClient<ITaskService, TaskService>((sp, client) =>
{
    var apiSettings = sp.GetRequiredService<IOptions<ApiSettings>>().Value;
    client.BaseAddress = new Uri(apiSettings.BaseUrl);
});

// Add services to the container
builder.Services.AddControllersWithViews();

//// Configure HttpClient for API communication
//builder.Services.AddHttpClient<ITaskService, TaskService>(client => {
//    client.BaseAddress = new Uri("http://localhost:5198/");
//});

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Task}/{action=Index}/{id?}");

app.Run();
