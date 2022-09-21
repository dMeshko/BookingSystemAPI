using BookingSystemAPI.ViewModels;

namespace BookingSystemAPI.Services
{
    /// <summary>
    /// Strategy
    /// </summary>
    public interface ISearchStrategy
    {
        public Task<SearchRes> Search(SearchReq searchReq);
    }

    /// <summary>
    /// ConcreteStrategy
    /// </summary>
    public class HotelOnlySearch : ISearchStrategy
    {
        private readonly IIntegrationService _hotelService;

        public HotelOnlySearch(IntegrationServiceFactory integrationServiceFactory)
        {
            _hotelService = integrationServiceFactory.GetIntegrationService(IntegrationServiceType.Hotel);
        }

        public Task<SearchRes> Search(SearchReq searchReq)
        {
            return _hotelService.Search(searchReq);
        }
    }

    /// <summary>
    /// ConcreteStrategy
    /// </summary>
    public class HotelAndFlightSearch : ISearchStrategy
    {
        private readonly IIntegrationService _hotelAndFlightService;

        public HotelAndFlightSearch(IntegrationServiceFactory integrationServiceFactory)
        {
            _hotelAndFlightService = integrationServiceFactory.GetIntegrationService(IntegrationServiceType.HotelAndFlight);
        }

        public Task<SearchRes> Search(SearchReq searchReq)
        {
            return _hotelAndFlightService.Search(searchReq);
        }
    }

    /// <summary>
    /// ConcreteStrategy
    /// </summary>
    public class LastMinuteHotelsSearch : ISearchStrategy
    {
        private readonly IIntegrationService _hotelService;

        public LastMinuteHotelsSearch(IntegrationServiceFactory integrationServiceFactory)
        {
            _hotelService = integrationServiceFactory.GetIntegrationService(IntegrationServiceType.Hotel);
        }

        public Task<SearchRes> Search(SearchReq searchReq)
        {
            return _hotelService.Search(searchReq);
        }
    }
}
