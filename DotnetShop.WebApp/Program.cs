using DotnetShop.Data;
using DotnetShop.Domain;
using DotnetShop.WebApp.Services;
using Microsoft.AspNetCore.Identity;
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
services.AddScoped<ICartService, CartService>();
services.AddScoped<ICheckoutService, CheckoutService>();
services.AddScoped<ICustomerRepository, CustomerRepository>();
services.AddScoped<IOrderRepository, OrderRepository>();
services.AddScoped<UserManager<ApplicationUser>>();

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

services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequiredLength = 8;
    options.Password.RequireUppercase = true;
    options.Password.RequireDigit = true;
}).AddEntityFrameworkStores<DotnetShopDbContext>();

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
app.UseAuthentication();
app.UseSession(); // place this before UseRouting
app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "MyArea",
        pattern: "{area:exists}/{controller=Products}/{action=Index}/{id?}");
    endpoints.MapRazorPages();
});

app.Logger.LogInformation("Seeding Database...");

using (var scope = app.Services.CreateScope())
{
    var scopedProvider = scope.ServiceProvider;
    try
    {
        var context = scopedProvider.GetRequiredService<DotnetShopDbContext>();
        var userManager = scopedProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = scopedProvider.GetRequiredService<RoleManager<IdentityRole>>();

        await DotnetShopSeed.SeedAsync(context, userManager, roleManager, app.Logger);
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, "An error occurred seeding the DB.");
    }
}

app.Logger.LogInformation("LAUNCHING");
app.Run();
