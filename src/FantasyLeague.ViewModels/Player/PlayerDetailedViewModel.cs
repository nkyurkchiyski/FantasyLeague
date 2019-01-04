using FantasyLeague.Common.Constants;
using FantasyLeague.Models.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace FantasyLeague.ViewModels.Player
{
    public class PlayerDetailedViewModel : PlayerViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        public string Nationality { get; set; }

        [Required]
        [Range(GlobalConstants.MinPlayerPrice, GlobalConstants.MaxPlayerPrice)]
        public double Price { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public PlayerPosition Position { get; set; }
        
        public IFormFile Image { get; set; }
    }
}
