using HotelApp.DataAccess.Data;
using HotelApp.Model.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.DataAccess.Services
{
    public class UserDataService : IUserDataService
    {
        private readonly HotelAppDbContext _dbContext;

        public UserDataService(HotelAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _dbContext.Users.Include(u => u.Reservations).FirstOrDefaultAsync(u => u.Email == email);

            return user;
        }

        public async Task<List<User>> GetUsers()
        {
            return await _dbContext.Users.Include(u => u.Reservations).ToListAsync();
        }

        public async Task SaveUser(User user)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
        }
    }
}
