using Cinema.Application.DTO.SessionDTOs;
using Cinema.Application.Helpers.Interfaces;
using Cinema.Domain.Entities;

namespace Cinema.Application.Interfaces
{
        public interface ISessionService
        {
            Task<IBaseResponse<List<GetSessionDTO>>> GetAllSessionsAsync();
            Task<IBaseResponse<GetSessionDTO>> GetSessionByIdAsync(long id);
            Task<IBaseResponse<string>> AddSessionAsync(AddSessionDTO sessionDto);
            Task<IBaseResponse<string>> UpdateSessionAsync(long id, UpdateSessionDTO sessionDto);
            Task<IBaseResponse<string>> DeleteSessionAsync(long id);
            Task<IBaseResponse<List<GetSessionDTO>>> GetSessionsByDateAsync(DateTime date);
            Task<IBaseResponse<List<GetSessionDTO>>> GetSessionsByMovieIdAsync(long movieId);
        }
}
