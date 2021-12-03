using HotelApp.DataAccess.Services;
using HotelApp.Events;
using HotelApp.Model.Model;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HotelApp.ViewModels
{
    public class ReservationViewModel : BindableBase
    {
        private readonly IUserDataService _userDataService;
        private readonly IReservationDataService _reservationDataService;
        private readonly IEventAggregator _eventAggrigator;
        private readonly IRegionManager _regionManager;
        public DelegateCommand ReservationCommand { get; set; }
        public bool IsLoggedIn { get; set; }

        public ReservationViewModel(IUserDataService userDataService, IReservationDataService reservationDataService, IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            ReservationCommand = new DelegateCommand(ReservationExecute, ReservationCanExecute);
            eventAggregator.GetEvent<ReservationEvent>().Subscribe(RoomRecived);
            eventAggregator.GetEvent<LoginEvent>().Subscribe(UserRecived);
            IsLoggedIn = false;
            _userDataService = userDataService;
            _reservationDataService = reservationDataService;
            _eventAggrigator = eventAggregator;
            _regionManager = regionManager;
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
        }

        private void UserRecived(User obj)
        {
            if (obj != null)
            {
                IsLoggedIn = true;
            }
            else if (obj == null)
            {
                IsLoggedIn = false;
            }

            User = obj;
        }

        private void RoomRecived(Room obj)
        {
            Room = obj;
        }

        private bool ReservationCanExecute()
        {
            return true;
        }

        private async void ReservationExecute()
        {
            await AddReservation();
        }

        private bool _oneWeek;
        public bool OneWeek
        {
            get { return _oneWeek; }
            set
            {
                SetProperty(ref _oneWeek, value);
                if (OneWeek)
                {
                    TwoWeeks = false;
                    TotalPrice = Room.Price * 7;
                }
            }
        }

        private bool _twoWeeks;
        public bool TwoWeeks
        {
            get { return _twoWeeks; }
            set
            {
                SetProperty(ref _twoWeeks, value);
                if (TwoWeeks)
                {
                    OneWeek = false;
                    TotalPrice = Room.Price * 14;
                }
            }
        }

        private DateTime _startDate;
        public DateTime StartDate
        {
            get { return _startDate; }
            set { SetProperty(ref _startDate, value); }
        }

        private DateTime _endDate;
        public DateTime EndDate
        {
            get { return _endDate; }
            set { SetProperty(ref _endDate, value); }
        }

        private int _totalPrice;
        public int TotalPrice
        {
            get { return _totalPrice; }
            set { SetProperty(ref _totalPrice, value); }
        }

        private Room _room;
        public Room Room
        {
            get { return _room; }
            set { SetProperty(ref _room, value); }
        }

        private User _user;
        public User User
        {
            get { return _user; }
            set { SetProperty(ref _user, value); }
        }

        public async Task AddReservation()
        {
            if (IsLoggedIn)
            {
                if (OneWeek || TwoWeeks)
                {
                    Reservation reservation = new Reservation
                    {
                        UserId = User.UserId,
                        RoomId = Room.RoomId,
                        Weeks = OneWeek ? 1 : 2,
                        StartDate = StartDate,
                        EndDate = OneWeek ? StartDate.AddDays(7) : StartDate.AddDays(14),
                        TotalPrice = TotalPrice
                    };

                    await _reservationDataService.SaveReservation(reservation);
                    var user = await _userDataService.GetUserByEmail(User.Email);
                    User = user;
                    _eventAggrigator.GetEvent<UpdateHotelsEvent>().Publish();
                    _eventAggrigator.GetEvent<LoginEvent>().Publish(User);

                    var i = new NavigationParameters();
                    MessageBox.Show("Din reservation är genomförd.");
                    _regionManager.RequestNavigate("ContentRegion", "HotelView", i);                    
                }
                else
                {
                    MessageBox.Show("Välj mellan 1 vecka eller 2 veckor.");
                }
            }
            else
            {
                MessageBox.Show("Du måste vara inloggad för att kunna göra en reservation.");
            }
        }

    }
}
