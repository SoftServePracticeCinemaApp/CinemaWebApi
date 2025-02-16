using Cinema.Application.Enums;
using Cinema.Application.Helpers.Interfaces;
using Cinema.Application.Interfaces;
using Cinema.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Application.Services
{
    public class RatingService : IRatingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IResponses _responses;

        public RatingService(IUnitOfWork unitOfWork, IResponses responses)
        {
            _unitOfWork = unitOfWork;
            _responses = responses;
        }

        public async Task<IBaseResponse<string>> AddRatingAsync(int movieId, string userId, int rating)
        {
            var existingRating = await _unitOfWork.Rating.GetByUserAndMovieAsync(movieId, userId);

            if (existingRating != null)
            {
                existingRating.Rating = rating;
            }
            else
            {
                var newRating = new RatingEntity { MovieId = movieId, UserId = userId, Rating = rating };
                await _unitOfWork.Rating.AddAsync(newRating);
            }

            await _unitOfWork.CompleteAsync(); 

            var ratingResponse = await GetAverageRatingAsync(movieId);
            if (ratingResponse.StatusCode == StatusCode.Ok)
            {
                var movie = await _unitOfWork.Movie.GetByIdAsync(movieId);
                if (movie != null)
                {
                    movie.CinemaRating = ratingResponse.Data; 

                    await _unitOfWork.Movie.UpdateAsync(movie.Id, movie); 
                }
            }

            return _responses.CreateBaseOk("Rating saved successfully.", 1);
        }

        public async Task<IBaseResponse<double>> GetAverageRatingAsync(int movieId)
        {
            var ratings = await _unitOfWork.Rating.GetByMovieIdAsync(movieId);
            if (!ratings.Any()) return _responses.CreateBaseOk(0.0, 0);

            var averageRating = ratings.Average(r => r.Rating);
            return _responses.CreateBaseOk(averageRating, ratings.Count());
        }

        public async Task<IBaseResponse<int?>> GetUserRatingAsync(int movieId, string userId)
        {
            var rating = await _unitOfWork.Rating.GetByUserAndMovieAsync(movieId, userId);
            return _responses.CreateBaseOk(rating?.Rating, rating != null ? 1 : 0);
        }

        public async Task<IBaseResponse<bool>> HasUserRatedAsync(int movieId, string userId)
        {
            var rating = await _unitOfWork.Rating.GetByUserAndMovieAsync(movieId, userId);
            return _responses.CreateBaseOk(rating != null, 1);
        }
    }
}
