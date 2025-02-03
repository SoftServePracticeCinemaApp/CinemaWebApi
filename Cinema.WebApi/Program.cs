using Cinema.Infrastructure.Utils;
using System.Text;
using Cinema.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Cinema.WebApi.Filters;
using System;
using Cinema.Domain.Entities;
using Microsoft.Extensions.Options;
using Cinema.Application.Helpers;
using Cinema;
using Cinema.Application.Helpers.Interfaces;
using Cinema.Application.Interfaces;
using Cinema.Application.Services;
using Cinema.Domain.Interfaces;
using Cinema.Infrastructure.Repositories;


public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var useInMemoryDB = builder.Configuration.GetValue<bool>("UseInMemoryDB");


        builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

        var secret = builder.Configuration.GetValue<string>("ApiSettings:Secret");
        var issuer = builder.Configuration.GetValue<string>("ApiSettings:Issuer");
        var audience = builder.Configuration.GetValue<string>("ApiSettings:Audience");

        builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
        builder.Services.AddControllers(options =>
        {
            options.Filters.Add<GlobalExceptionFilter>();
        });

        var key = Encoding.ASCII.GetBytes(secret);

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

        builder.Services.Configure<TmdbSettings>(builder.Configuration.GetSection("TMDB"));
        builder.Services.AddHttpClient<TmdbService>();
        builder.Services.AddScoped<TmdbService>();

        builder.Services.AddScoped<IResponses, Responses>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

        builder.Services.AddScoped<IMovieService, MovieService>();
        builder.Services.AddScoped<IMovieRepository, MovieRepository>();

        builder.Services.AddScoped<ISessionService, SessionService>();
        builder.Services.AddScoped<ISessionRepository, SessionRepository>();

        builder.Services.AddScoped<ITicketService, TicketService>();
        builder.Services.AddScoped<ITicketRepository, TicketRepository>();

        builder.Services.AddScoped<IHallRepository, HallRepository>();

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.TokenValidationParameters = new()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = issuer,
                ValidateAudience = true,
                ValidAudience = audience
            };
        });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();


        builder.Services.AddAuthorization();

        var app = builder.Build();
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cinema API V1");
            c.RoutePrefix = string.Empty;
        });

        if (useInMemoryDB)
        {
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<CinemaDbContext>();
                SeedData(context);
            }
        }
        else
        {
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<CinemaDbContext>();
                context.Database.EnsureCreated(); // create database if not exists
            }
        }


        app.MapControllers();
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.Run();
    }


 private static void SeedData(CinemaDbContext context)
    {
        var name = "John";
        var lastName = "Doe";

        // Додавання користувачів

        context.Users.AddRange(
            new UserEntity { Id = "1",  Name = "JonDoe", LastName = "Doe", UserName = $"{name} {lastName}", Tickets = [] },
            new UserEntity { Id = "2", Name = "JonDoe", LastName = "Doe", UserName = $"{name} {lastName}", Tickets = [] }
        );

        context.SaveChanges();

        // Додавання залів
        var halls = new List<HallEntity>
    {
        new HallEntity { Id = 1, Seats = new List<List<int>> { new List<int> { 1, 2, 3 }, new List<int> { 4, 5, 6 } } },
        new HallEntity { Id = 2, Seats = new List<List<int>> { new List<int> { 1, 2 }, new List<int> { 3, 4 } } }
    };
        context.Halls.AddRange(halls);

        // Додавання фільмів
        var movies = new List<MovieEntity>
    {
        new MovieEntity { Id = 1, SearchId = 101, CinemaRating = 8.5 },
        new MovieEntity { Id = 2, SearchId = 102, CinemaRating = 7.8 }
    };
        context.Movies.AddRange(movies);

        // Додавання сеансів
        var sessions = new List<SessionEntity>
    {
        new SessionEntity { Id = 1, MovieId = 1, Date = DateTime.Now.AddDays(1), HallId = 1 },
        new SessionEntity { Id = 2, MovieId = 2, Date = DateTime.Now.AddDays(2), HallId = 2 }
    };
        context.Sessions.AddRange(sessions);

        // Додавання квитків
        var tickets = new List<TicketEntity>
    {
        new TicketEntity { Id = 1, SessionId = 1, UserId = "1", MovieId = 1, Row = 1 },
        new TicketEntity { Id = 2, SessionId = 2, UserId = "2", MovieId = 2, Row = 2 }
    };
        context.Tickets.AddRange(tickets);

        context.SaveChanges();
    }
}
