using Microsoft.AspNetCore.Identity;
using Cinema.Business.Interfaces;
using Cinema.Business.Services;
using Cinema.Domain.Entities;
using Cinema.Infrastructure.Repositories;
using Cinema.Infrastructure;
using Cinema.Business.Services.IServices;
using Cinema.Business.Options;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.AddIdentity<UserEntity, IdentityRole>().AddEntityFrameworkStores<CinemaDbContext>();
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("ApiSettings:JwtOptions"));
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

app.UseAuthorization();

app.MapControllers();

app.Run();
