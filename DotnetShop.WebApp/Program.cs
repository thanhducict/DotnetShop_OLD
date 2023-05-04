using DotnetShop.Data;
using DotnetShop.Data.Domain;
using DotnetShop.Data.Repositories;
using DotnetShop.Infrastructure.Initialization;
using DotnetShop.WebApp.Areas.Admin.Helpers;
using DotnetShop.WebApp.Middleware;
using DotnetShop.WebApp.ModelBinding;
using DotnetShop.WebApp.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
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
            policy.WithOrigins("https://localhost:7200")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .WithExposedHeaders("x-pagination");
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

services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true;
}).AddXmlDataContractSerializerFormatters();

services.AddMvc(options =>
{
    options.ModelBinderProviders.Insert(0, new CSVModelBinderProvider());
});
services.AddRazorPages();
services.AddHttpContextAccessor();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(1);
});

services.AddScoped<IProductRepository, ProductRepository>();
services.AddScoped<ICategoryRepository, CategoryRepository>();
services.AddScoped<ICartService, CartService>();
services.AddScoped<ICheckoutService, CheckoutService>();
services.AddScoped<ICustomerRepository, CustomerRepository>();
services.AddScoped<IOrderRepository, OrderRepository>();
services.AddScoped<UserManager<ApplicationUser>>();
services.AddScoped<IHelpers, Helpers>();

var allModules = AppDomain.CurrentDomain.GetAssemblies()
               .SelectMany(s => s.GetTypes()).Where(p => typeof(IInitialization).IsAssignableFrom(p) && p.IsClass);

foreach (var item in allModules)
{
    var module = Activator.CreateInstance(item) as IInitialization;
    if (module != null)
    {
        module.ConfigureContainer(builder);
    }
}

string dbPath, connectionString;
var provider = configuration.GetValue<string>("DatabaseProvider");

if (Directory.Exists(Path.Combine(environment.ContentRootPath, "App_Data")) == false)
{
    Directory.CreateDirectory(Path.Combine(environment.ContentRootPath, "App_Data"));
}

switch (provider)
{
    case "SqlServer":
        dbPath = Path.Combine(environment.ContentRootPath, "App_Data\\DotnetShop.mdf");
        connectionString = string.Format(configuration.GetConnectionString("SqlServer"), dbPath);
        services.AddDbContext<DotnetShopDbContext>(options => options.UseSqlServer(connectionString));
        break;
    default:
        dbPath = Path.Combine(environment.ContentRootPath, "App_Data\\DotnetShop.db");
        connectionString = string.Format(configuration.GetConnectionString("Sqlite"), dbPath);
        Console.WriteLine(connectionString);
        services.AddDbContext<DotnetShopDbContext>(options => options.UseSqlite(connectionString));
        break;
}

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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
if (environment.IsDevelopment())
{
    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(
            Path.Combine(environment.ContentRootPath, @"../DotnetShop.Admin.Warehouses/Assets")),
        RequestPath = "/warehouses-assets"
    });

    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(
            Path.Combine(environment.ContentRootPath, @"../DotnetShop.Books/Assets")),
        RequestPath = "/books-assets"
    });
}

app.UseSession();
app.UseRouting();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.UseChristmasPage();
app.UseFeatureSwitch();

app.MapControllerRoute(
    name: "MyArea",
    pattern: "{area:exists}/{controller=Products}/{action=Index}/{id?}");
app.MapRazorPages();

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
