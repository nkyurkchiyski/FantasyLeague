using FantasyLeague.Common.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FantasyLeague.ViewModels.Score
{
    public class ScoreViewModel
    {
        [Required]
        public Guid PlayerId { get; set; }

        [Required]
        [Range(minimum: 0, maximum: int.MaxValue)]
        public int Goals { get; set; }

        [Required]
        [Range(minimum: 0, maximum: int.MaxValue)]
        public int Shots { get; set; }

        [Required]
        [Range(minimum: 0, maximum: int.MaxValue)]
        public int Assists { get; set; }

        [Required]
        [Range(minimum: 0, maximum: int.MaxValue)]
        public int Tackles { get; set; }

        [Required]
        [Range(minimum: 0, maximum: ScoreConstants.MaxPlayedMinutesValue)]
        public int PlayedMinutes { get; set; }
    }
}
