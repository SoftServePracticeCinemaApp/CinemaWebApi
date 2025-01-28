using AutoMapper;
using Cinema.Application.DTO.TicketDTOs;
using Cinema.Application.Helpers.Interfaces;
using Cinema.Application.Interfaces;
using Cinema.Domain.Entities;

namespace Cinema.Application.Services
{
    public class TicketService : ITicketService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IResponses _responses;

        public TicketService(IResponses responses, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _responses = responses;
        }

        public async Task<IBaseResponse<List<GetTicketDTO>>> GetAllTicketsAsync()
        {
            try
            {
                var tickets = await _unitOfWork.Ticket.GetAllAsync();

                if (tickets == null || tickets.Count() == 0)
                    return _responses.CreateBaseBadRequest<List<GetTicketDTO>>("No tickets found.");

                var ticketsDto = _mapper.Map<List<GetTicketDTO>>(tickets);
                return _responses.CreateBaseOk(ticketsDto, ticketsDto.Count);
            }
            catch (Exception ex)
            {
                return _responses.CreateBaseServerError<List<GetTicketDTO>>(ex.Message);
            }
        }

        public async Task<IBaseResponse<GetTicketDTO>> GetTicketByIdAsync(long id)
        {
            try
            {
                var ticket = await _unitOfWork.Ticket.GetByIdAsync(id);

                if (ticket == null)
                    return _responses.CreateBaseNotFound<GetTicketDTO>($"Ticket with id {id} not found.");

                var ticketDto = _mapper.Map<GetTicketDTO>(ticket);
                return _responses.CreateBaseOk(ticketDto, 1);
            }
            catch (Exception ex)
            {
                return _responses.CreateBaseServerError<GetTicketDTO>(ex.Message);
            }
        }

        public async Task<IBaseResponse<string>> AddTicketAsync(AddTicketDTO ticketDto)
        {
            try
            {
                var ticket = _mapper.Map<TicketEntity>(ticketDto);

                await _unitOfWork.Ticket.AddAsync(ticket);
                await _unitOfWork.CompleteAsync();

                return _responses.CreateBaseOk("Ticket added successfully.", 1);
            }
            catch (Exception ex)
            {
                return _responses.CreateBaseServerError<string>(ex.Message);
            }
        }

        public async Task<IBaseResponse<string>> UpdateTicketAsync(long id, UpdateTicketDTO ticketDto)
        {
            try
            {
                var existingTicket = await _unitOfWork.Ticket.GetByIdAsync(id);

                if (existingTicket == null)
                    return _responses.CreateBaseNotFound<string>($"Ticket with id {id} not found.");

                _mapper.Map(ticketDto, existingTicket);

                await _unitOfWork.CompleteAsync();

                return _responses.CreateBaseOk("Ticket updated successfully.", 1);
            }
            catch (Exception ex)
            {
                return _responses.CreateBaseServerError<string>(ex.Message);
            }
        }

        public async Task<IBaseResponse<string>> DeleteTicketAsync(long id)
        {
            try
            {
                await _unitOfWork.Ticket.DeleteAsync(id);
                await _unitOfWork.CompleteAsync();

                return _responses.CreateBaseOk("Ticket deleted successfully.", 1);
            }
            catch (Exception ex)
            {
                return _responses.CreateBaseServerError<string>(ex.Message);
            }
        }

        public async Task<IBaseResponse<List<GetTicketDTO>>> GetTicketsByMovieIdAsync(int movieId)
        {
            try
            {
                var tickets = await _unitOfWork.Ticket.GetByMovieIdAsync(movieId);

                if (tickets == null || tickets.Count() == 0)
                    return _responses.CreateBaseBadRequest<List<GetTicketDTO>>("No tickets found for the specified movie.");

                var ticketsDto = _mapper.Map<List<GetTicketDTO>>(tickets);
                return _responses.CreateBaseOk(ticketsDto, ticketsDto.Count);
            }
            catch (Exception ex)
            {
                return _responses.CreateBaseServerError<List<GetTicketDTO>>(ex.Message);
            }
        }

        public async Task<IBaseResponse<List<GetTicketDTO>>> GetTicketsByUserIdAsync(string userId)
        {
            try
            {
                var tickets = await _unitOfWork.Ticket.GetByUserIdAsync(userId);

                if (tickets == null || tickets.Count() == 0)
                    return _responses.CreateBaseBadRequest<List<GetTicketDTO>>("No tickets found for the specified user.");

                var ticketsDto = _mapper.Map<List<GetTicketDTO>>(tickets);
                return _responses.CreateBaseOk(ticketsDto, ticketsDto.Count);
            }
            catch (Exception ex)
            {
                return _responses.CreateBaseServerError<List<GetTicketDTO>>(ex.Message);
            }
        }

        public async Task<IBaseResponse<List<GetTicketDTO>>> GetTicketsBySessionIdAsync(long sessionId)
        {
            try
            {
                var tickets = await _unitOfWork.Ticket.GetBySessionIdAsync(sessionId);

                if (tickets == null || tickets.Count() == 0)
                    return _responses.CreateBaseBadRequest<List<GetTicketDTO>>("No tickets found for the specified session.");

                var ticketsDto = _mapper.Map<List<GetTicketDTO>>(tickets);
                return _responses.CreateBaseOk(ticketsDto, ticketsDto.Count);
            }
            catch (Exception ex)
            {
                return _responses.CreateBaseServerError<List<GetTicketDTO>>(ex.Message);
            }
        }

        public async Task<IBaseResponse<List<GetTicketDTO>>> GetTicketsForHallAsync(int hallId)
        {
            try
            {
                var tickets = await _unitOfWork.Ticket.GetTicketsForHallAsync(hallId);

                if (tickets == null || tickets.Count() == 0)
                    return _responses.CreateBaseBadRequest<List<GetTicketDTO>>("No tickets found for the specified hall.");

                var ticketsDto = _mapper.Map<List<GetTicketDTO>>(tickets);
                return _responses.CreateBaseOk(ticketsDto, ticketsDto.Count);
            }
            catch (Exception ex)
            {
                return _responses.CreateBaseServerError<List<GetTicketDTO>>(ex.Message);
            }
        }
    }
}
