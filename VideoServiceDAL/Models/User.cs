using System.ComponentModel.DataAnnotations;
using VideoServiceDAL.Interfaces;

namespace VideoServiceDAL.Models
{
    public class User : IIdentifier
    {
        public long Id { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Username { get; set; }

        [Required]
        public byte Role { get; set; }
        
        [Required]
        public string Password { get; set; }
    }
}