using Cinema.Business.Interfaces;
using Cinema.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Cinema.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CinemaDbContext _context;
        public UserRepository(CinemaDbContext context)
        {
            _context = context;
        }
    
        public async Task AddAsync(UserEntity user)
        {
            if (user == null) throw new ArgumentNullException("No data provided");
            await _context.Users.AddAsync(user);        
        }

        public async Task DeleteAsync(string id)
        {
            var userInDb = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);

            if (userInDb == null) throw new InvalidOperationException($"User with Id {id} does not exist");

            await Task.Run(() => _context.Remove(userInDb));
        }

        public async Task<UserEntity> GetAsync(Expression<Func<UserEntity, bool>> filter)
        {
            var userInDb = await _context.Users.AsNoTracking().FirstOrDefaultAsync(filter);
            
            return userInDb;
        }

        public async Task<IEnumerable<UserEntity>> GetAllAsync(Expression<Func<UserEntity, bool>>? filter = null)
        {
            var query =_context.Users.AsNoTracking();
            if(filter != null)
            {
                query = query.Where(filter);
            }
			return await query.ToListAsync();
        }

        public async Task UpdateAsync(string id, UserEntity user)
        {
            if (user == null) throw new ArgumentNullException($"No data provided");

            var userInDb = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);

            if (userInDb == null) throw new ArgumentException($"User with Id {id} does not exist");
        }
    }
}
