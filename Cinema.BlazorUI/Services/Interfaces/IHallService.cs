using Cinema.BlazorUI.Model;

namespace Cinema.BlazorUI.Services.Interfaces
{
    public interface IHallService
    {
        Task<List<FormattedHall>> GetHallsAsync();
    }
}
