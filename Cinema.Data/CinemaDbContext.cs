using Cinema.Business.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Data;

public class CinemaDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<MovieEntity> Movies {  get; set; }
    public DbSet<TicketEntity> Tickets {  get; set; }
    public DbSet<SessionEntity> Sessions { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<HallEntity> Halls { get; set; }
}
