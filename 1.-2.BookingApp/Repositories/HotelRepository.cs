﻿using BookingApp.Models.Hotels.Contacts;
using BookingApp.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookingApp.Repositories
{
    public class HotelRepository : IRepository<IHotel>
    {
        private ICollection<IHotel> hotels;
        public HotelRepository()
        {
            hotels = new List<IHotel>();
        }

       

        public void AddNew(IHotel model) => hotels.Add(model);

        public IReadOnlyCollection<IHotel> All() => hotels as IReadOnlyCollection<IHotel>;

        public IHotel Select(string criteria) => hotels.FirstOrDefault(x => x.FullName == criteria);
    }
}
