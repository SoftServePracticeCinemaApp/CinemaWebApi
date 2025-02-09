using Cinema.Application.DTO.HallDTOs;
using Cinema.Application.Helpers.Interfaces;
using Cinema.Domain.Entities;

namespace Cinema.Application.Interfaces
{
    public interface IHallService
    {
        Task<IBaseResponse<GetHallDto>> GetHallByIdAsync(int id);
        Task<IBaseResponse<string>> UpdateHallSeatsAsync(int id, UpdateHallDTO hallDto);
    }
}

