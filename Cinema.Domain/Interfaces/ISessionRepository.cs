using Cinema.Domain.Entities;

namespace Cinema.Domain.Interfaces
{
    public interface ISessionRepository
    {
        Task AddAsync(SessionEntity session);
        Task UpdateAsync(long sessionId, SessionEntity session);
        Task<SessionEntity> GetByIdAsync(long sessionId);
        Task<IEnumerable<SessionEntity>> GetAllAsync();
        Task<IEnumerable<SessionEntity>> GetByDateAsync(DateTime dateTime);
        Task DeleteByIdAsync(long sessionId);
    }
}
