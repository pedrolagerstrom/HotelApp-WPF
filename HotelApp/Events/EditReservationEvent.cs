using HotelApp.Model.Model;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.Events
{
    public class EditReservationEvent : PubSubEvent<Reservation>
    {
    }
}
