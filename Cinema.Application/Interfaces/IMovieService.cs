using Cinema.Application.DTO.MovieDTOs;
using Cinema.Application.Helpers.Interfaces;
using Cinema.Domain.Entities;

namespace Cinema.Application.Interfaces
{
    public interface IMovieService
    {
        Task<IBaseResponse<List<GetMovieDTO>>> GetAllMoviesAsync();
        Task<IBaseResponse<GetMovieDTO>> GetMovieByIdAsync(int id);
        Task<IBaseResponse<string>> AddMovieAsync(AddMovieDTO movieDto);
        Task<IBaseResponse<string>> UpdateMovieAsync(int id, UpdateMovieDTO movieDto);
        Task<IBaseResponse<string>> DeleteMovieAsync(int id);
        Task<IBaseResponse<List<GetMovieDTO>>> GetTopRatedMoviesAsync(int take);
        Task<IBaseResponse<string>> AddMovieFromTmdbAsync(int searchId);
    }
}
