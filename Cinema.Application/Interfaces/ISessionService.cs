using Cinema.Application.DTO.SessionDTOs;

namespace Cinema.Application.Interfaces
{
    interface ISessionService
    {
        Task<IEnumerable<GetSessionDTO>> GetAllAsync();
        Task<GetSessionDTO> GetByIdAsync(int id);
        Task AddAsync(AddSessionDTO sessionDto);
        Task UpdateAsync(UpdateSessionDTO sessionDto);
        Task DeleteAsync(int id);
    }
}
