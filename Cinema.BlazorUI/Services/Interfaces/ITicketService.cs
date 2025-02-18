using Cinema.BlazorUI.Model;

namespace Cinema.BlazorUI.Services.Interfaces
{
    public interface ITicketService
    {
        Task<List<Ticket>> GetBySessionIdAsync(int sessionid);
        Task<FormResult> BookTicketAsync(int ticketid);
    }
}
