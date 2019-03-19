using System;
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
    [Route("api/rentals")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class RentalController : Controller
    {
        private readonly IRentalService _rentalService;
        private readonly IMapper _mapper;

        public RentalController(IRentalService rentalService, IMapper mapper)
        {
            _rentalService = rentalService;
            _mapper = mapper;
        }

        public async Task<QueryResultDto<RentalDto>> GetRentalsMoviesByUserIdAsync([FromQuery] RentalDataTableSettings model)
        {
            var queryResult = await
                _rentalService.GetAllRentalMoviesAsync(model);
            return _mapper.Map<QueryResult<Rental>, QueryResultDto<RentalDto>>(queryResult);
        }

        [HttpPost]
        public async Task<IActionResult> AddRentalByUserIdAndMovieIdAsync(AddRentalDto model)
        {
            var rental = _mapper.Map<AddRentalDto, Rental>(model);
            rental.DateRented = DateTime.Now;
            await _rentalService.AddAsync(rental);
            return Ok();
        }
    }
}