using DotnetShop.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;
var enviroiment = builder.Environment;

// Add services to the container.
services.AddRazorPages();
services.AddControllersWithViews();
services.AddHttpContextAccessor();
services.AddSession();

services.AddScoped<IProductRepository, ProductRepository>();
services.AddScoped<ICategoryRepository, CategoryRepository>();

string dbPath, connectionString;
var provider = configuration.GetValue<string>("DatabaseProvider");

if (provider.Equals("Sqlite"))
{
    dbPath = Path.Combine(enviroiment.ContentRootPath, "App_Data\\DotnetShop.db");
    connectionString = string.Format(configuration.GetConnectionString("SqliteConnection"), dbPath);
}
else
{
    dbPath = Path.Combine(enviroiment.ContentRootPath, "App_Data\\DotnetShop.mdf");
    connectionString = string.Format(configuration.GetConnectionString("SqlServerConnection"), dbPath);
}

services.AddDbContext<DotnetShopDbContext>(
    options => _ = provider switch
        {
            "Sqlite" => options.UseSqlite(
                connectionString,
                x => x.MigrationsAssembly("DotnetShop.Migrations.Sqlite")),

            "SqlServer" => options.UseSqlServer(
                connectionString,
                x => x.MigrationsAssembly("DotnetShop.Migrations.SqlServer")),

            _ => throw new Exception($"Unsupported provider: {provider}")
        });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession(); // place this before UseRouting
app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "MyArea",
        pattern: "{area:exists}/{controller=Products}/{action=List}/{id?}");
    endpoints.MapRazorPages();
});

using (var scope = app.Services.CreateScope())
{
    var scopedProvider = scope.ServiceProvider;
    try
    {
        var context = scopedProvider.GetRequiredService<DotnetShopDbContext>();
        await DotnetShopSeed.SeedAsync(context);
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, "An error occurred seeding the DB.");
    }
}

app.Run();
