using HotelApp.Model.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.DataAccess.Data
{
    public static class SeedData
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hotel>().HasData(
                new Hotel { HotelId = 1, Name = "The Mark, New York", Description = "This swish uptown hotel on the Upper East side is near the Met, Central Park and lots of upscale shopping in Fifth Avenue." },
                new Hotel { HotelId = 2, Name = "Hotel Martinez, Cannes", Description = "Right on La Croisette, this gleaming white, art deco icon, plays host to many a celebrity-filled shindig during the Cannes Film Festival." },
                new Hotel { HotelId = 3, Name = "BVLGARI Resort & Residences, Dubai", Description = "On its own private, man-made island, with a lovely white sand beach, the resort is accessed by a 300-metre bridge from the mainland." }
                );

            modelBuilder.Entity<Room>().HasData(
                new Room { RoomId = 1, Size = "Singelrum", Price = 1000, HotelId = 1 },
                new Room { RoomId = 2, Size = "Dubbelrum", Price = 2000, HotelId = 1 },
                new Room { RoomId = 3, Size = "Familjerum", Price = 3000, HotelId = 1 },
                new Room { RoomId = 4, Size = "Singelrum", Price = 1000, HotelId = 2 },
                new Room { RoomId = 5, Size = "Dubbelrum", Price = 2000, HotelId = 2 },
                new Room { RoomId = 6, Size = "Familjerum", Price = 3000, HotelId = 2 },
                new Room { RoomId = 7, Size = "Singelrum", Price = 1000, HotelId = 3 },
                new Room { RoomId = 8, Size = "Dubbelrum", Price = 2000, HotelId = 3 },
                new Room { RoomId = 9, Size = "Familjerum", Price = 3000, HotelId = 3 }
                );

            modelBuilder.Entity<User>().HasData(
                new User { UserId = 1, FirstName = "Kalle", LastName = "Anka", Email = "kalle@gmail.com", Password = "hej123" },
                new User { UserId = 2, FirstName = "Fredric", LastName = "Fredriksson", Email = "fredric@gmail.com", Password = "hej123" },
                new User { UserId = 3, FirstName = "Anders", LastName = "Andersson", Email = "anders@gmail.com", Password = "hej123" },
                new User { UserId = 4, FirstName = "Maria", LastName = "Svensson", Email = "maria@gmail.com", Password = "hej123" },
                new User { UserId = 5, FirstName = "Julia", LastName = "Larsson", Email = "julia@gmail.com", Password = "hej123" }
                );

            modelBuilder.Entity<Reservation>().HasData(
                new Reservation { ReservationId = 1, UserId = 1, RoomId = 1, StartDate = DateTime.Parse("2021-11-02"), EndDate = DateTime.Parse("2021-11-09"), TotalPrice = 7000, Weeks = 1 },
                new Reservation { ReservationId = 2, UserId = 1, RoomId = 2, StartDate = DateTime.Parse("2021-11-12"), EndDate = DateTime.Parse("2021-11-26"), TotalPrice = 28000, Weeks = 2 },
                new Reservation { ReservationId = 3, UserId = 1, RoomId = 3, StartDate = DateTime.Parse("2021-11-08"), EndDate = DateTime.Parse("2021-11-15"), TotalPrice = 21000, Weeks = 1 }
                );
        }
    }
}
