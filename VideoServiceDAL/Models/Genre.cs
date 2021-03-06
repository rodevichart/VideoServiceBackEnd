﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VideoServiceDAL.Interfaces;

namespace VideoServiceDAL.Models
{
    [Table("Genres")]
    public class Genre : IIdentifier
    {
        public long Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }
    }
}
