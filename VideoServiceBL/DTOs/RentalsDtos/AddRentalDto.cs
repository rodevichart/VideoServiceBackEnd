using System.ComponentModel.DataAnnotations;

namespace VideoServiceBL.DTOs.RentalsDtos
{
    public class AddRentalDto
    {
        [Required]
        public long UserId { get; set; }
        [Required]
        public long MovieId { get; set; }
    }
}