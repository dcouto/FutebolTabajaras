using System;
using System.Collections.Generic;

namespace FutebolTabajaras.Repositories.Entities
{
    public class Accomplishment : BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<Player> Players { get; set; }

        public Accomplishment() : base()
        {

        }

        public Accomplishment(int id, DateTime createdDate, string name) : base (id, createdDate)
        {
            Name = name;
        }
    }
}
