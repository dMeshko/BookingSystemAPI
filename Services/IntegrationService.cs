using AutoMapper;
using BookingSystemAPI.Models;
using BookingSystemAPI.ViewModels;

namespace BookingSystemAPI.Services
{
    /// <summary>
    /// Proxy pattern - subject
    /// </summary>
    public interface IIntegrationService
    {
        Task<SearchRes> Search(SearchReq searchReq);
    }

    /// <summary>
    /// Proxy pattern - real subject
    /// </summary>
    public class HotelService : IIntegrationService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMapper _mapper;
        private readonly HttpClient _hotelsHttpClient;

        public HotelService(IHttpClientFactory httpClientFactory, IMapper mapper)
        {
            _httpClientFactory = httpClientFactory;
            _mapper = mapper;
            _hotelsHttpClient = _httpClientFactory.CreateClient("HotelsEndpoint");
        }

        public async Task<SearchRes> Search(SearchReq searchReq)
        {
            var hotels = await _hotelsHttpClient.GetFromJsonAsync<IEnumerable<HotelDto>>(
                $"?destinationCode={searchReq.Destination}");

            var response = new SearchRes();

            foreach (var hotel in hotels!)
            {
                response.Options.Add(_mapper.Map<Option>(hotel));
            }

            return response;
        }
    }

    /// <summary>
    /// Proxy pattern - proxy
    /// </summary>
    public class HotelAndFlightService : IIntegrationService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMapper _mapper;
        private readonly HttpClient _flightsHttpClient;
        private readonly IntegrationServiceFactory _integrationServiceFactory;

        public HotelAndFlightService(IHttpClientFactory httpClientFactory, IMapper mapper, IntegrationServiceFactory integrationServiceFactory)
        {
            _httpClientFactory = httpClientFactory;
            _mapper = mapper;
            _flightsHttpClient = _httpClientFactory.CreateClient("FlightsEndpoint");
            _integrationServiceFactory = integrationServiceFactory;
        }

        public async Task<SearchRes> Search(SearchReq searchReq)
        {
            var hotelService = _integrationServiceFactory.GetIntegrationService(IntegrationServiceType.Hotel);
            var response = await hotelService.Search(searchReq);

            var flights = await _flightsHttpClient.GetFromJsonAsync<IEnumerable<FlightDto>>(
                $"?departureAirport={searchReq.DepartureAirport}&arrivalAirport={searchReq.Destination}");

            foreach (var flight in flights!)
            {
                response.Options.Add(_mapper.Map<Option>(flight));
            }

            return response;
        }
    }
}
