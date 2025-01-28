using Cinema.Application.Interfaces;
using Cinema.Application.DTO.SessionDTOs;
using AutoMapper;
using Cinema.Domain.Entities;
using Cinema.Application.DTO.TicketDTOs;
using Cinema.Application.Helpers.Interfaces;

namespace Cinema.Application.Services
{
    public class SessionService : ISessionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IResponses _responses;

        public SessionService(IResponses responses, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _responses = responses;
        }

        public async Task<IBaseResponse<List<GetSessionDTO>>> GetAllSessionsAsync()
        {
            try
            {
                var sessions = await _unitOfWork.Session.GetAllAsync();

                if (sessions == null || sessions.Count() == 0)
                    return _responses.CreateBaseBadRequest<List<GetSessionDTO>>("No sessions found.");

                var sessionsDto = _mapper.Map<List<GetSessionDTO>>(sessions);
                return _responses.CreateBaseOk(sessionsDto, sessionsDto.Count);
            }
            catch (Exception ex)
            {
                return _responses.CreateBaseServerError<List<GetSessionDTO>>(ex.Message);
            }
        }

        public async Task<IBaseResponse<GetSessionDTO>> GetSessionByIdAsync(long id)
        {
            try
            {
                var session = await _unitOfWork.Session.GetByIdAsync(id);

                if (session == null)
                    return _responses.CreateBaseNotFound<GetSessionDTO>($"Session with id {id} not found.");

                var sessionDto = _mapper.Map<GetSessionDTO>(session);
                return _responses.CreateBaseOk(sessionDto, 1);
            }
            catch (Exception ex)
            {
                return _responses.CreateBaseServerError<GetSessionDTO>(ex.Message);
            }
        }

        public async Task<IBaseResponse<string>> AddSessionAsync(AddSessionDTO sessionDto)
        {
            try
            {
                var session = _mapper.Map<SessionEntity>(sessionDto);

                await _unitOfWork.Session.AddAsync(session);
                await _unitOfWork.CompleteAsync();

                return _responses.CreateBaseOk("Session added successfully.", 1);
            }
            catch (Exception ex)
            {
                return _responses.CreateBaseServerError<string>(ex.Message);
            }
        }

        public async Task<IBaseResponse<string>> UpdateSessionAsync(long id, UpdateSessionDTO sessionDto)
        {
            try
            {
                var existingSession = await _unitOfWork.Session.GetByIdAsync(id);

                if (existingSession == null)
                    return _responses.CreateBaseNotFound<string>($"Session with id {id} not found.");

                _mapper.Map(sessionDto, existingSession);

                await _unitOfWork.CompleteAsync();

                return _responses.CreateBaseOk("Session updated successfully.", 1);
            }
            catch (Exception ex)
            {
                return _responses.CreateBaseServerError<string>(ex.Message);
            }
        }

        public async Task<IBaseResponse<string>> DeleteSessionAsync(long id)
        {
            try
            {
                await _unitOfWork.Session.DeleteByIdAsync(id);
                await _unitOfWork.CompleteAsync();

                return _responses.CreateBaseOk("Session deleted successfully.", 1);
            }
            catch (Exception ex)
            {
                return _responses.CreateBaseServerError<string>(ex.Message);
            }
        }

        public async Task<IBaseResponse<List<GetSessionDTO>>> GetSessionsByDateAsync(DateTime date)
        {
            try
            {
                var sessions = await _unitOfWork.Session.GetByDateAsync(date);

                if (sessions == null || sessions.Count() == 0)
                    return _responses.CreateBaseBadRequest<List<GetSessionDTO>>("No sessions found for the specified date.");

                var sessionsDto = _mapper.Map<List<GetSessionDTO>>(sessions);
                return _responses.CreateBaseOk(sessionsDto, sessionsDto.Count);
            }
            catch (Exception ex)
            {
                return _responses.CreateBaseServerError<List<GetSessionDTO>>(ex.Message);
            }
        }

        public async Task<IBaseResponse<List<GetSessionDTO>>> GetSessionsByMovieIdAsync(long movieId)
        {
            try
            {
                var sessions = await _unitOfWork.Session.GetByMovieIdAsync(movieId);

                if (sessions == null || sessions.Count() == 0)
                    return _responses.CreateBaseBadRequest<List<GetSessionDTO>>("No sessions found for the specified movie.");

                var sessionsDto = _mapper.Map<List<GetSessionDTO>>(sessions);
                return _responses.CreateBaseOk(sessionsDto, sessionsDto.Count);
            }
            catch (Exception ex)
            {
                return _responses.CreateBaseServerError<List<GetSessionDTO>>(ex.Message);
            }
        }
    }
}
