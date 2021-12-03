using HotelApp.DataAccess.Services;
using HotelApp.Events;
using HotelApp.Model.Model;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HotelApp.ViewModels
{
    public class EditReservationViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IReservationDataService _reservationDataService;
        private readonly IRegionManager _regionManager;
        public DelegateCommand EditCommand { get; set; }
        public DelegateCommand DeleteCommand { get; set; }

        public EditReservationViewModel(IEventAggregator eventAggregator, IReservationDataService reservationDataService, IRegionManager regionManager)
        {
            eventAggregator.GetEvent<EditReservationEvent>().Subscribe(ReservationRecived);
            eventAggregator.GetEvent<LoginEvent>().Subscribe(UserRecived);
            _eventAggregator = eventAggregator;
            _reservationDataService = reservationDataService;
            _regionManager = regionManager;
            EditCommand = new DelegateCommand(EditExecute, EditCanExicute);
            DeleteCommand = new DelegateCommand(DeleteExecute, DeleteCanExecute);
        }

        private bool DeleteCanExecute()
        {
            return true;
        }

        private void DeleteExecute()
        {
            _reservationDataService.DeleteReservation(Reservation);
            User.Reservations.Remove(Reservation);
            _eventAggregator.GetEvent<LoginEvent>().Publish(User);

            var i = new NavigationParameters();
            MessageBox.Show("Du har tagit bort din reservation.");
            _regionManager.RequestNavigate("ContentRegion", "UserView", i);
        }

        private bool EditCanExicute()
        {
            return true;
        }

        private async void EditExecute()
        {
            await EditReservation();
        }

        public async Task EditReservation()
        {            
            User.Reservations.Remove(Reservation);
            var reservation = new Reservation
            {
                ReservationId = Reservation.ReservationId,
                UserId = User.UserId,
                RoomId = RoomId,
                Weeks = OneWeek ? 1 : 2,
                StartDate = StartDate,
                EndDate = OneWeek ? StartDate.AddDays(7) : StartDate.AddDays(14),
                TotalPrice = TotalPrice
            };
            
            await _reservationDataService.EditReservation(reservation);
            _eventAggregator.GetEvent<UpdateHotelsEvent>().Publish();
            _eventAggregator.GetEvent<LoginEvent>().Publish(User);
            User.Reservations.Add(reservation);

            var i = new NavigationParameters();
            MessageBox.Show("Din reservation är nu ändrad.");
            _regionManager.RequestNavigate("ContentRegion", "UserView", i);
        }

        private void UserRecived(User obj)
        {
            User = obj;
        }

        private void ReservationRecived(Reservation obj)
        {
            Reservation = obj;
            RoomId = obj.RoomId;
            OneWeek = Reservation.Weeks == 1 ? true : false;
            TwoWeeks = Reservation.Weeks == 2 ? true : false;
            StartDate = obj.StartDate;
            EndDate = obj.EndDate;
            TotalPrice = Reservation.TotalPrice;
        }

        private int _roomId;
        public int RoomId
        {
            get { return _roomId; }
            set { SetProperty(ref _roomId, value); }
        }

        private bool _oneWeek;
        public bool OneWeek
        {
            get { return _oneWeek; }
            set { SetProperty(ref _oneWeek, value);
                if (OneWeek)
                {
                    TwoWeeks = false;
                    //TotalPrice = Room.Price * 7;
                }
            }
        }

        private bool _twoWeeks;
        public bool TwoWeeks
        {
            get { return _twoWeeks; }
            set{ SetProperty(ref _twoWeeks, value);
                if (TwoWeeks) {
                    OneWeek = false;
                    //TotalPrice = Room.Price * 14;
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

        private Reservation _reservation;
        public Reservation Reservation
        {
            get { return _reservation; }
            set { SetProperty(ref _reservation, value); }
        }

        private User _user;
        public User User
        {
            get { return _user; }
            set { SetProperty(ref _user, value); }
        }

        private Room _room;
        public Room Room
        {
            get { return _room; }
            set { SetProperty(ref _room, value); }
        }
    }
}
