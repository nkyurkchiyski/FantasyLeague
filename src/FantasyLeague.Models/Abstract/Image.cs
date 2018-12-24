using System.ComponentModel.DataAnnotations;

namespace FantasyLeague.Models.Abstract
{
    public abstract class Image : BaseEntity
    {
        [Required]
        public string PublicId { get; set; }

        [Required]
        public string Url { get; set; }

        [Required]
        public string ImageType { get; set; }

    }
}
