using Cinema.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Domain.Interfaces
{
    public interface IRatingRepository
    {
        Task AddAsync(RatingEntity rating);
        Task<RatingEntity?> GetByUserAndMovieAsync(int movieId, string userId);
        Task<List<RatingEntity>> GetByMovieIdAsync(int movieId);
    }
}
