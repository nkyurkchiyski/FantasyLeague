using FantasyLeague.Models.Abstract;
using System;
using System.ComponentModel.DataAnnotations;

namespace FantasyLeague.Models
{
    public class TeamImage : Image
    {
        public Guid? TeamId { get; set; }
        public virtual Team Team { get; set; }
    }
}
