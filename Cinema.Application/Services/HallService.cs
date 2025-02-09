using Cinema.Application.DTO.HallDTOs;
using Cinema.Application.Helpers.Interfaces;
using Cinema.Application.Interfaces;
using Cinema.Domain.Entities;
using AutoMapper;

namespace Cinema.Application.Services
{
    public class HallService : IHallService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IResponses _responses;

        public HallService(IResponses responses, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _responses = responses;
        }

        public async Task<IBaseResponse<GetHallDTO>> GetHallByIdAsync(int id)
        {
            try
            {
                var hall = await _unitOfWork.Hall.GetAsync(id);

                if (hall == null)
                    return _responses.CreateBaseNotFound<GetHallDTO>($"Hall with id {id} not found.");

                var hallDto = _mapper.Map<GetHallDTO>(hall);
                return _responses.CreateBaseOk(hallDto, 1);
            }
            catch (Exception ex)
            {
                return _responses.CreateBaseServerError<GetHallDTO>(ex.Message);
            }
        }

        public async Task<IBaseResponse<string>> UpdateHallSeatsAsync(int id, UpdateHallDTO hallDto)
        {
            try
            {
                var existingHall = await _unitOfWork.Hall.GetAsync(id);

                if (existingHall == null)
                    return _responses.CreateBaseNotFound<string>($"Hall with id {id} not found.");

                existingHall.Seats = hallDto.Seats;

                await _unitOfWork.CompleteAsync();

                return _responses.CreateBaseOk("Hall seats updated successfully.", 1);
            }
            catch (Exception ex)
            {
                return _responses.CreateBaseServerError<string>(ex.Message);
            }
        }
    }
}
