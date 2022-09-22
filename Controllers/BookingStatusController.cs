using BookingSystemAPI.Services;
using BookingSystemAPI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystemAPI.Controllers
{
    [Route("api/status")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class BookingStatusController : ControllerBase
    {
        private readonly IManagerService _managerService;

        public BookingStatusController(IManagerService managerService)
        {
            _managerService = managerService;
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet(Name = nameof(CheckStatus))]
        public async Task<ActionResult<CheckStatusRes>> CheckStatus([FromQuery] CheckStatusReq checkStatusReq)
        {
            var bookingStatus = await _managerService.CheckStatus(checkStatusReq);
            if (bookingStatus == null)
            {
                return NotFound();
            }

            return Ok(bookingStatus);
        }
    }
}
