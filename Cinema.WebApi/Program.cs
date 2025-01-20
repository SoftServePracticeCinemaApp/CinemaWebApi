using Cinema.Data;
using Cinema.Data.Utils;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInMemoryDataBase();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
