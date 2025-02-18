namespace Cinema.BlazorUI.Services.Interfaces
{
    public interface IAnalyticsService
    {
        Task<double> GetTotalRevenue(DateTime startDate, DateTime endDate);
        Task<List<(string MovieTitle, int TicketsSold)>> GetTopMovies(DateTime startDate, DateTime endDate);
        Task<List<(DateTime Date, double Revenue)>> GetRevenueByPeriod(DateTime startDate, DateTime endDate);
    }
}
