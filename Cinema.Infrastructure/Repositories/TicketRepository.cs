using Cinema.Domain.Entities;
using Cinema.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Infrastructure.Repositories;

public class TicketRepository : ITicketRepository
{
    private readonly CinemaDbContext _context;
    public TicketRepository(CinemaDbContext context)
    {
        _context = context;
    }
    public async Task AddAsync(TicketEntity ticket)
    {
        if(ticket != null) await _context.Tickets.AddAsync(ticket);
    }

    public async Task<IEnumerable<TicketEntity>> GetAllAsync() 
        => await _context.Tickets
        .AsNoTracking()
        .ToListAsync();

    public async Task<TicketEntity> GetByIdAsync(long Id)
    {
        var ticketInDb = await _context.Tickets
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == Id);

        if (ticketInDb == null) throw new ArgumentException($"Ticket with Id {Id} does not exist");
        return ticketInDb;
    }

    public async Task<IEnumerable<TicketEntity>> GetByMovieIdAsync(int movieId)
    {
        var ticketInDb = await _context.Tickets
            .AsNoTracking()
            .Where(t => t.MovieId == movieId).ToListAsync();

        if(ticketInDb == null) throw new ArgumentException($"Ticket with movie id {movieId} does not exist");
        return ticketInDb;
    }

    public async Task<IEnumerable<TicketEntity>> GetByUserIdAsync(string UserId)
    {
        var ticketsInDb = await _context.Tickets
            .AsNoTracking()
            .Where(t => t.UserId == UserId)
            .ToListAsync();

        if(ticketsInDb.Count <= 0) return new List<TicketEntity>();

        return ticketsInDb;
    }

    public async Task DeleteAsync(long Id)
    {
        var ticketInDb = await _context.Tickets.AsNoTracking().FirstOrDefaultAsync(t => t.Id == Id);

        if (ticketInDb == null) throw new InvalidOperationException($"Ticket with Id {Id} does not exist");
        else await Task.Run(() => _context.Remove(ticketInDb));
    }

    public async Task UpdateAsync(long Id, TicketEntity ticketEntity)
    {
        var ticketInDb = await _context.Tickets.AsNoTracking().FirstOrDefaultAsync(t => t.Id == Id);

        if (ticketInDb == null) throw new InvalidOperationException($"Ticket with Id {Id} does not exist");

        ticketInDb.SessionId = Id;
        ticketInDb.MovieId = Id;
        ticketInDb.Row = ticketEntity.Row;
    }
    public async Task<IEnumerable<TicketEntity>> GetBySessionIdAsync(long sessionId)
    {
        return await _context.Tickets
            .Where(t => t.SessionId == sessionId)
            .Include(t => t.User)
            .Include(t => t.Movie)
            .ToListAsync();
    }

    public async Task<IEnumerable<TicketEntity>> GetTicketsForHallAsync(int hallId)
    {
        return await _context.Tickets
            .Where(t => t.Session != null && t.Session.HallId == hallId)
            .Include(t => t.Session)
            .Include(t => t.Movie)
            .ToListAsync();
    }
}
