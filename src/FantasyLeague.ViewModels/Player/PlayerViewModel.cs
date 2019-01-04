using System;
using System.ComponentModel.DataAnnotations;

namespace FantasyLeague.ViewModels.Player
{
    public class PlayerViewModel
    {
        public Guid Id { get; set; }

        public Guid TeamId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }
    }
}
