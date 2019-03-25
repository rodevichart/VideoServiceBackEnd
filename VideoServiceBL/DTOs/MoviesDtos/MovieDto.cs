using System.Collections;
using System.Collections.Generic;

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
        public long CoverId { get; set; }
        public CoverDto Cover { get; set; }
    }
}
