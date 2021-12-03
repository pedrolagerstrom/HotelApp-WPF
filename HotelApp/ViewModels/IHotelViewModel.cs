using HotelApp.Model.Model;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.ViewModels
{
    public interface IHotelViewModel
    {
        DelegateCommand GetHotelsCommand { get; set; }
        DelegateCommand ShowHotelCommand { get; set; }
        ObservableCollection<Hotel> Hotels { get; set; }
        Hotel SelectedHotel { get; set; }
        string View { get; set; }
        Task LoadHotels();
    }
}
