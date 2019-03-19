using System;

namespace VideoServiceBL.DTOs.MoviesDtos
{
    public class MovieDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long GenreId { get; set; }
        public GenreDto Genre { get; set; }
        public byte NumberInStock { get; set; }
        public double Rate { get; set; }
    }
}
