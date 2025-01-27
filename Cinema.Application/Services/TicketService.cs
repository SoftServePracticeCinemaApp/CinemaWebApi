using AutoMapper;
using Cinema.Application.DTO.TicketDTOs;
using Cinema.Application.Interfaces;
using Cinema.Domain.Entities;

namespace Cinema.Application.Services
{
    public class TicketService : ITicketService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public TicketService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(AddTicketDTO ticketDto)
        {
            var ticket = _mapper.Map<TicketEntity>(ticketDto);
            await _unitOfWork.Ticket.AddAsync(ticket);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteAsync(long id)
        {
            await _unitOfWork.Ticket.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<GetTicketDTO>> GetAllAsync()
        {
            var tickets = await _unitOfWork.Ticket.GetAllAsync();
            return _mapper.Map<IEnumerable<GetTicketDTO>>(tickets);
        }

        public async Task<GetTicketDTO> GetByIdAsync(long id)
        {
            var ticket = await _unitOfWork.Ticket.GetByIdAsync(id);
            return _mapper.Map<GetTicketDTO>(ticket);
        }

        public async Task<IEnumerable<GetTicketDTO>> GetByMovieIdAsync(int movieId)
        {
            var tickets = await _unitOfWork.Ticket.GetByMovieIdAsync(movieId);
            return _mapper.Map<IEnumerable<GetTicketDTO>>(tickets);
        }

        public async Task<IEnumerable<GetTicketDTO>> GetByUserIdAsync(string Id)
        {
            var tickets = await _unitOfWork.Ticket.GetByUserIdAsync(Id);
            return _mapper.Map<IEnumerable<GetTicketDTO>>(tickets);
        }

        public async Task UpdateAsync(long Id, UpdateTicketDTO ticketDto)
        {
            var ticket = await _unitOfWork.Ticket.GetByIdAsync(ticketDto.Id);
            _mapper.Map(ticketDto, ticket);
            await _unitOfWork.CompleteAsync();
        }
    }
}
