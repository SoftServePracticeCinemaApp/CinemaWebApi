using Cinema.Domain.Entities;
using Cinema.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Infrastructure.Repositories
{
    public class RatingRepository : IRatingRepository
    {
        private readonly CinemaDbContext _context;

        public RatingRepository(CinemaDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(RatingEntity rating)
        {
            await _context.Ratings.AddAsync(rating);
        }

        public async Task<RatingEntity?> GetByUserAndMovieAsync(int movieId, string userId)
        {
            return await _context.Ratings.FirstOrDefaultAsync(r => r.MovieId == movieId && r.UserId == userId);
        }

        public async Task<List<RatingEntity>> GetByMovieIdAsync(int movieId)
        {
            return await _context.Ratings.Where(r => r.MovieId == movieId).ToListAsync();
        }
    }
}
