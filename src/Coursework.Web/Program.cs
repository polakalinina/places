using Coursework.Core.Services;
using Coursework.Data;
using Coursework.Web;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Environment.IsEnvironment(Environments.Development)
    ? "Server=localhost;Port=5432;Database=coursework_db;User Id=postgres;Password=postgres;"
    : Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")!;

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services
    .AddDbContext<ApplicationContext>(options => options.UseNpgsql(connectionString))
    .AddScoped<PlacesService>()
    .AddScoped<ImagesService>()
    .AddScoped<DistrictsService>()
    .AddScoped<TypesService>();

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

app.MapRazorPages();

app.MigrateDatabase();

await app.AddSeedData();

app.Run();