using Cinema.Business.Entities;
using Cinema.Business.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Data.Repositories;

public class TicketRepository : ITicketRepository
{
    private readonly CinemaDbContext _context;
    public TicketRepository(CinemaDbContext context)
    {
        _context = context;
    }
    public async Task Add(TicketEntity ticket)
    {
        if(ticket != null) await _context.Tickets.AddAsync(ticket);
    }

    public async Task<IEnumerable<TicketEntity>> GetAll() 
        => await _context.Tickets
        .AsNoTracking()
        .ToListAsync();

    public async Task<TicketEntity> GetById(long Id)
    {
        var ticketInDb = await _context.Tickets
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == Id);

        if (ticketInDb == null) throw new ArgumentException($"Ticket with Id {Id} does not exist");
        return ticketInDb;
    }

    public async Task<IEnumerable<TicketEntity>> GetByMovieId(int movieId)
    {
        var ticketInDb = await _context.Tickets
            .AsNoTracking()
            .Where(t => t.MovieId == movieId).ToListAsync();

        if(ticketInDb == null) throw new ArgumentException($"Ticket with movie id {movieId} does not exist");
        return ticketInDb;
    }

    public async Task<IEnumerable<TicketEntity>> GetByUserId(string UserId)
    {
        var ticketsInDb = await _context.Tickets
            .AsNoTracking()
            .Where(t => t.UserId == UserId)
            .ToListAsync();

        if(ticketsInDb.Count <= 0) return new List<TicketEntity>();

        return ticketsInDb;
    }

    public async Task Remove(long Id)
    {
        var ticketInDb = await _context.Tickets.AsNoTracking().FirstOrDefaultAsync(t => t.Id == Id);

        if (ticketInDb == null) throw new InvalidOperationException($"Ticket with Id {Id} does not exist");
        else await Task.Run(() => _context.Remove(ticketInDb));
    }

    public async Task Update(long Id, TicketEntity ticketEntity)
    {
        var ticketInDb = await _context.Tickets.AsNoTracking().FirstOrDefaultAsync(t => t.Id == Id);

        if (ticketInDb == null) throw new InvalidOperationException($"Ticket with Id {Id} does not exist");

        ticketInDb.SessionId = Id;
        ticketInDb.MovieId = Id;
        ticketInDb.Row = ticketEntity.Row;
    }

    public async Task Save() => await _context.SaveChangesAsync();
}
