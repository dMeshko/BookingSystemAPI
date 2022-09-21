namespace BookingSystemAPI.Models
{
    /// <summary>
    /// Bookings made by the user
    /// </summary>
    public class Booking
    {
        public BookingType BookingType { get; private set; }
        public string OptionCode { get; private set; }
        public string BookingCode { get; private set; }
        public ushort SleepTime { get; private set; }
        public DateTime BookingTime { get; private set; }

        public Booking(BookingType bookingType, string optionCode)
        {
            var random = new Random();

            BookingType = bookingType;
            OptionCode = optionCode;
            SleepTime = (ushort)random.Next(30, 60);
            BookingTime = DateTime.UtcNow;

            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            BookingCode = new string(Enumerable.Repeat(chars, 6)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }

    public enum BookingType
    {
        Normal,
        LastMinute
    }
}
