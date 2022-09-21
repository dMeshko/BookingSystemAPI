namespace BookingSystemAPI.Services
{
    /// <summary>
    /// FactoryMethod
    /// </summary>
    public class IntegrationServiceFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public IntegrationServiceFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IIntegrationService? GetIntegrationService(IntegrationServiceType type)
        {
            var services = _serviceProvider.GetServices<IIntegrationService>();
            return type switch
            {
                IntegrationServiceType.Hotel => services.First(x => x.GetType() == typeof(HotelService)),
                IntegrationServiceType.HotelAndFlight => services.First(x => x.GetType() == typeof(HotelAndFlightService)),
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }
    }

    public enum IntegrationServiceType
    {
        Hotel,
        HotelAndFlight
    }
}
