using Cinema.Application.Helpers.Interfaces;
using System.Threading.Tasks;

namespace Cinema.Application.Interfaces
{
    public interface IRatingService
    {
        Task<IBaseResponse<string>> AddRatingAsync(int movieId, string userId, int rating);
        Task<IBaseResponse<double>> GetAverageRatingAsync(int movieId);
        Task<IBaseResponse<int?>> GetUserRatingAsync(int movieId, string userId);
        Task<IBaseResponse<bool>> HasUserRatedAsync(int movieId, string userId);
    }
}
