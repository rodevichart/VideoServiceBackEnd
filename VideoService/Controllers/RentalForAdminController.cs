using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VideoServiceBL.DTOs.MoviesDtos;
using VideoServiceBL.DTOs.RentalsDtos;
using VideoServiceBL.Services.Interfaces;
using VideoServiceDAL.Models;

namespace VideoService.Controllers
{
    [Route("api/rentals/admin")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class RentalForAdminController : Controller
    {
        private readonly IRentalService _rentalService;

        public RentalForAdminController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }

        public async Task<QueryResultDto<RentalDto>> GetAllRentalsWithUsersMoviesAsync([FromQuery] RentalDataTableSettings model)
        {
           return await _rentalService.GetAllRentalMoviesWithUsersAsync(model);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveRentalByIdAsync(long id)
        {
            await _rentalService.RemoveAsync(id);
            return Ok();
        }

    }
}