using Cinema.Application.DTO.SessionDTOs;

namespace Cinema.Application.Interfaces
{
    public interface ISessionService
    {
        Task<IEnumerable<GetSessionDTO>> GetAllAsync();
        Task<GetSessionDTO> GetByIdAsync(int id);
        Task AddAsync(AddSessionDTO sessionDto);
        Task UpdateAsync(UpdateSessionDTO sessionDto);
        Task DeleteByIdAsync(int id);
    }
}
