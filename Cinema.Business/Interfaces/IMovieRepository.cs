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
        Task DeleteById(int Id);
        Task<MovieEntity> GetById(int id);
        Task<List<MovieEntity>> GetAll();
        Task Save();
    }
}
