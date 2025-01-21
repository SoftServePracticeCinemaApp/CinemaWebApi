using Cinema.Domain.Entities;

namespace Cinema.Domain.Interfaces
{
    public interface IMovieRepository
    {
        Task Add(MovieEntity movie);
        Task Update(int Id, MovieEntity movie);
        Task DeleteById(int Id);
        Task<MovieEntity> GetById(int id);
        Task<List<MovieEntity>> GetAll();
        Task Save();
    }
}
