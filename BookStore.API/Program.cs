using BookStore.API.Data;
using BookStore.API.DataLoaders;
using BookStore.API.Schema.Mutations;
using BookStore.API.Schema.Queries;
using BookStore.API.Schema.Subscriptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;
var environment = builder.Environment;

builder.Services.AddCors(options =>
{
    options.AddPolicy("Policy2",
        policy =>
        {
            policy.WithOrigins("https://localhost:7100")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

services.AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddSubscriptionType<Subscription>()
    .AddType<BookType>()
    .AddType<AuthorType>()
    .AddFiltering()
    .AddSorting()
    .AddProjections();
services.AddInMemorySubscriptions();

string dbPath = System.IO.Path.Combine(environment.ContentRootPath, "App_Data\\BookStore.db");
string connectionString = string.Format(configuration.GetConnectionString("default"), dbPath);
services.AddPooledDbContextFactory<BookStoreDbContext>(o => o.UseSqlite(connectionString));

services.AddScoped<CategoryDataLoader>();

var app = builder.Build();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
            System.IO.Path.Combine(environment.ContentRootPath, @"Assets/Images")),
    RequestPath = "/assets/images"
});

app.UseRouting();
app.UseWebSockets();
app.UseCors("Policy2");

app.UseEndpoints(endpoints =>
{
    endpoints.MapGraphQL();
});

using (var scope = app.Services.CreateScope())
{
    var scopedProvider = scope.ServiceProvider;
    try
    {
        var contextFactory = scopedProvider.GetRequiredService<IDbContextFactory<BookStoreDbContext>>();
        await BookStoreSeeding.SeedAsync(contextFactory.CreateDbContext(), app.Logger);
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, "An error occurred seeding the DB.");
    }
}

app.Run();
