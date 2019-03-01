using System.ComponentModel.DataAnnotations;

namespace VideoServiceDAL.Models
{
    public class User
    {
        public int Id { get; set; }

        [StringLength(255)]
        public string FirstName { get; set; }

        [StringLength(255)]
        public string LastName { get; set; }

        [Required]
        [StringLength(255)]
        public string Username { get; set; }

        [Required]
        public byte Role { get; set; }
        
        [Required]
        public string Password { get; set; }
    }
}