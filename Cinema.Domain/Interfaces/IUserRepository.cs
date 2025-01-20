using Cinema.Domain.Entities;

namespace Cinema.Domain.Interfaces
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
