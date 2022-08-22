using System;
using System.Collections.Generic;
using System.Text;

namespace BookingApp.Models.Rooms
{
    internal class Studio : Room
    {
        private const int bedCapacity = 4;
        public Studio() : base(bedCapacity)
        {
        }
    }
}
