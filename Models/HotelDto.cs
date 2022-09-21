namespace BookingSystemAPI.Models
{
    /// <summary>
    /// Represents the hotel returned by the external hotels API
    /// </summary>
    public class HotelDto
    {
        public long Id { get; set; }
        public long HotelCode { get; set; }
        public string HotelName { get; set; }
        public string DestinationCode { get; set; }
        public string City { get; set; }
    }
}
