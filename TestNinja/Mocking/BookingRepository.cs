using System.Linq;

namespace TestNinja.Mocking
{
    public interface IBookingRepository
    {
        IQueryable<Booking> GetActiveBookings(int? excludedBookingId = null);
    }

    public class BookingRepository : IBookingRepository
    {
        public IQueryable<Booking> GetActiveBookings(int? excludedBookingId = null)
        {
            var unitOfWork = new UnitOfWork();
            var bookings =
                unitOfWork.Query<Booking>()
                    .Where(b =>
                            b.Status != "Cancelled" 
                            && (!excludedBookingId.HasValue || b.Id != excludedBookingId.Value));

            return bookings;
        }
    }
}
 