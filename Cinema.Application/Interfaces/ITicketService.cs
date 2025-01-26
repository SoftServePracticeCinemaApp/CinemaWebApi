
using Cinema.Application.DTO.MovieDTOs;
using Cinema.Application.DTO.TicketDTOs;
using Cinema.Domain.Entities;

namespace Cinema.Application.Interfaces
{
    public interface ITicketService
    {
        Task<IEnumerable<GetTicketDTO>> GetAllAsync();
        Task<GetTicketDTO> GetByIdAsync(long id);
        Task AddAsync(AddTicketDTO ticketDto);
        Task DeleteAsync(long id);
        Task UpdateAsync(long Id, UpdateTicketDTO ticketDto);
        Task<IEnumerable<GetTicketDTO>> GetByUserIdAsync(string Id);
        Task<IEnumerable<GetTicketDTO>> GetByMovieIdAsync(int movieId);
    }
}
