using Cinema.Infrastructure.Utils;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInMemoryDataBase();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
