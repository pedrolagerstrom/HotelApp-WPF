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
using System.Windows.Controls;

namespace HotelApp.ViewModels
{
    public class LoginViewModel : BindableBase
    {
        private readonly IUserDataService _userDataService;
        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManager;

        public ObservableCollection<User> Users { get; set; }

        public DelegateCommand<object> LoginCommand { get; set; }
        public DelegateCommand<string> NavigateCommand { get; set; }

        private string _email;
        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }

        public string _password;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        public LoginViewModel(IUserDataService userDataService, IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            _userDataService = userDataService;
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;
            Users = new ObservableCollection<User>();
            LoginCommand = new DelegateCommand<object>(Execute);
            NavigateCommand = new DelegateCommand<string>(Navigate);
        }

        private async void Execute(object parameter)
        {
            try
            {
                User user = await _userDataService.GetUserByEmail(Email);
                var passwordBox = parameter as PasswordBox;
                var password = passwordBox.Password.ToString();

                LoginCheck(user, password);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        public void LoginCheck(User user, string password)
        {
            bool verifeid = user != null && password == user.Password;
            if (verifeid)
            {
                _eventAggregator.GetEvent<LoginEvent>().Publish(user);
                var i = new NavigationParameters();
                MessageBox.Show($"Välkommen {user.FirstName}");
                _regionManager.RequestNavigate("ContentRegion", "UserView", i);
                Email = "";
                Password = "";
            }
            else
            {
                MessageBox.Show("Fel email eller lösenord, försök igen.");
            }
        }

        private void Navigate(string viewName)
        {
            _regionManager.RequestNavigate("ContentRegion", viewName);
        }

        public async Task LoadUsers()
        {
            var users = await _userDataService.GetUsers();
            foreach (var user in users)
            {
                Users.Add(user);
            }
        }        
    }
}
