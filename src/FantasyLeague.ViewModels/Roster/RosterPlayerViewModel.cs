using FantasyLeague.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace FantasyLeague.ViewModels.Roster
{
    public class RosterPlayerViewModel
    {
        public Guid Id { get; set; }

        public Guid PlayerId { get; set; }

        public Guid RosterId { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public int CurrentPoints { get; set; }
        
        public bool Selected { get; set; }

        public PlayerPosition Position { get; set; }
    }
}
