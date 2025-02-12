using Cinema.Domain.Entities;
using Cinema.Infrastructure.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Infrastructure;

public class CinemaDbContext(DbContextOptions<CinemaDbContext> options) : IdentityDbContext<UserEntity>(options)
{
    public DbSet<MovieEntity> Movies { get; set; }
    public DbSet<TicketEntity> Tickets { get; set; }
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
            .WithMany(m => m.Sessions)
            .HasForeignKey(s => s.MovieId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<SessionEntity>()
            .HasOne(s => s.Hall)
            .WithMany(h => h.Sessions)
            .HasForeignKey(s => s.HallId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<TicketEntity>()
            .HasOne(t => t.User)
            .WithMany(u => u.Tickets)
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // Квиток -> Фільм (залишаємо Cascade)
        builder.Entity<TicketEntity>()
            .HasOne(t => t.Movie)
            .WithMany(m => m.Tickets)
            .HasForeignKey(t => t.MovieId)
            .OnDelete(DeleteBehavior.Cascade);

        // Решта конфігурації...
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