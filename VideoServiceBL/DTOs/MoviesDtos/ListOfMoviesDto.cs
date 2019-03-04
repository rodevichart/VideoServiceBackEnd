﻿namespace VideoServiceBL.DTOs.MoviesDtos
{
    public class ListOfMoviesDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public GenreDto Genre { get; set; }
        public byte NumberInStock { get; set; }
        public double Rate { get; set; }
    }
}