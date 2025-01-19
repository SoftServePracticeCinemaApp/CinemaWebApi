using Cinema.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Business.Interfaces
{
    public interface IHallRepository
    {
        Task Add(HallEntity hall);
        Task Update(int Id, HallEntity hall);
        Task Delete(int Id);
        Task<HallEntity> Get(int Id);
        Task<IEnumerable<HallEntity>> GetAll();
        Task Save();
    }
}
