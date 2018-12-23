using FantasyLeague.Models.Abstract;
using System;
using System.ComponentModel.DataAnnotations;

namespace FantasyLeague.Models
{
    public class Score : BaseEntity
    {
        private const double MaxRatingValue = 10;

        [Required]
        public Guid PlayerId { get; set; }
        public virtual Player Player { get; set; }

        [Required]
        public Guid FixtureId { get; set; }
        public virtual Fixture Fixture { get; set; }

        [Required]
        [Range(minimum: 0, maximum: MaxRatingValue)]
        public double Rating { get; set; }

    }
}
