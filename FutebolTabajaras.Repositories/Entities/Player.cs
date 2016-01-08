using System;
using System.Collections.Generic;

namespace FutebolTabajaras.Repositories.Entities
{
    public class Player : BaseEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<Accomplishment> Accomplishments { get; set; }

        public virtual ICollection<Team> Teams { get; set; }

        public Player() : base()
        {

        }

        public Player(int id, DateTime createdDate, string firstName, string lastName, List<Accomplishment> accomplishments) : base(id, createdDate)
        {
            FirstName = firstName;
            LastName = lastName;
            Accomplishments = accomplishments;
        }
    }
}
