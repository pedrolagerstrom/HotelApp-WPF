using HotelApp.DataAccess.Services;
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
    public class HotelViewModel : BindableBase, IHotelViewModel
    {
        public DelegateCommand GetHotelsCommand { get; set; }
        public DelegateCommand ShowHotelCommand { get; set; }
        public ObservableCollection<Hotel> Hotels { get; set; }
        private readonly IHotelDataService _hotelDataService;
        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManager;

        private Hotel _selectedHotel;
        public Hotel SelectedHotel
        {
            get { return _selectedHotel; }
            set { SetProperty(ref _selectedHotel, value); }
        }

        private string _view;
        public string View
        {
            get { return _view; }
            set { _view = value; }
        }

        public HotelViewModel(IHotelDataService hotelDataService, IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            Hotels = new ObservableCollection<Hotel>();
            GetHotelsCommand = new DelegateCommand(GetHotelsExecute, GetHotelsCanExecute);
            ShowHotelCommand = new DelegateCommand(ShowHotelExecute, ShowHotelCanExecute);
            _hotelDataService = hotelDataService;
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<UpdateHotelsEvent>().Subscribe(UpdateHotels);
            _regionManager = regionManager;
            _view = "HotelView";
            LoadHotels();
        }        

        private async void UpdateHotels()
        {
            await LoadHotels();
        }

        private bool ShowHotelCanExecute()
        {
            return true;
        }

        private void ShowHotelExecute()
        {
            if (SelectedHotel != null)
            {
                _regionManager.RequestNavigate("ContentRegion", "RoomView");
                _eventAggregator.GetEvent<HotelDetailsEvent>().Publish(SelectedHotel);
            }
            else
            {
                MessageBox.Show("Du måste välja ett hotell.");
            }
            
        }

        private bool GetHotelsCanExecute()
        {
            return true;
        }

        private async void GetHotelsExecute()
        {
            await LoadHotels();
        }
        private async Task LoadHotels()
        {
            var hotels = await _hotelDataService.GetHotels();
            Hotels.Clear();
            foreach (var hotel in hotels)
            {
                Hotels.Add(hotel);
                Debug.WriteLine(hotel.Name);
            }
        }

        Task IHotelViewModel.LoadHotels()
        {
            throw new NotImplementedException();
        }
    }
}
