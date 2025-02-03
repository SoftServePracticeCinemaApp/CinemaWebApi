 using Cinema.Domain.Entities;
using Cinema.Infrastructure.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Cinema.Infrastructure;

public class CinemaDbContext(DbContextOptions<CinemaDbContext> options) : IdentityDbContext<UserEntity>(options)
{
    public DbSet<MovieEntity> Movies {  get; set; }
    public DbSet<TicketEntity> Tickets {  get; set; }
    public DbSet<SessionEntity> Sessions { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<HallEntity> Halls { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<TicketEntity>()
        .HasOne(t => t.Session)
        .WithMany()
        .HasForeignKey(t => t.SessionId)
        .OnDelete(DeleteBehavior.Restrict); 

        builder.Entity<SessionEntity>()
            .HasOne(s => s.Movie)
            .WithMany()
            .HasForeignKey(s => s.MovieId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<SessionEntity>()
            .HasOne(s => s.Hall)
            .WithMany()
            .HasForeignKey(s => s.HallId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<TicketEntity>()
            .HasOne(t => t.User)
            .WithMany(u => u.Tickets)
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<TicketEntity>()
            .HasOne(t => t.Movie)
            .WithMany()
            .HasForeignKey(t => t.MovieId)
            .OnDelete(DeleteBehavior.Restrict);


        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new HallConfiguration());
        builder.ApplyConfiguration(new MovieConfiguration());
        builder.ApplyConfiguration(new SessionConfiguration());
        builder.ApplyConfiguration(new TicketConfiguration());

        builder.Entity<HallEntity>()
        .Ignore(h => h.Seats);

        base.OnModelCreating(builder);
    }


}
