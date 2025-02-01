using System;
using Cinema.Domain.Entities;
using Cinema.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Infrastructure.Repositories;

public class MovieRepository : IMovieRepository
{
    private readonly CinemaDbContext _context;
    public MovieRepository(CinemaDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(MovieEntity movie)
    {
        if (movie == null) throw new ArgumentNullException(nameof(movie) + " No data provided");
        await _context.AddAsync(movie);
    }

    public async Task DeleteByIdAsync(int Id)
    {
        var movieInDb = await _context.Movies
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == Id);

        if (movieInDb != null) await Task.Run(() => _context.Movies.Remove(movieInDb));
        else throw new InvalidOperationException($"session with id {Id} doesn't exist");
    }

    public async Task<List<MovieEntity>> GetAllAsync() => await _context.Movies.ToListAsync();

    public async Task<MovieEntity> GetByIdAsync(int Id)
    {
        var movieInDb = await _context.Movies
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == Id);

        if (movieInDb != null) return movieInDb;
        else throw new ArgumentException($"movie with {Id} doesn't exist");
    }

    public async Task UpdateAsync(int Id, MovieEntity movie)
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

    public async Task<List<MovieEntity>> GetTopRatedAsync(int take)
    {
        return await _context.Movies
            .OrderByDescending(m => m.CinemaRating)
            .Take(take)
            .ToListAsync();
    }
    public async Task<MovieEntity> GetBySearchIdAsync(int searchId)
    {
        var movie = await _context.Movies
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.SearchId == searchId);

        if (movie != null) return movie;
        else throw new ArgumentException($"movie with {searchId} doesn't exist");
    }
}
