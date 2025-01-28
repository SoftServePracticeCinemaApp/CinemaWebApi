
using Cinema.Application.DTO.MovieDTOs;
using Cinema.Application.DTO.TicketDTOs;
using Cinema.Application.Helpers.Interfaces;
using Cinema.Domain.Entities;

namespace Cinema.Application.Interfaces
{
    public interface ITicketService
    {
        public interface ITicketService
        {
            Task<IBaseResponse<List<GetTicketDTO>>> GetAllTicketsAsync();
            Task<IBaseResponse<GetTicketDTO>> GetTicketByIdAsync(long id);
            Task<IBaseResponse<string>> AddTicketAsync(AddTicketDTO ticketDto);
            Task<IBaseResponse<string>> UpdateTicketAsync(long id, UpdateTicketDTO ticketDto);
            Task<IBaseResponse<string>> DeleteTicketAsync(long id);
            Task<IBaseResponse<List<GetTicketDTO>>> GetTicketsByMovieIdAsync(int movieId);
            Task<IBaseResponse<List<GetTicketDTO>>> GetTicketsByUserIdAsync(string userId);
            Task<IBaseResponse<List<GetTicketDTO>>> GetTicketsBySessionIdAsync(long sessionId);
            Task<IBaseResponse<List<GetTicketDTO>>> GetTicketsForHallAsync(int hallId);
        }
    }
}
