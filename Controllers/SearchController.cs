using BookingSystemAPI.Services;
using BookingSystemAPI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystemAPI.Controllers
{
    [Route("api/search")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class SearchController : ControllerBase
    {
        private readonly IManagerService _managerService;

        public SearchController(IManagerService managerService)
        {
            _managerService = managerService;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<ActionResult<SearchRes>> Search([FromQuery] SearchReq searchReq)
        {
            var searchResponse = await _managerService.Search(searchReq);

            if (!searchResponse.Options.Any())
            {
                return NotFound();
            }

            return Ok(searchResponse);
        }
    }
}
