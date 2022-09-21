using BookingSystemAPI.Services;
using BookingSystemAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystemAPI.Controllers
{
    [Route("api/book")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class BookingController : ControllerBase
    {
        private readonly IManagerService _managerService;

        public BookingController(IManagerService managerService)
        {
            _managerService = managerService;
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        public async Task<ActionResult<BookRes>> AddBooking(BookReq bookReq)
        {
            var bookingResponse = await _managerService.Book(bookReq);
            return CreatedAtRoute(nameof(BookingStatusController.CheckStatus), new { controller = "BookingStatus", bookingCode = bookingResponse.BookingCode }, bookingResponse);
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{bookingCode:regex(\\b([[0-9A-Za-z]]{{6}})\\b)}", Name = nameof(GetBooking))]
        public async Task<ActionResult<BookRes>> GetBooking(string bookingCode)
        {
            var booking = await _managerService.GetBooking(bookingCode);
            if (booking == null)
            {
                return NotFound();
            }

            return Ok(booking);
        }
    }
}
