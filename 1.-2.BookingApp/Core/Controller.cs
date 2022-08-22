using BookingApp.Core.Contracts;
using BookingApp.Models.Bookings;
using BookingApp.Models.Hotels;
using BookingApp.Models.Hotels.Contacts;
using BookingApp.Models.Rooms;
using BookingApp.Models.Rooms.Contracts;
using BookingApp.Repositories;
using BookingApp.Repositories.Contracts;
using BookingApp.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookingApp.Core
{
    public class Controller : IController
    {
        private IRepository<IHotel> hotels;

        public Controller()
        {
            hotels = new HotelRepository();
        }
        public string AddHotel(string hotelName, int category)
        {
            
            foreach (var hotels in hotels.All())
            {
                if (hotels.FullName == hotelName)
                {
                    return $"{String.Format(OutputMessages.HotelAlreadyRegistered, hotelName)}";
                }
            }
            Hotel hotel = new Hotel(hotelName, category);
            hotels.AddNew(hotel);
            return $"{String.Format(OutputMessages.HotelSuccessfullyRegistered, category, hotelName)}";
        }
        public string UploadRoomTypes(string hotelName, string roomTypeName)
        {
            IHotel hotel = hotels.All().FirstOrDefault(x => x.FullName == hotelName);
            if (!hotels.All().Any(x => x.FullName == hotelName))
            {
                return $"{String.Format(OutputMessages.HotelNameInvalid, hotelName)}";
            }
            if (hotel.Rooms.All().Any(x => x.GetType().Name == roomTypeName))
            {
                return $"{String.Format(OutputMessages.RoomTypeAlreadyCreated)}";
            }
            if (roomTypeName == "Apartment")
            {
                Apartment room = new Apartment();
                hotel.Rooms.AddNew(room);
                return $"{String.Format(OutputMessages.RoomTypeAdded, roomTypeName, hotelName)}";
            }
            else if (roomTypeName == "DoubleBed")
            {
                DoubleBed room = new DoubleBed();
                hotel.Rooms.AddNew(room);
                return $"{String.Format(OutputMessages.RoomTypeAdded, roomTypeName, hotelName)}";
            }
            else if (roomTypeName == "Studio")
            {
                Studio room = new Studio();
                hotel.Rooms.AddNew(room);
                return $"{String.Format(OutputMessages.RoomTypeAdded, roomTypeName, hotelName)}";
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.RoomTypeIncorrect);
            }
        }
        public string SetRoomPrices(string hotelName, string roomTypeName, double price)
        {
            IHotel hotel = hotels.All().FirstOrDefault(x => x.FullName == hotelName);
            IRoom room = hotel.Rooms.All().FirstOrDefault(x => x.GetType().Name == roomTypeName);
            if (!hotels.All().Any(x => x == hotel))
            {
                return $"{String.Format(OutputMessages.HotelNameInvalid, hotelName)}";
            }
            if (roomTypeName != "Apartment" && roomTypeName != "DoubleBed" && roomTypeName != "Studio")
            {
                throw new ArgumentException(ExceptionMessages.RoomTypeIncorrect);
            }
            if (!hotel.Rooms.All().Any(x => x.GetType().Name == roomTypeName))
            {
                return $"{OutputMessages.RoomTypeNotCreated}";
            }
            if (room.PricePerNight != 0)
            {
                throw new InvalidOperationException(ExceptionMessages.PriceAlreadySet);
            }
            room.SetPrice(price);
            return $"{String.Format(OutputMessages.PriceSetSuccessfully, roomTypeName, hotelName)}";
        }
        public string BookAvailableRoom(int adults, int children, int duration, int category)
        {
            List<IHotel> hotelList = hotels.All().OrderBy(x => x.FullName).ToList();
            List<IRoom> roomsWithPrice = new List<IRoom>();
            int totalGuests = adults + children;

            foreach (var hotel in hotelList)
            {
                foreach (var room in hotel.Rooms.All())
                {
                    if (room.PricePerNight > 0)
                    {
                        roomsWithPrice.Add(room);
                    }
                }
            }
            if (!hotels.All().Any(x => x.Category == category))
            {
                return $"{String.Format(OutputMessages.CategoryInvalid, category)}";
            }
            if (!roomsWithPrice.Contains(roomsWithPrice.FirstOrDefault(x => x.BedCapacity >= totalGuests)))
            {
                return $"{String.Format(OutputMessages.RoomNotAppropriate)}";
            }
            else
            {
                List<IRoom> sortedRooms = roomsWithPrice.OrderBy(x => x.BedCapacity).ToList();
                IRoom selectedRoom = sortedRooms.FirstOrDefault(x => x.BedCapacity >= totalGuests);
                string selectedRoomName = selectedRoom.GetType().Name;
                IHotel selectedHotel = hotels.All().FirstOrDefault(x => x.Rooms.Select(selectedRoomName) == selectedRoom);

                int bookingNumber = selectedHotel.Bookings.All().Count + 1;
                Booking booking = new Booking(selectedRoom, duration, adults, children, bookingNumber);
                selectedHotel.Bookings.AddNew(booking);

                return $"{String.Format(OutputMessages.BookingSuccessful, bookingNumber, selectedHotel.FullName)}";
            }
            
            
            
        }

        public string HotelReport(string hotelName)
        {
            var sb = new StringBuilder();
            if (!hotels.All().Contains(hotels.Select(hotelName)))
            {
                sb.AppendLine($"Profile {hotelName} doesn't exist!");
            }
            IHotel hotel = hotels.Select(hotelName);

            sb.AppendLine($"Hotel name: {hotel.FullName}");
            sb.AppendLine($"--{hotel.Category} star hotel");
            sb.AppendLine($"--Turnover: {hotel.Turnover:F2} $");
            sb.AppendLine($"Bookings: ");
            
            if (hotel.Bookings.All().Count == 0)
            {
                sb.AppendLine("none");
            }
            else
            {
                foreach (var booking in hotel.Bookings.All())
                {
                    sb.AppendLine(booking.BookingSummary());
                }
            }

            return sb.ToString().TrimEnd();
        }

        

        
    }
}
