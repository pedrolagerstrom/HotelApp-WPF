using HotelApp.DataAccess.Data;
using HotelApp.DataAccess.Services;
using HotelApp.Views;
using Prism.Ioc;
using Prism.Regions;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace HotelApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<HotelAppDbContext>();
            containerRegistry.RegisterScoped<IHotelDataService, HotelDataService>();
            containerRegistry.RegisterScoped<IReservationDataService, ReservationDataService>();
            containerRegistry.RegisterScoped<IRoomDataService, RoomDataService>();
            containerRegistry.RegisterScoped<IUserDataService, UserDataService>();

            containerRegistry.RegisterForNavigation<HotelView>();
            containerRegistry.RegisterForNavigation<LoginView>();
            containerRegistry.RegisterForNavigation<CreateNewUserView>();
            containerRegistry.RegisterForNavigation<EditReservationView>();
            containerRegistry.RegisterForNavigation<ReservationView>();
            containerRegistry.RegisterForNavigation<RoomView>();
            containerRegistry.RegisterForNavigation<UserView>();
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<Views.MainWindow>();
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            var regionManager = Container.Resolve<IRegionManager>();
            var contentRegion = regionManager.Regions["ContentRegion"];
            var hotelView = Container.Resolve<HotelView>();
            var loginView = Container.Resolve<LoginView>();
            var editReservationView = Container.Resolve<EditReservationView>();
            var reservationView = Container.Resolve<ReservationView>();
            var roomView = Container.Resolve<RoomView>();
            var userView = Container.Resolve<UserView>();

            contentRegion.Add(hotelView);
            contentRegion.Add(loginView);
            contentRegion.Add(userView);
            contentRegion.Add(editReservationView);
            contentRegion.Add(reservationView);
            contentRegion.Add(roomView);
        }
    }
}
