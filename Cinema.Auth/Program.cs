using Microsoft.AspNetCore.Identity;
using Cinema.Business.Interfaces;
using Cinema.Business.Services;
using Cinema.Domain.Entities;
using Cinema.Infrastructure.Repositories;
using Cinema.Infrastructure;
using Cinema.Business.Services.IServices;
using Cinema.Business.Options;
using Microsoft.EntityFrameworkCore;
using Cinema.Infrastructure.Utils;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en-US");

// Add services to the container.
var useInMemoryDB = builder.Configuration.GetValue<bool>("UseInMemoryDB");
builder.Services.AddControllers();
if (useInMemoryDB)
{
	builder.Services.AddInMemoryDataBase();
}
else
{
	builder.Services.AddDbContext<CinemaDbContext>(options =>
		options.UseSqlServer(builder.Configuration.GetConnectionString("CinemaDb"),
		ServiceProviderOptions => ServiceProviderOptions.EnableRetryOnFailure()));
}
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.AddIdentity<UserEntity, IdentityRole>().AddEntityFrameworkStores<CinemaDbContext>();
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("ApiSettings:JwtOptions"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
options.AddPolicy("AllowAll", builder =>
{
    builder.AllowAnyOrigin()    // Allow requests from any origin
           .AllowAnyMethod()    // Allow any HTTP method
           .AllowAnyHeader();   // Allow any headers
});
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
