using Cinema.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Business.Interfaces
{
    public interface IMovieRepository
    {
        Task Add(MovieEntity movie);
        Task Update(int Id, MovieEntity movie);
        Task Delete(MovieEntity movie);
        Task<MovieEntity> GetById(int id);
        Task<MovieEntity> GetByName(string name);
        Task<List<MovieEntity>> GetAll();
    }
}
