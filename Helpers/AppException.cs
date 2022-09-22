namespace BookingSystemAPI.Helpers
{
    public class AppException : Exception
    {
        public bool IsJson { get; protected set; }

        public AppException(string message) : base(message)
        {

        }
    }
}
