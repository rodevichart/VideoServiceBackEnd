using System.ComponentModel.DataAnnotations;
using VideoServiceDAL.Interfaces;

namespace VideoServiceDAL.Models
{
    public class Cover: IIdentifier
    {
        public long Id { get; set; }

        [Required]
        [StringLength(255)]
        public string FileName { get; set; }
    }
}