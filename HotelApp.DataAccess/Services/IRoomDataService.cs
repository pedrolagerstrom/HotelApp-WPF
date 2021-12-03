using HotelApp.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.DataAccess.Services
{
    public interface IRoomDataService
    {
        Task<List<Room>> GetRooms();
    }
}
