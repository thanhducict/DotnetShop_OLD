using DotnetShop.Data;
using DotnetShop.Domain;
using DotnetShop.Infrastructure.Initialization;
using DotnetShop.WebApp.Areas.Admin;
using DotnetShop.WebApp.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;
var environment = builder.Environment;

builder.Services.AddCors(options =>
{
    options.AddPolicy("Policy1",
        policy =>
        {
            policy.WithOrigins("https://localhost:7052")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

services.AddEndpointsApiExplorer();
services.AddSwaggerGen(setupAction =>
{
    var xmlCommentsFile = "DotnetShop.APIs.xml";
    var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

    setupAction.IncludeXmlComments(xmlCommentsFullPath);

    setupAction.AddSecurityDefinition("DotnetShopApiBearerAuth", new OpenApiSecurityScheme()
    {
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        Description = "Input a valid token to access this API."
    });

    setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "DotnetShopApiBearerAuth" }
            }, new List<string>() }
    });
});

// Add services to the container.
services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true;
}).AddXmlDataContractSerializerFormatters();

services.AddRazorPages();
services.AddHttpContextAccessor();
services.AddSession();

services.AddScoped<IProductRepository, ProductRepository>();
services.AddScoped<ICategoryRepository, CategoryRepository>();
services.AddScoped<ICartService, CartService>();
services.AddScoped<ICheckoutService, CheckoutService>();
services.AddScoped<ICustomerRepository, CustomerRepository>();
services.AddScoped<IOrderRepository, OrderRepository>();
services.AddScoped<UserManager<ApplicationUser>>();
services.AddScoped<Helpers>();

//TODO: Why is the order important here?
var allModules = AppDomain.CurrentDomain.GetAssemblies()
               .SelectMany(s => s.GetTypes()).Where(p => typeof(IInitialization).IsAssignableFrom(p) && p.IsClass);

foreach (var item in allModules)
{
    var module = Activator.CreateInstance(item) as IInitialization;
    if (module != null)
    {
        module.ConfigureContainer(services);
    }
}

string dbPath, connectionString;
var provider = configuration.GetValue<string>("DatabaseProvider");

if (provider.Equals("Sqlite"))
{
    dbPath = Path.Combine(environment.ContentRootPath, "App_Data\\DotnetShop.db");
    connectionString = string.Format(configuration.GetConnectionString("SqliteConnection"), dbPath);
}
else
{
    dbPath = Path.Combine(environment.ContentRootPath, "App_Data\\DotnetShop.mdf");
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

services.AddAuthentication("Bearer").AddJwtBearer(options =>
{
    options.TokenValidationParameters = new()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Authentication:Issuer"],
        ValidAudience = builder.Configuration["Authentication:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForKey"])),
        ClockSkew = TimeSpan.Zero
    };
});

services.AddAuthorization(options =>
{
    options.AddPolicy("MustBeAdminAndLeader", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireRole("Admin");
        policy.RequireClaim("level", "Leadership");
    });
});

services.AddApiVersioning(setupAction =>
{
    setupAction.AssumeDefaultVersionWhenUnspecified = true;
    setupAction.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    setupAction.ReportApiVersions = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

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
app.UseCors();
app.UseAuthentication();
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

        await DotnetShopSeeding.SeedAsync(context, userManager, roleManager, app.Logger);
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, "An error occurred seeding the DB.");
    }
}

app.Logger.LogInformation("LAUNCHING");
app.Run();
