using FantasyLeague.Models.Abstract;
using System;
using System.ComponentModel.DataAnnotations;

namespace FantasyLeague.Models
{
    public class PlayerImage : Image
    {
        public Guid? PlayerId { get; set; }
        public virtual Player Player { get; set; }
    }
}
