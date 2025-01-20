using Cinema.Business.Entities;
using Cinema.Business.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Data.Repositories
{
    public class HallRepository : IHallRepository
    {
        private readonly CinemaDbContext _context;
        public HallRepository(CinemaDbContext context)
        {
            _context = context;
        }
        public async Task Add(HallEntity hall)
        {
            if (hall == null) throw new ArgumentNullException($"No data provided");
            await _context.Halls.AddAsync(hall);
        }

        public async Task Delete(int Id)
        {
            var hallInDb = await _context.Halls.AsNoTracking().FirstOrDefaultAsync(h => h.Id == Id);
            if (hallInDb == null) throw new InvalidOperationException($"Hall with Id {Id} does not exist");
        }

        public async Task<HallEntity> Get(int Id)
        {
            var hallInDb = await _context.Halls
                .AsNoTracking()
                .FirstOrDefaultAsync(h => h.Id == Id);

            if (hallInDb != null) return hallInDb;
            else throw new ArgumentException($"Hall with Id {Id} does not exist");
        }

        public async Task<IEnumerable<HallEntity>> GetAll() => 
            await _context.Halls
            .AsNoTracking()
            .ToListAsync();

        public async Task Update(int Id, HallEntity hall)
        {
            if (hall == null) throw new InvalidOperationException($"No data provided");

            var hallInDb = await _context.Halls
                .AsNoTracking()
                .FirstOrDefaultAsync(h => h.Id == Id);

            if (hallInDb == null) throw new ArgumentException($"Hall with Id {Id} does not exist");

            hallInDb.Seats = hall.Seats;
        }

        public async Task Save() => await _context.SaveChangesAsync();
    }
}
