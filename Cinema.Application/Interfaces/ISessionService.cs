using Cinema.Application.DTO.SessionDTOs;
using Cinema.Domain.Entities;

namespace Cinema.Application.Interfaces
{
    public interface ISessionService
    {
        Task<IEnumerable<GetSessionDTO>> GetAllAsync();
        Task<GetSessionDTO> GetByIdAsync(int id);
        Task AddAsync(AddSessionDTO sessionDto);
        Task UpdateAsync(UpdateSessionDTO sessionDto);
        Task DeleteByIdAsync(int id);
        Task<IEnumerable<GetSessionDTO>> GetByDateAsync(DateTime dateTime);
    }
}
