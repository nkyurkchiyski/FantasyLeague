using FantasyLeague.Models.Enums;
using Microsoft.AspNetCore.Http;

namespace FantasyLeague.ViewModels.Player
{
    public class PlayerDetailedViewModel : PlayerViewModel
    {
        public string Nationality { get; set; }

        public double Price { get; set; }

        public PlayerPosition Position { get; set; }

        public IFormFile Image { get; set; }
    }
}
