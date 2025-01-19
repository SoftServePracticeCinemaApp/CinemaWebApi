using Cinema.Business.Entities;
using Cinema.Business.Interfaces;
using Microsoft.EntityFrameworkCore;
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

    public async Task Add(MovieEntity movie)
    {
        if (movie == null) throw new ArgumentNullException(nameof(movie) + " No data provided");
        await _context.AddAsync(movie);
    }

    public async Task DeleteById(int Id)
    {
        var movieInDb = await _context.Movies
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == Id);

        if (movieInDb != null) await Task.Run(() => _context.Movies.Remove(movieInDb));
        else throw new InvalidOperationException($"session with id {Id} doesn't exist");
    }

    public async Task<List<MovieEntity>> GetAll() => await _context.Movies.ToListAsync();

    public async Task<MovieEntity> GetById(int Id)
    {
        var movieInDb = await _context.Movies
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == Id);

        if (movieInDb != null) return movieInDb;
        else throw new ArgumentException($"movie with {Id} doesn't exist");
    }

    public async Task Update(int Id, MovieEntity movie)
    {
        if (movie == null) throw new ArgumentException($"{nameof(movie)} can't be null");

        var movieInDb = await _context.Movies
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == Id);

        if (movieInDb != null)
        {
            movieInDb.SearchId = movie.SearchId;
            movieInDb.CinemaRating = movie.CinemaRating;
        }
    }

    public async Task Save() => await _context.SaveChangesAsync();
}
