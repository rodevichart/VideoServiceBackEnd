using System;
using VideoServiceBL.DTOs.MoviesDtos;
using VideoServiceBL.DTOs.UsersDtos;
using VideoServiceDAL.Models;

namespace VideoServiceBL.DTOs.RentalsDtos
{
    public class RentalDto
    {
        public int Id { get; set; }
        public UserDto User { get; set; }
        public MovieDto Movie { get; set; }
        public DateTime DateRented { get; set; }
        public DateTime? DateReturned { get; set; }
    }
}