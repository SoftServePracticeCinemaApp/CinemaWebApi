using Cinema.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Business.Interfaces
{
    public interface ISessionRepository
    {
        Task Add(SessionEntity session);
        Task Update(long sessionId, SessionEntity session);
        Task Get(long sessionId);
        Task<IEnumerable<SessionEntity>> GetAll();
        Task<IEnumerable<SessionEntity>> GetByDate(DateTime dateTime);
        Task Delete(long sessionId);
    }
}
