using BookingSystemAPI.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookingSystemAPI.Controllers
{
    [AllowAnonymous]
    [Route("api/authorize")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthorizationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> Authorize()
        {
            // create a token
            if (string.IsNullOrWhiteSpace(_configuration["Authentication:Secret"]))
            {
                throw new AppException("Please provide a secret for the tokens!");
            }

            var securityKey =
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Authentication:Secret"]));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // claims
            var claimsForToken = new List<Claim>
            {
                new("sub", Guid.NewGuid().ToString()),
            };

            if (string.IsNullOrWhiteSpace(_configuration["Authentication:Issuer"]))
            {
                throw new AppException("Please provide token issuer url!");
            }

            if (string.IsNullOrWhiteSpace(_configuration["Authentication:Audience"]))
            {
                throw new AppException("Please provide token audience url!");
            }

            var jwtSecurityToken = new JwtSecurityToken(
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(10),
                signingCredentials);

            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return Ok(token);
        }
    }
}
