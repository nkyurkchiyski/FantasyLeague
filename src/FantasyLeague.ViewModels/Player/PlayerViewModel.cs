using System;

namespace FantasyLeague.ViewModels.Player
{
    public class PlayerViewModel
    {
        public Guid Id { get; set; }

        public Guid TeamId { get; set; }

        public string Name { get; set; }
    }
}
