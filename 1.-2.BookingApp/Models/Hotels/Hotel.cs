using BookingApp.Models.Bookings.Contracts;
using BookingApp.Models.Hotels.Contacts;
using BookingApp.Models.Rooms.Contracts;
using BookingApp.Repositories;
using BookingApp.Repositories.Contracts;
using BookingApp.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookingApp.Models.Hotels
{
    public class Hotel : IHotel
    {
        private string fullName;
        private int category;
        private double turnover = 0;
        private IRepository<IRoom> rooms;
        private IRepository<IBooking> bookings;

        public Hotel(string fullName, int category)
        {
            rooms = new RoomRepository();
            bookings = new BookingRepository();
            this.FullName = fullName;
            this.Category = category;
        }

        public string FullName
        {
            get => this.fullName;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.HotelNameNullOrEmpty);
                }
                this.fullName = value;
            }
        }

        public int Category
        {
            get => this.category;
            private set
            {
                if (value < 1 || value > 5)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidCategory);
                }
                this.category = value;
            }
        }

        public double Turnover => this.Bookings.All().Sum(x => x.ResidenceDuration * x.Room.PricePerNight);
        //{
        //    get => this.turnover;
        //    private set
        //    {
        //        double total = 0;
        //        foreach (var booking in bookings.All())
        //        {
        //            total += booking.ResidenceDuration * booking.Room.PricePerNight;
        //        }
        //        this.turnover = total;
        //    }
        //}
        
        public IRepository<IRoom> Rooms
        {
            get => this.rooms;
            set => this.rooms = value;
        }
        public IRepository<IBooking> Bookings 
        {
            get => this.bookings;
            set => this.bookings = value;
        }
    }
}
