using Cinema.Application.DTO.AnalyticsDTO;
using Cinema.Application.Helpers.Interfaces;

namespace Cinema.Application.Interfaces
{
    public interface IAnalyticsService
    {
        Task<IBaseResponse<double>> GetTotalRevenueAsync(DateTime start, DateTime end);
        Task<IBaseResponse<List<MovieSalesDTO>>> GetTopMoviesAsync(DateTime start, DateTime end);
        Task<IBaseResponse<List<DailyRevenueDTO>>> GetRevenueByPeriodAsync(DateTime start, DateTime end);
    }
}