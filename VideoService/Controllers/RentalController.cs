using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VideoServiceBL.DTOs.MoviesDtos;
using VideoServiceBL.DTOs.RentalsDtos;
using VideoServiceBL.Services.Interfaces;

namespace VideoService.Controllers
{
    [Route("api/rentals")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class RentalController : Controller
    {
        private readonly IRentalService _rentalService;

        public RentalController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }

        public async Task<QueryResultDto<RentalDto>> GetRentalsMoviesByUserIdAsync([FromQuery] RentalDataTableSettings model)
        {
           return await _rentalService.GetAllRentalMoviesAsync(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddRentalByUserIdAndMovieIdAsync(AddRentalDto model)
        {
            await _rentalService.AddRentalByUserIdAndMovieIdAsync(model);
            return Ok();
        }
    }
}