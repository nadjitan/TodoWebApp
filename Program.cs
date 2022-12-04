using Microsoft.EntityFrameworkCore;
using TodoWebApp.Data;

var builder = WebApplication.CreateBuilder(args);
//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var connectionString = "Server=localhost;Database=test;User=root;Password=;";
var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<TodoWebAppContext>(options =>
        //options.UseSqlServer(builder.Configuration.GetConnectionString("TodoWebAppContext") ?? throw new InvalidOperationException("Connection string 'TodoWebAppContext' not found.")));
        options.UseMySql(connectionString, serverVersion));
}
else if (builder.Environment.IsProduction())
{
    var server = Environment.GetEnvironmentVariable("MYSQLHOST");
    var db = Environment.GetEnvironmentVariable("MYSQLDATABASE");
    var port = Environment.GetEnvironmentVariable("MYSQLPORT");
    var user = Environment.GetEnvironmentVariable("MYSQLUSER");
    var pass = Environment.GetEnvironmentVariable("MYSQLPASSWORD");

    var prodConnection = $"server={server};database={db};port={port};user={user};password={pass};";

    builder.Services.AddDbContext<TodoWebAppContext>(options =>
        options.UseMySql(prodConnection, serverVersion));
}

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
