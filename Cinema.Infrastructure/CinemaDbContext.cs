using Cinema.Domain.Entities;
using Cinema.Infrastructure.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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
        

        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new HallConfiguration());
        builder.ApplyConfiguration(new MovieConfiguration());
        builder.ApplyConfiguration(new SessionConfiguration());
        builder.ApplyConfiguration(new TicketConfiguration());

        base.OnModelCreating(builder);
    }
}
