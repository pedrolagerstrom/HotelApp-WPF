using HotelApp.Events;
using HotelApp.Model.Model;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HotelApp.ViewModels
{
    public class RoomViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManager;
        public ObservableCollection<Room> Rooms { get; set; }
        public DelegateCommand ShowRoomCommand { get; set; }

        public RoomViewModel(IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            Rooms = new ObservableCollection<Room>();
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;
            eventAggregator.GetEvent<HotelDetailsEvent>().Subscribe(HotelRecived);
            ShowRoomCommand = new DelegateCommand(Execute, CanExecute);
        }

        private bool CanExecute()
        {
            return true;
        }

        private void Execute()
        {
            if (SelectedRoom != null)
            {
                _eventAggregator.GetEvent<ReservationEvent>().Publish(SelectedRoom);
                _regionManager.RequestNavigate("ContentRegion", "ReservationView");
            }
            else
            {
                MessageBox.Show("Du måste välja ett rum.");
            }
            
        }

        private void HotelRecived(Hotel obj)
        {
            Hotel = obj;
            Rooms.Clear();

            foreach (var room in obj.Rooms)
            {
                Rooms.Add(room);
            }
        }

        private Hotel _hotel;
        public Hotel Hotel
        {
            get { return _hotel; }
            set { SetProperty(ref _hotel, value); }
        }

        private Room _selectedRoom;
        public Room SelectedRoom
        {
            get { return _selectedRoom; }
            set { SetProperty(ref _selectedRoom, value); }
        }
    }
}
