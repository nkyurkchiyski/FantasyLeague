using FantasyLeague.Models.Abstract;
using System;
using System.ComponentModel.DataAnnotations;

namespace FantasyLeague.Models
{
    public class Image : BaseEntity
    {
        [Required]
        public string PublicId { get; set; }

        [Required]
        public string Url { get; set; }

        [Required]
        public Guid PlayerId { get; set; }
        public virtual Player Player { get; set; }
    }
}
