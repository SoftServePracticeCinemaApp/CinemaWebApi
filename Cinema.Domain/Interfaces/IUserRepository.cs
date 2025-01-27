
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
        Task AddAsync(UserEntity user);
        Task<UserEntity> GetAsync(Expression<Func<UserEntity, bool>>? filter = null);
        Task<IEnumerable<UserEntity>> GetAllAsync(Expression<Func<UserEntity, bool>>? filter = null);
        Task UpdateAsync(string id, UserEntity user);
        Task DeleteAsync(string id);
    }
}
