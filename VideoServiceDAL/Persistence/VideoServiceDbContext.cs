using Microsoft.EntityFrameworkCore;
using VideoServiceDAL.Models;

namespace VideoServiceDAL.Persistence
{
    public class VideoServiceDbContext : DbContext
    {
        public VideoServiceDbContext(DbContextOptions<VideoServiceDbContext> options)
            :base(options)
        {
            
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Cover> Covers { get; set; }
        
    }
}
