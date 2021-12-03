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
    public class RoomDataService : IRoomDataService
    {
        private readonly HotelAppDbContext _dbContext;

        public RoomDataService(HotelAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Room>> GetRooms()
        {
            using (var context = _dbContext)
            {
                return await context.Rooms.ToListAsync();
            }
        }
    }
}
