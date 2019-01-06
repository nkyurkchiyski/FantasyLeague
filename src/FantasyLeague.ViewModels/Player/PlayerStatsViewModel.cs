﻿using FantasyLeague.Common.Constants;
using FantasyLeague.Models.Enums;

namespace FantasyLeague.ViewModels.Player
{
    public class PlayerStatsViewModel : PlayerViewModel
    {
        public int Goals { get; set; }

        public int Assists { get; set; }

        public int TotalPoints { get; set; }

        public string PlayerImage { get; set; }

        public string Nationality { get; set; }

        public PlayerPosition Position { get; set; }

        public bool Active { get; set; }

        public double Price { get; set; }
    }
}