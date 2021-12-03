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
    public class UserViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManager;
        public ObservableCollection<Reservation> Reservations { get; set; }
        public DelegateCommand EditReservationCommand { get; set; }
        public DelegateCommand LogoutCommand { get; set; }

        public UserViewModel(IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            Reservations = new ObservableCollection<Reservation>();
            eventAggregator.GetEvent<LoginEvent>().Subscribe(UserRecived);
            EditReservationCommand = new DelegateCommand(EditReservationExecute, EditReservationCanExecute);
            LogoutCommand = new DelegateCommand(LogoutExecute, LogoutCanExecute);
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;
        }

        private bool LogoutCanExecute()
        {
            return true;
        }

        private void LogoutExecute()
        {
            User user = null;
            _eventAggregator.GetEvent<LoginEvent>().Publish(user);
        }

        private bool EditReservationCanExecute()
        {
            return true;
        }

        private void EditReservationExecute()
        {
            if (SelectedReservation != null)
            {
                _eventAggregator.GetEvent<EditReservationEvent>().Publish(SelectedReservation);
                _regionManager.RequestNavigate("ContentRegion", "EditReservationView");
            }
            else
            {
                MessageBox.Show("Välj en reservation.");
            }
        }

        private void UserRecived(User obj)
        {
            if (obj != null)
            {
                Reservations.Clear();
                User = obj;
                foreach (var reservation in obj.Reservations)
                {
                    Reservations.Add(reservation);
                }
            }
            else if (obj == null)
            {
                var i = new NavigationParameters();
                MessageBox.Show("Du är nu utloggad.");
                _regionManager.RequestNavigate("ContentRegion", "LoginView", i);
            }
        }

        private User _user;

        public User User
        {
            get { return _user; }
            set { SetProperty(ref _user, value); }
        }

        private Reservation _selectedReservation;

        public Reservation SelectedReservation
        {
            get { return _selectedReservation; }
            set { SetProperty(ref _selectedReservation, value); }
        }
    }
}
