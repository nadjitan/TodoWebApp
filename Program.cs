using Microsoft.EntityFrameworkCore;
using TodoWebApp.Data;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<TodoWebAppContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("TodoWebAppContext") ?? throw new InvalidOperationException("Connection string 'TodoWebAppContext' not found.")));
}
else if (builder.Environment.IsProduction())
{
    builder.Services.AddDbContext<TodoWebAppContext>(options =>
        options.UseMySQL(Environment.GetEnvironmentVariable("DATABASE_URL")!));
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
