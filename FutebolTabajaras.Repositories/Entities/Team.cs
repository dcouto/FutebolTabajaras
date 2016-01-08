using System;
using System.Collections.Generic;

namespace FutebolTabajaras.Repositories.Entities
{
    public class Team : BaseEntity
    {
        public string Name { get; set; }

        public virtual List<Player> Players { get; set; }

        public virtual ICollection<Match> Matches { get; set; }

        public Team() : base()
        {

        }

        public Team(int id, DateTime createdDate, string name, List<Player> players) : base(id, createdDate)
        {
            Players = players;
        }
    }
}
