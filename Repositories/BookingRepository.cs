using BookingSystemAPI.Models;

namespace BookingSystemAPI.Repositories
{
    /// <summary>
    /// IRepository
    /// </summary>
    public interface IBookingRepository
    {
        Task AddBooking(Booking booking);
        Task<Booking?> GetBooking(string bookingCode);
    }

    /// <summary>
    /// ConcreteRepository
    /// </summary>
    public class BookingRepository : IBookingRepository
    {
        private readonly List<Booking> _bookings = new();


        public Task AddBooking(Booking booking)
        {
            _bookings.Add(booking);
            return Task.CompletedTask;
        }

        public Task<Booking?> GetBooking(string bookingCode)
        {
            var booking = _bookings.FirstOrDefault(x => x.BookingCode == bookingCode);
            return Task.FromResult(booking);
        }
    }
}
