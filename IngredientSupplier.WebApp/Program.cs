using IngredientSupplier.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;
var enviroiment = builder.Environment;

// Add services to the container.
services.AddRazorPages();
services.AddControllersWithViews();

services.AddScoped<IIngredientRepository, IngredientRepository>();
services.AddScoped<ICategoryRepository, CategoryRepository>();

string dbPath, connectionString;
var provider = configuration.GetValue<string>("DatabaseProvider");

if (provider.Equals("Sqlite"))
{
    dbPath = Path.Combine(enviroiment.ContentRootPath, "App_Data\\IngredientSupplier.db");
    connectionString = string.Format(configuration.GetConnectionString("SqliteConnection"), dbPath);
}
else
{
    dbPath = Path.Combine(enviroiment.ContentRootPath, "App_Data\\IngredientSupplier.mdf");
    connectionString = string.Format(configuration.GetConnectionString("SqlServerConnection"), dbPath);
}

services.AddDbContext<IngredientSupplierDbContext>(
    options => _ = provider switch
        {
            "Sqlite" => options.UseSqlite(
                connectionString,
                x => x.MigrationsAssembly("IngredientSupplier.Migrations.Sqlite")),

            "SqlServer" => options.UseSqlServer(
                connectionString,
                x => x.MigrationsAssembly("IngredientSupplier.Migrations.SqlServer")),

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

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "MyArea",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
    endpoints.MapRazorPages();
});

app.Run();
