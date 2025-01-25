
using Cinema.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Business.Interfaces
{
    public interface IUserRepository
    {
        Task Add(UserEntity user);
        Task<UserEntity> Get(Expression<Func<UserEntity, bool>>? filter = null);
        Task<IEnumerable<UserEntity>> GetAll(Expression<Func<UserEntity, bool>>? filter = null);
        Task Update(string id, UserEntity user);
        Task Delete(string id);
    }
}
