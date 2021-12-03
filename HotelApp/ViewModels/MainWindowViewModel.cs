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
    public class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;

        private User _user;
        public User User
        {
            get { return _user; }
            set { SetProperty(ref _user, value); }
        }

        private string _email = "Logga in";
        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }

        public bool isLoggedIn { get; set; }

        public DelegateCommand<string> NavigateCommand { get; set; }
        public MainWindowViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            NavigateCommand = new DelegateCommand<string>(Navigate);
            _regionManager = regionManager;
            eventAggregator.GetEvent<LoginEvent>().Subscribe(UserReceived);
        }

        private void UserReceived(User user)
        {
            if(user != null)
            {
                User = user;
                Email = user.Email;
                foreach (var reservation in User.Reservations)
                {
                    Debug.WriteLine(reservation.RoomId);
                }
                isLoggedIn = true;
            }
            else if (user == null)
            {
                User = user;
                Email = "Logga in";
                isLoggedIn = false;
            }
        }

        private void Navigate(string viewName)
        {
            if (viewName == "LoginView")
            {
                if (isLoggedIn)
                {
                    var i = new NavigationParameters();
                    MessageBox.Show("Du är redan inloggad");
                    _regionManager.RequestNavigate("ContentRegion", "UserView", i);
                }
                else
                {
                    _regionManager.RequestNavigate("ContentRegion", viewName);
                }
            }
            else if (viewName == "HotelView")
            {
                _regionManager.RequestNavigate("ContentRegion", viewName);
            }
            else if (viewName == "UserView")
            {
                if (isLoggedIn)
                {
                    _regionManager.RequestNavigate("ContentRegion", viewName);
                }
                else
                {
                    MessageBox.Show("Du måste logga in.");
                    _regionManager.RequestNavigate("ContentRegion", "LoginView");
                }
            }
        }
    }
}
