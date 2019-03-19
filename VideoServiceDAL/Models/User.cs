using System.ComponentModel.DataAnnotations;

namespace VideoServiceDAL.Models
{
    public class User
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