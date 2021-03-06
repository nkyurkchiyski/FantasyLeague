﻿using FantasyLeague.Common.Constants;
using FantasyLeague.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace FantasyLeague.ViewModels.Score
{
    public class ScoreViewModel
    {
        [Required]
        public Guid PlayerId { get; set; }

        public Guid TeamId { get; set; }

        public PlayerPosition Position { get; set; }

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
        [Range(minimum: 0, maximum: ScoreConstants.MaxYellowCards)]
        public int YellowCards { get; set; }

        [Required]
        [Range(minimum: 0, maximum: ScoreConstants.MaxRedsCards)]
        public int RedCards { get; set; }

        [Required]
        [Range(minimum: 0, maximum: ScoreConstants.MaxPlayedMinutesValue)]
        public int PlayedMinutes { get; set; }
    }
}
