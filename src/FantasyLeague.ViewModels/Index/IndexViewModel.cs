using FantasyLeague.Models.Enums;
using FantasyLeague.ViewModels.User;
using System;

namespace FantasyLeague.ViewModels.Index
{
    public class IndexViewModel
    {
        public UserViewModel User { get; set; }

        public int MarchdayWeek { get; set; }
        
        public Guid MarchdayId { get; set; }

        public TransferWindowStatus TransferWindowStatus { get; set; }
    }
}
