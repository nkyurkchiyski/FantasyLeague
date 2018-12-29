using System;
using System.ComponentModel.DataAnnotations;

namespace FantasyLeague.ViewModels.Matchday
{
    public class MatchdayViewModel
    {
        public Guid Id { get; set; }
        
        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }
        
        [Range(minimum: 0, maximum: int.MaxValue)]
        public int Week { get; set; }
        
        public string Status { get; set; }
    }
}
