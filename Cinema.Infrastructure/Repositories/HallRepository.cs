using Cinema.Domain.Entities;
using Cinema.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Infrastructure.Repositories
{
    public class HallRepository : IHallRepository
    {
        private readonly CinemaDbContext _context;
        public HallRepository(CinemaDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(HallEntity hall)
        {
            if (hall == null) throw new ArgumentNullException($"No data provided");
            await _context.Halls.AddAsync(hall);
        }

        public async Task DeleteAsync(int Id)
        {
            var hallInDb = await _context.Halls.AsNoTracking().FirstOrDefaultAsync(h => h.Id == Id);
            if (hallInDb == null) throw new InvalidOperationException($"Hall with Id {Id} does not exist");
        }

        public async Task<HallEntity> GetAsync(int Id)
        {
            var hallInDb = await _context.Halls
                .AsNoTracking()
                .FirstOrDefaultAsync(h => h.Id == Id);

            if (hallInDb != null) return hallInDb;
            else throw new ArgumentException($"Hall with Id {Id} does not exist");
        }

        public async Task<IEnumerable<HallEntity>> GetAllAsync() => 
            await _context.Halls
            .AsNoTracking()
            .ToListAsync();

        public async Task UpdateAsync(int Id, HallEntity hall)
        {
            if (hall == null) throw new InvalidOperationException($"No data provided");

            var hallInDb = await _context.Halls
                .AsNoTracking()
                .FirstOrDefaultAsync(h => h.Id == Id);

            if (hallInDb == null) throw new ArgumentException($"Hall with Id {Id} does not exist");

            hallInDb.Seats = hall.Seats;
        }
    }
}
