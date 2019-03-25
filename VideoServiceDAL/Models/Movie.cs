using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VideoServiceDAL.Interfaces;

namespace VideoServiceDAL.Models
{
    public class Movie : IIdentifier
    {
        public long Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public Genre Genre { get; set; }

        [Required]
        public long GenreId { get; set; }

        public DateTime DateAdded { get; set; }

        public DateTime ReleaseDate { get; set; }

        public byte NumberInStock { get; set; }

        public byte NumberAvailable { get; set; }

        public double Rate { get; set; }

        public List<Rental> Rentals { get; set; }

        public long? CoverId { get; set; }

        public Cover Cover { get; set; }
    }
}
