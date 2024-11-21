using AutoMapper;
using FIAP.IRRIGACAO.API.Data.Context;
using FIAP.IRRIGACAO.API.Data.Repository;
using FIAP.IRRIGACAO.API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");

builder.Services.AddDbContext<DatabaseContext>(
    opt => opt.UseOracle(connectionString).EnableSensitiveDataLogging(true)
);

var mapperConfig = new AutoMapper.MapperConfiguration(c => {
    c.AllowNullCollections = true;
    c.AllowNullDestinationValues = true;
    c.AddProfile(typeof(FaucetProfile));

});

IMapper mapper = mapperConfig.CreateMapper();

// Add services to the container.
builder.Services.AddScoped<IFaucetRepository, FaucetRepository>();
builder.Services.AddScoped<IFaucetService, FaucetService>();
builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddAutoMapper(typeof(LocationProfile));
builder.Services.AddAutoMapper(typeof(FaucetProfile));
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
    pattern: "{controller=Faucet}/{action=Index}/{id?}");

app.Run();
