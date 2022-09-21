namespace BookingSystemAPI.Models
{
    /// <summary>
    /// Represents the flight returned by the external flights API
    /// </summary>
    public class FlightDto
    {
        public int FlightCode { get; set; }
        public string FlightNumber { get; set; }
        public string DepartureAirport { get; set; }
        public string ArrivalAirport { get; set; }
    }
}
