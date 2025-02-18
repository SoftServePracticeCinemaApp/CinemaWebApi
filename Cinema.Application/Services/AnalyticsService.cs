using Cinema.Application.DTO.AnalyticsDTO;
using Cinema.Application.Interfaces;
using Cinema.Domain.Interfaces;
using Cinema.Application.Helpers.Interfaces;
using Microsoft.EntityFrameworkCore;
using AutoMapper;


namespace Cinema.Application.Services
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IResponses _responses;

        public AnalyticsService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IResponses responses)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _responses = responses;
        }

        public async Task<IBaseResponse<double>> GetTotalRevenueAsync(DateTime start, DateTime end)
        {
            try
            {
                var sessions = await _unitOfWork.Session.GetAllAsync();
                var tickets = await _unitOfWork.Ticket.GetAllAsync();

                var totalRevenue = sessions
                    .Where(s => s.Date >= start && s.Date <= end)
                    .Join(tickets.Where(t => t.isBooked),
                        session => session.Id,
                        ticket => ticket.SessionId,
                        (session, ticket) => session.TicketPrice)
                    .Sum();

                return _responses.CreateBaseOk(totalRevenue, 1);
            }
            catch (Exception ex)
            {
                return _responses.CreateBaseServerError<double>(ex.Message);
            }
        }

        public async Task<IBaseResponse<List<MovieSalesDTO>>> GetTopMoviesAsync(DateTime start, DateTime end)
        {
            try
            {
                var movies = await _unitOfWork.Movie.GetAllAsync();
                var sessions = await _unitOfWork.Session.GetAllAsync();
                var tickets = await _unitOfWork.Ticket.GetAllAsync();

                var topMovies = movies
                    .Join(sessions.Where(s => s.Date >= start && s.Date <= end),
                        movie => movie.Id,
                        session => session.MovieId,
                        (movie, session) => new { Movie = movie, Session = session })
                    .Join(tickets.Where(t => t.isBooked),
                        ms => ms.Session.Id,
                        ticket => ticket.SessionId,
                        (ms, ticket) => new { ms.Movie, ms.Session, Ticket = ticket })
                    .GroupBy(mst => new { mst.Movie.Id, mst.Movie.Title })
                    .Select(g => new MovieSalesDTO
                    {
                        MovieTitle = g.Key.Title,
                        TicketsSold = g.Count(),
                        Revenue = g.Sum(mst => mst.Session.TicketPrice)
                    })
                    .OrderByDescending(m => m.Revenue)
                    .Take(10)
                    .ToList();

                return _responses.CreateBaseOk(topMovies, topMovies.Count);
            }
            catch (Exception ex)
            {
                return _responses.CreateBaseServerError<List<MovieSalesDTO>>(ex.Message);
            }
        }

        public async Task<IBaseResponse<List<DailyRevenueDTO>>> GetRevenueByPeriodAsync(DateTime start, DateTime end)
        {
            try
            {
                var sessions = await _unitOfWork.Session.GetAllAsync();
                var tickets = await _unitOfWork.Ticket.GetAllAsync();

                var dailyRevenue = sessions
                    .Where(s => s.Date.Date >= start.Date && s.Date.Date <= end.Date)
                    .Join(tickets.Where(t => t.isBooked),
                        session => session.Id,
                        ticket => ticket.SessionId,
                        (session, ticket) => new { Session = session, Ticket = ticket })
                    .GroupBy(st => st.Session.Date.Date)
                    .Select(g => new DailyRevenueDTO
                    {
                        Date = g.Key,
                        Revenue = g.Sum(st => st.Session.TicketPrice),
                        TicketsSold = g.Count()
                    })
                    .OrderBy(d => d.Date)
                    .ToList();

                return _responses.CreateBaseOk(dailyRevenue, dailyRevenue.Count);
            }
            catch (Exception ex)
            {
                return _responses.CreateBaseServerError<List<DailyRevenueDTO>>(ex.Message);
            }
        }

        
        public async Task<IBaseResponse<Dictionary<string, double>>> GetAverageTicketPriceByMovieAsync(DateTime start, DateTime end)
        {
            try
            {
                var sessions = await _unitOfWork.Session.GetAllAsync();
                var movies = await _unitOfWork.Movie.GetAllAsync();

                var avgPrices = movies
                    .Join(sessions.Where(s => s.Date >= start && s.Date <= end),
                        movie => movie.Id,
                        session => session.MovieId,
                        (movie, session) => new { Movie = movie, Session = session })
                    .GroupBy(ms => ms.Movie.Title)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Average(ms => ms.Session.TicketPrice)
                    );

                return _responses.CreateBaseOk(avgPrices, avgPrices.Count);
            }
            catch (Exception ex)
            {
                return _responses.CreateBaseServerError<Dictionary<string, double>>(ex.Message);
            }
        }

        public async Task<IBaseResponse<Dictionary<DayOfWeek, (int TicketsSold, double Revenue)>>>
            GetSalesByDayOfWeekAsync(DateTime start, DateTime end)
        {
            try
            {
                var sessions = await _unitOfWork.Session.GetAllAsync();
                var tickets = await _unitOfWork.Ticket.GetAllAsync();

                var salesByDay = sessions
                    .Where(s => s.Date >= start && s.Date <= end)
                    .Join(tickets.Where(t => t.isBooked),
                        session => session.Id,
                        ticket => ticket.SessionId,
                        (session, ticket) => new { Session = session, Ticket = ticket })
                    .GroupBy(st => st.Session.Date.DayOfWeek)
                    .ToDictionary(
                        g => g.Key,
                        g => (g.Count(), g.Sum(st => st.Session.TicketPrice))
                    );

                return _responses.CreateBaseOk(salesByDay, salesByDay.Count);
            }
            catch (Exception ex)
            {
                return _responses.CreateBaseServerError<Dictionary<DayOfWeek, (int TicketsSold, double Revenue)>>(ex.Message);
            }
        }
    }
}