using Cinema.Domain.Entities;

namespace Cinema.Domain.Interfaces
{
    public interface IMovieRepository
    {
        Task AddAsync(MovieEntity movie);
        Task UpdateAsync(int Id, MovieEntity movie);
        Task DeleteByIdAsync(int Id);
        Task<MovieEntity> GetByIdAsync(int id);
        Task<List<MovieEntity>> GetAllAsync();
    }
}
