using Cinema.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Business.Interfaces
{
    public interface IUserRepository
    {
        Task Add(UserEntity user);
        Task<UserEntity> Get(string id);
        Task<IEnumerable<UserEntity>> GetAll();
        Task Update(string id, UserEntity user);
        Task Delete(string id);
    }
}
