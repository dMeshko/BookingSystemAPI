using AutoMapper;
using BookingSystemAPI.Models;
using BookingSystemAPI.Repositories;
using BookingSystemAPI.ViewModels;

namespace BookingSystemAPI.Services
{
    public interface IManagerService
    {
        /// <summary>
        /// Searches for hotels or flights
        /// </summary>
        /// <param name="searchRequest"></param>
        /// <returns></returns>
        Task<SearchRes> Search(SearchReq searchReq);
        
        /// <summary>
        /// Makes a booking based on the optionCode
        /// </summary>
        /// <param name="bookRequest"></param>
        /// <returns></returns>
        Task<BookRes> Book(BookReq bookReq);

        /// <summary>
        /// Gets a booking by bookingCode
        /// </summary>
        /// <param name="bookingCode"></param>
        /// <returns></returns>
        Task<BookRes?> GetBooking(string bookingCode);
        
        /// <summary>
        /// Checks booking status based on bookingCode
        /// </summary>
        /// <param name="checkStatusReq"></param>
        /// <returns></returns>
        Task<CheckStatusRes?> CheckStatus(CheckStatusReq checkStatusReq);
    }

    /// <summary>
    /// StrategyContext
    /// </summary>
    public class ManagerService : IManagerService
    {
        private ISearchStrategy _searchStrategy;
        private readonly IMapper _mapper;
        private readonly IBookingRepository _bookingRepository;
        private readonly IntegrationServiceFactory _integrationServiceFactory;

        public ManagerService(IMapper mapper, IBookingRepository bookingRepository, IntegrationServiceFactory integrationServiceFactory)
        {
            _mapper = mapper;
            _bookingRepository = bookingRepository;
            _integrationServiceFactory = integrationServiceFactory;
        }


        public Task<SearchRes> Search(SearchReq searchReq)
        {
            SetStrategy(searchReq);
            return _searchStrategy.Search(searchReq);
        }

        private void SetStrategy(SearchReq searchReq)
        {
            if (string.IsNullOrEmpty(searchReq.DepartureAirport))
            {
                if (searchReq.FromDate <= DateTime.UtcNow.AddDays(45))
                {
                    _searchStrategy = new LastMinuteHotelsSearch(_integrationServiceFactory);
                }
                else
                {
                    _searchStrategy = new HotelOnlySearch(_integrationServiceFactory);
                }
            }
            else
            {
                _searchStrategy = new HotelAndFlightSearch(_integrationServiceFactory);
            }
        }

        public virtual async Task<BookRes> Book(BookReq bookRequest)
        {
            var searchResult = await Search(bookRequest.SearchReq);
            var desiredOption = searchResult.Options.FirstOrDefault(x => x.OptionCode == bookRequest.OptionCode);

            if (desiredOption == null)
            {
                throw new Exception("That option is no longer available for booking!");
            }

            var bookingType = _searchStrategy.GetType() == typeof(LastMinuteHotelsSearch) 
                ? BookingType.LastMinute : BookingType.Normal;
            var booking = new Booking(bookingType, bookRequest.OptionCode);
            await _bookingRepository.AddBooking(booking);
            var bookRes = _mapper.Map<BookRes>(booking);
            return bookRes;
        }

        public virtual async Task<BookRes?> GetBooking(string bookingCode)
        {
            var booking = await _bookingRepository.GetBooking(bookingCode);
            var bookingRes = booking != null ? _mapper.Map<BookRes>(booking) : null;
            return bookingRes;
        }

        public virtual async Task<CheckStatusRes?> CheckStatus(CheckStatusReq checkStatusReq)
        {
            var booking = await _bookingRepository.GetBooking(checkStatusReq.BookingCode);
            var checkStatusRes = booking != null ? _mapper.Map<CheckStatusRes>(booking) : null;
            return checkStatusRes;
        }
    }
}
