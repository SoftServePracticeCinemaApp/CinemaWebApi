using Cinema.Business.Entities;
using Cinema.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Data.Repositories;

public class MovieRepository : IMovieRepository
{
    private readonly CinemaDbContext _context;
    public MovieRepository(CinemaDbContext context)
    {
        _context = context;
    }

    public Task Add(MovieEntity movie)
    {
        throw new NotImplementedException();
    }

    public Task Delete(MovieEntity movie)
    {
        throw new NotImplementedException();
    }

    public Task<List<MovieEntity>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<MovieEntity> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<MovieEntity> GetByName(string name)
    {
        throw new NotImplementedException();
    }

    public Task Update(int Id, MovieEntity movie)
    {
        throw new NotImplementedException();
    }
}
