using System.ComponentModel.DataAnnotations;

namespace VideoServiceBL.DTOs.MoviesDtos
{
    public class MovieDataDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int GenreId { get; set; }

        [Required]
        public byte NumberInStock { get; set; }

        [Required]
        public double Rate { get; set; }
    }
}