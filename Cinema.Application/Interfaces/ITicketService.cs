
using Cinema.Application.DTO.MovieDTOs;
using Cinema.Application.DTO.TicketDTOs;

namespace Cinema.Application.Interfaces
{
    interface ITicketService
    {
        Task<IEnumerable<GetTicketDTO>> GetAllAsync();
        Task<GetTicketDTO> GetByIdAsync(int id);
        Task AddAsync(AddTicketDTO ticketDto);
        Task UpdateAsync(UpdateTicketDTO ticketDto);
        Task DeleteAsync(int id);
    }
}
