﻿using HotelApp.DataAccess.Data;
using HotelApp.Model.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.DataAccess.Services
{
    public class HotelDataService : IHotelDataService
    {
        private readonly HotelAppDbContext _dbContext;

        public HotelDataService(HotelAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Hotel>> GetHotels()
        {
            return await _dbContext.Hotels.Include(h => h.Rooms).ThenInclude(r => r.Reservations).ToListAsync();
        }
    }
}
