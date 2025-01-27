using Cinema.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Domain.Interfaces
{
    public interface IHallRepository
    {
        Task AddAsync(HallEntity hall);
        Task UpdateAsync(int Id, HallEntity hall);
        Task DeleteAsync(int Id);
        Task<HallEntity> GetAsync(int Id);
        Task<IEnumerable<HallEntity>> GetAllAsync();
    }
}
