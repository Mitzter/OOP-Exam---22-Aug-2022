using FrontDeskApp;
using NUnit.Framework;
using System;
using System.Linq;

namespace BookigApp.Tests
{
    public class Tests
    {
        //[SetUp]
        //public void Setup()
        //{
        //}

        //[Test]
        //public void Test1()
        //{
        //    Assert.Pass();
        //}

        [Test]
        public void HotelConstructor_InitializingCorrectly()
        {
            string expectedName = "Hotel";
            int expectedCategory = 1;
            int expectedRoomsCount = 0;
            int expectedBookingsCount = 0;
            double expectedTurnover = 0;

            Hotel hotel = new Hotel("Hotel", 1);

            Assert.AreEqual(expectedName, hotel.FullName,
                "Hotel must have a name!");
            Assert.AreEqual(expectedCategory, hotel.Category,
                "Hotel must have category!");
            Assert.AreEqual(expectedRoomsCount, hotel.Rooms.Count,
                "Hotel rooms list must be initialized!");
            Assert.AreEqual(expectedBookingsCount, hotel.Bookings.Count,
                "Hotel bookings list must be instantiated!");
            Assert.AreEqual(expectedTurnover, hotel.Turnover,
                "Hotel must start with 0 turnover!");
        }

        [TestCase(" ")]
        [TestCase("")]
        [TestCase(null)]
        [TestCase("    ")]
        public void HotelFullNameProperty_ThrowsArgumentNullException(string name)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                Hotel hotel = new Hotel(name, 1);
            });
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(6)]
        [TestCase(10)]
        public void HotelCategoryProperty_ThrowsArgumentException(int category)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Hotel hotel = new Hotel("Hotel", category);
            });
        }

        [Test]
        public void HotelAddRoomMethod()
        {
            int count = 1;
            Hotel hotel = new Hotel("Hotel", 1);
            Room room = new Room(1, 100);
            hotel.AddRoom(room);

            Assert.AreEqual(room, hotel.Rooms.FirstOrDefault());
            Assert.AreEqual(1, hotel.Rooms.Count);
        }


        [Test]
        public void RoomConstructor()
        {
            int expectedCapacity = 2;
            double expectedPrice = 200;

            Room room = new Room(2, 200);

            Assert.AreEqual(expectedCapacity, room.BedCapacity);
            Assert.AreEqual(expectedPrice, room.PricePerNight);
        }

        [TestCase(0)]
        [TestCase(null)]
        [TestCase(-1)]
        public void RoomBedCapacityProperty_ThrowsArgumentException(int capacity)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Room room = new Room(capacity, 100);
            });
        }
        [TestCase(0)]
        [TestCase(null)]
        [TestCase(-1)]
        public void RoomPriceProperty_ThrowsArgumentException(double price)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Room room = new Room(1, price);
            });
        }


        [TestCase(0)]
        [TestCase(null)]
        [TestCase(-1)]
        public void HotelBookRoom_AdultsArgumentException(int adults)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Hotel hotel = new Hotel("Hotel", 1);
                Room room = new Room(3, 300);
                hotel.AddRoom(room);
                hotel.BookRoom(adults, 1, 1, 20);
            });
        }
       
        [TestCase(-1)]
        [TestCase(-100)]
        [TestCase(-2)]
        public void HotelBookRoom_ChildrenArgumentException(int children)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Hotel hotel = new Hotel("Hotel", 1);
                Room room = new Room(3, 300);
                hotel.AddRoom(room);
                hotel.BookRoom(1, children, 1, 20);
            });
        }
        [TestCase(0)]
        [TestCase(null)]
        [TestCase(-1)]
        public void HotelBoomRoom_DurationArgumentException(int duration)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Hotel hotel = new Hotel("Hotel", 1);
                Room room = new Room(3, 300);
                hotel.AddRoom(room);
                hotel.BookRoom(1, 1, duration, 20);
            });
        }
        [Test]
        public void HotelBoomRoom_BookingAdded()
        {
            Hotel hotel = new Hotel("Hotel", 1);
            Room room = new Room(3, 20);
            hotel.AddRoom(room);
            hotel.BookRoom(1, 2, 1, 20);

            Assert.AreEqual(1, hotel.Bookings.Count);
        }
        [Test]
        public void HotelBoomRoom_Budget()
        {
            Hotel hotel = new Hotel("Hotel", 1);
            Room room = new Room(3, 20);
            hotel.AddRoom(room);
            hotel.BookRoom(1, 1, 1, 20);

            double expectedTurnover = 20;
            Assert.AreEqual(expectedTurnover, hotel.Turnover);
        }
    }
}