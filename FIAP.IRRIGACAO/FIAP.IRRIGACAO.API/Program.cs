using AutoMapper;
using FIAP.IRRIGACAO.API.Data.Context;
using FIAP.IRRIGACAO.API.Model;
using FIAP.IRRIGACAO.API.Profile;
using FIAP.IRRIGACAO.API.Repository;
using FIAP.IRRIGACAO.API.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");

builder.Services.AddDbContext<OracleContext>(
    opt => opt.UseOracle(connectionString).EnableSensitiveDataLogging(true)
);

var mapperConfig = new AutoMapper.MapperConfiguration(c => {
    c.AllowNullCollections = true;
    c.AllowNullDestinationValues = true;
    c.AddProfile(typeof(FaucetProfile));
    c.AddProfile(typeof(LocationProfile));
});

IMapper mapper = mapperConfig.CreateMapper();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<OracleContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/api/v1/user/login";
    options.LogoutPath = "/api/v1/user/logout";
    options.Events = new CookieAuthenticationEvents
    {
        OnRedirectToLogin = context =>
        {
            context.Response.StatusCode = 401;
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync("{\"error\":\"Acesso não autorizado. Por favor, efetue o login.\"}");
        }
    };
});

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IGenericService<,,>), typeof(GenericService<,,>));
builder.Services.AddScoped<IFaucetRepository, FaucetRepository>();
builder.Services.AddScoped<IFaucetService, FaucetService>();
builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddAutoMapper(typeof(LocationProfile));
builder.Services.AddAutoMapper(typeof(FaucetProfile));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
