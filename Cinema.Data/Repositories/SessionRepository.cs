using Cinema.Business.Entities;
using Cinema.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Data.Repositories;

public class SessionRepository : ISessionRepository
{
    private readonly CinemaDbContext _context;
    public SessionRepository(CinemaDbContext context)
    {
        _context = context;
    }
    public Task Add(SessionEntity session)
    {
        throw new NotImplementedException();
    }

    public Task Delete(long sessionId)
    {
        throw new NotImplementedException();
    }

    public Task Get(long sessionId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<SessionEntity>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<SessionEntity>> GetByDate(DateTime dateTime)
    {
        throw new NotImplementedException();
    }

    public Task Update(long sessionId, SessionEntity session)
    {
        throw new NotImplementedException();
    }
}
