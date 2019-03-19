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
        private readonly IMapper _mapper;

        public RentalForAdminController(IRentalService rentalService, IMapper mapper)
        {
            _rentalService = rentalService;
            _mapper = mapper;
        }

        public async Task<QueryResultDto<RentalDto>> GetAllRentalsWithUsersMoviesAsync([FromQuery] RentalDataTableSettings model)
        {
            var queryResult = await
                _rentalService.GetAllRentalMoviesWithUsersAsync(model);
            return _mapper.Map<QueryResult<Rental>, QueryResultDto<RentalDto>>(queryResult);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveRentalByIdAsync(long id)
        {
            await _rentalService.RemoveAsync(id);
            return Ok();
        }

    }
}