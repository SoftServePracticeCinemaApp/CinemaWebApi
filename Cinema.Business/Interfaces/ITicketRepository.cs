using Cinema.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Business.Interfaces
{
    public interface ITicketRepository
    {
        Task Add(TicketEntity ticket);
        Task Update(long Id, TicketEntity ticketEntity);
        Task Remove(long Id);
        Task<TicketEntity> Get(long Id);
        Task<IEnumerable<TicketEntity>> GetByUserId(string Id);
        Task<IEnumerable<TicketEntity>> GetAll();
        Task<TicketEntity> GetById(long Id);
        Task<TicketEntity> GetByMovieId(int movieId);
        Task<TicketEntity> GetByMovieName(string movieName);
    }
}
