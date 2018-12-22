using System;
using System.Collections.Generic;
using System.Text;

namespace FantasyLeague.Models.Abstract
{
    public abstract class ImageEntity : BaseEntity
    {
        public Guid ImageId { get; set; }
        public Image Image { get; set; }
    }
}
