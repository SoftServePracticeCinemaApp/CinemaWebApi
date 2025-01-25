using Cinema.Domain.Entities;

namespace Cinema.Domain.Interfaces
{
    public interface ISessionRepository
    {
        Task Add(SessionEntity session);
        Task Update(long sessionId, SessionEntity session);
        Task<SessionEntity> Get(long sessionId);
        Task<IEnumerable<SessionEntity>> GetAll();
        Task<IEnumerable<SessionEntity>> GetByDate(DateTime dateTime);
        Task Delete(long sessionId);
        Task Save();
    }
}
