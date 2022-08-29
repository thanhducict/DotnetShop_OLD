using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
var environment = builder.Environment;

// Add services to the container.
builder.Services.AddRazorPages();

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
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
            Path.Combine(environment.ContentRootPath, @"ClientApp/dist")),
    RequestPath = "/ClientResources"
});

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
