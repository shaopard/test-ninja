using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class BookingHelper_OverlappingBookingsExistTests
    {
        private Mock<IBookingRepository> _bookingRepository;
        private Booking _existingBooking;

        [SetUp]
        public void SetUp()
        {
            _bookingRepository = new Mock<IBookingRepository>();
            _existingBooking = new Booking
            {
                Id = 2,
                ArrivalDate = ArriveOn(2017, 1, 15),
                DepartureDate = DepartOn(2017, 1, 20),
                Reference = "a",
            };

            _bookingRepository.Setup(r => r.GetActiveBookings(1)).Returns(new List<Booking>
            {
                _existingBooking
            }.AsQueryable());
        }

        [Test]
        public void OverlapCancelledBooking_ReturnsEmptyString()
        {
            var booking = new Booking { Status = "Cancelled" };

            var result = BookingHelper.OverlappingBookingsExist(booking, _bookingRepository.Object);

            Assert.That(result, Is.EqualTo(string.Empty));
        }

        [Test]
        public void BookingStartsAndFinishesBeforeAnExistingBooking_ReturnsEmptyString()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate, days: 2),
                DepartureDate = Before(_existingBooking.ArrivalDate),
                Reference = "a",
            }, _bookingRepository.Object);

            Assert.That(result, Is.EqualTo(string.Empty));
        }

        [Test]
        public void BookingStartsBeforeAndFinishesInTheMiddleOfAnExistingBooking_ReturnsExistingBookingReference()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate, days: 2),
                DepartureDate = After(_existingBooking.ArrivalDate),
                Reference = "a",
            }, _bookingRepository.Object);

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void BookingStartsBeforeAndFinishesAfterAnExistingBooking_ReturnsExistingBookingReference()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate, days: 2),
                DepartureDate = After(_existingBooking.DepartureDate),
                Reference = "a",
            }, _bookingRepository.Object);

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void BookingStartsAndFinishesInTheMiddleAnExistingBooking_ReturnsExistingBookingReference()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(_existingBooking.ArrivalDate, days: 1),
                DepartureDate = Before(_existingBooking.DepartureDate, days: 1),
                Reference = "a",
            }, _bookingRepository.Object);

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void BookingStartsInTheMiddleAnExistingBookingAndFinishesAfter_ReturnsExistingBookingReference()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(_existingBooking.ArrivalDate, days: 1),
                DepartureDate = After(_existingBooking.DepartureDate, days: 1),
                Reference = "a",
            }, _bookingRepository.Object);

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void BookingStartsAndFinishesAfterAnExistingBooking_ReturnsExistingBookingReference()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(_existingBooking.DepartureDate, days: 1),
                DepartureDate = After(_existingBooking.DepartureDate, days: 2),
                Reference = "a",
            }, _bookingRepository.Object);

            Assert.That(result, Is.EqualTo(string.Empty));
        }


        private DateTime ArriveOn(int year, int month, int day) => new DateTime(year, month, day, 14, 0, 0);

        private DateTime DepartOn(int year, int month, int day) => new DateTime(year, month, day, 10, 0, 0);

        private DateTime Before(DateTime dateTime, int days = 1) => dateTime.AddDays(-days);

        private DateTime After(DateTime dateTime, int days = 1) => dateTime.AddDays(days);
    }
}
