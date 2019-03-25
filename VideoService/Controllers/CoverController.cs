using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VideoServiceBL.Services.Interfaces;
using VideoServiceDAL.Models;

namespace VideoService.Controllers
{
    [ApiController]
//    [Route("/api/covers")]
    [Route("/api/movies/{movieId}/covers")]
    public class CoverController : Controller
    {
        private readonly ICoverService _coverService;

        public CoverController(ICoverService coverService)
        {
            _coverService = coverService;
        }

        [HttpPost]
        public async Task<IActionResult> Upload(long movieId, IFormFile file)
        {
            var result = await _coverService.SaveImage(movieId, file);

            return Ok(result);
        }
    }
}