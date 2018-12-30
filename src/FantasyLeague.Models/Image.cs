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
        public string ImageType { get; set; }

        public Guid? TeamId { get; set; }
        public virtual Team Team { get; set; }

        public Guid? PlayerId { get; set; }
        public virtual Player Player { get; set; }
    }
}
