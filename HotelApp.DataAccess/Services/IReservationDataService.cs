using HotelApp.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.DataAccess.Services
{
    public interface IReservationDataService
    {
        Task DeleteReservation(Reservation reservation);
        Task EditReservation(Reservation reservation);
        Task SaveReservation(Reservation reservation);
    }
}
