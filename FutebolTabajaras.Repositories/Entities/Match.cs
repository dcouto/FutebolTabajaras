using System;

namespace FutebolTabajaras.Repositories.Entities
{
    public class Match : BaseEntity
    {
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }

        public Match() : base()
        {

        }

        public Match(int id, DateTime createdDate, Team homeTeam, Team awayTeam) : base(id, createdDate)
        {
            HomeTeam = homeTeam;
            AwayTeam = awayTeam;
        }
    }
}
