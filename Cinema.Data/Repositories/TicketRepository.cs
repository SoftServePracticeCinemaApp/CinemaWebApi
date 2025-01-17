using Cinema.Business.Entities;
using Cinema.Business.Interfaces;
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
    public Task Add(TicketEntity ticket)
    {
        throw new NotImplementedException();
    }

    public Task<TicketEntity> Get(long Id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TicketEntity>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<TicketEntity> GetById(long Id)
    {
        throw new NotImplementedException();
    }

    public Task<TicketEntity> GetByMovieId(int movieId)
    {
        throw new NotImplementedException();
    }

    public Task<TicketEntity> GetByMovieName(string movieName)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TicketEntity>> GetByUserId(string Id)
    {
        throw new NotImplementedException();
    }

    public Task Remove(long Id)
    {
        throw new NotImplementedException();
    }

    public Task Update(long Id, TicketEntity ticketEntity)
    {
        throw new NotImplementedException();
    }
}
