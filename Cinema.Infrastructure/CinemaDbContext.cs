using Cinema.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Infrastructure;

public class CinemaDbContext(DbContextOptions options) : IdentityDbContext(options)
{
    public DbSet<MovieEntity> Movies {  get; set; }
    public DbSet<TicketEntity> Tickets {  get; set; }
    public DbSet<SessionEntity> Sessions { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<HallEntity> Halls { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
