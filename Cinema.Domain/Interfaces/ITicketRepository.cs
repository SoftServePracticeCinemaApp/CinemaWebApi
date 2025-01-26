﻿using Cinema.Domain.Entities;

namespace Cinema.Domain.Interfaces
{
    public interface ITicketRepository
    {
        Task AddAsync(TicketEntity ticket);
        Task UpdateAsync(long Id, TicketEntity ticketEntity);
        Task RemoveAsync(long Id);
        Task<IEnumerable<TicketEntity>> GetByUserIdAsync(string Id);
        Task<IEnumerable<TicketEntity>> GetAllAsync();
        Task<TicketEntity> GetByIdAsync(long Id);
        Task<IEnumerable<TicketEntity>> GetByMovieIdAsync(int movieId);
    }
}
