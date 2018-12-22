using FantasyLeague.Models.Abstract;
using System;

namespace FantasyLeague.Models
{
    public class Image : BaseEntity
    {
        public string PublicId { get; set; }

        public string Url { get; set; }

        public Guid ImageEntityId { get; set; }
        public ImageEntity ImageEntity { get; set; }
    }
}
