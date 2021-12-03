using HotelApp.DataAccess.Data;
using HotelApp.Model.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.DataAccess.Services
{
    public class ReservationDataService : IReservationDataService
    {
        private readonly HotelAppDbContext _dbContext;

        public ReservationDataService(HotelAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SaveReservation(Reservation reservation)
        {
            _dbContext.Reservations.Add(reservation);
            await _dbContext.SaveChangesAsync();
        }

        public async Task EditReservation(Reservation reservation)
        {
            try
            {
                _dbContext.Entry(reservation).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        public async Task DeleteReservation(Reservation reservation)
        {
            var reservationToDelete = await _dbContext.Reservations.FirstAsync(r => r.ReservationId == reservation.ReservationId);
            _dbContext.Reservations.Remove(reservationToDelete);
            await _dbContext.SaveChangesAsync();
        }
    }
}
