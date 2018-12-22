using System;
using System.Collections.Generic;
using System.Text;

namespace FantasyLeague.Models.Abstract
{
    public abstract class BaseEntity
    {
        public BaseEntity()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
    }
}
