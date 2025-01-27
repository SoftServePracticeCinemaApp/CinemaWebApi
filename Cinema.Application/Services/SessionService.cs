using Cinema.Application.Interfaces;
using Cinema.Application.DTO.SessionDTOs;
using AutoMapper;
using Cinema.Domain.Entities;
using Cinema.Application.DTO.TicketDTOs;

namespace Cinema.Application.Services
{
    class SessionService : ISessionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public SessionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(AddSessionDTO sessionDto)
        {
            var session = _mapper.Map<SessionEntity>(sessionDto);
            await _unitOfWork.Session.AddAsync(session);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _unitOfWork.Session.DeleteByIdAsync(id);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<GetSessionDTO>> GetAllAsync()
        {
            var sessions = await _unitOfWork.Session.GetAllAsync();
            return _mapper.Map<IEnumerable<GetSessionDTO>>(sessions);
        }

        public async Task<GetSessionDTO> GetByIdAsync(int id)
        {
            var session = await _unitOfWork.Session.GetByIdAsync(id);
            return _mapper.Map<GetSessionDTO>(session);
        }

        public async Task UpdateAsync(UpdateSessionDTO sessionDto)
        {
            var session = await _unitOfWork.Session.GetByIdAsync(sessionDto.Id);
            _mapper.Map(sessionDto, session);
            await _unitOfWork.CompleteAsync();
        }
        public async Task<IEnumerable<GetSessionDTO>> GetByDateAsync(DateTime dateTime)
        {
            var sessions = await _unitOfWork.Session.GetByDateAsync(dateTime);
            return _mapper.Map<IEnumerable<GetSessionDTO>>(sessions);
        }
    }
}
