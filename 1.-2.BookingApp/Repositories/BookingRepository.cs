using BookingApp.Models.Bookings.Contracts;
using BookingApp.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookingApp.Repositories
{
    public class BookingRepository : IRepository<IBooking>
    {
        private ICollection<IBooking> bookings;
        public BookingRepository()
        {
            bookings = new List<IBooking>();
        }
        
        public void AddNew(IBooking model) => bookings.Add(model);

        public IReadOnlyCollection<IBooking> All() => bookings as IReadOnlyCollection<IBooking>;

        public IBooking Select(string criteria) => bookings.FirstOrDefault(x => x.BookingNumber == int.Parse(criteria));
    }
}
