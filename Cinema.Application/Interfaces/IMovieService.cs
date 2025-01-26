using Cinema.Application.DTO.MovieDTOs;

namespace Cinema.Application.Interfaces
{
    interface IMovieService
    {
        Task<List<GetMovieDTO>> GetAllAsync();
        Task<GetMovieDTO> GetByIdAsync(int id);
        Task AddAsync(AddMovieDTO movieDto);
        Task UpdateAsync(UpdateMovieDTO movieDto);
        Task DeleteAsync(int id);
    }
}
