﻿using Cinema.Domain.Entities;
using Cinema.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CinemaDbContext _context;
        public UserRepository(CinemaDbContext context)
        {
            _context = context;
        }
    
        public async Task Add(UserEntity user)
        {
            if (user == null) throw new ArgumentNullException("No data provided");
            await _context.Users.AddAsync(user);        
        }

        public async Task Delete(string id)
        {
            var userInDb = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);

            if (userInDb == null) throw new InvalidOperationException($"User with Id {id} does not exist");

            await Task.Run(() => _context.Remove(userInDb));
        }

        public async Task<UserEntity> Get(Expression<Func<UserEntity, bool>> filter)
        {
            var userInDb = await _context.Users.AsNoTracking().FirstOrDefaultAsync(filter);
            
            return userInDb;
        }

        public async Task<IEnumerable<UserEntity>> GetAll(Expression<Func<UserEntity, bool>>? filter = null)
        {
            var query =_context.Users.AsNoTracking();
            if(filter != null)
            {
                query = query.Where(filter);
            }
			return await query.ToListAsync();
        }

        public async Task Update(string id, UserEntity user)
        {
            if (user == null) throw new ArgumentNullException($"No data provided");

            var userInDb = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);

            if (userInDb == null) throw new ArgumentException($"User with Id {id} does not exist");
        }

        public async Task Save() => await _context.SaveChangesAsync();
    }
}
