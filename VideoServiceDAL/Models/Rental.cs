using System;
using VideoServiceDAL.Interfaces;

namespace VideoServiceDAL.Models
{
    public class Rental : IIdentifier
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
        public long MovieId { get; set; }
        public Movie Movie { get; set; }
        public DateTime DateRented { get; set; }
        public DateTime? DateReturned { get; set; }
    }
}