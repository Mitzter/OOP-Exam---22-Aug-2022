using System;
using System.Collections.Generic;
using System.Text;

namespace BookingApp.Models.Rooms
{
    public class Apartment : Room
    {
        private const int bedCapacity = 6;
        public Apartment() : base(bedCapacity)
        {
        }
    }
}
