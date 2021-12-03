using HotelApp.DataAccess.Services;
using HotelApp.Model.Model;
using Prism.Commands;
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
    public class CreateNewUserViewModel : BindableBase
    {
        private readonly IUserDataService _userDataService;
        private readonly IRegionManager _regionManager;
        public DelegateCommand SaveUserCommand { get; set; }
        public CreateNewUserViewModel(IUserDataService userDataService, IRegionManager regionManager)
        {
            SaveUserCommand = new DelegateCommand(Execute, CanExecute);
            _userDataService = userDataService;
            _regionManager = regionManager;
        }

        private bool CanExecute()
        {
            return true;
        }

        private void Execute()
        {
            CreateUser();
        }

        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set { SetProperty(ref _firstName, value); }
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set { SetProperty(ref _lastName, value); }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        public void CreateUser()
        {
            if (FirstName != null && LastName != null && Email != null && Password != null)
            {
                User user = new User
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    Email = Email,
                    Password = Password
                };

                _userDataService.SaveUser(user);

                var i = new NavigationParameters();
                MessageBox.Show("Du har nu skapat ett konto. Logga in för att göra en reservation.");
                _regionManager.RequestNavigate("ContentRegion", "LoginView", i);
            }
            else
            {
                MessageBox.Show("Du har inte fyllt i alla rader korrekt.");
            }
        }
    }
}
