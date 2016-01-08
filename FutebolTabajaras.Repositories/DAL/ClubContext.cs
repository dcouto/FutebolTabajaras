using FutebolTabajaras.Repositories.Entities;
using System.Data.Entity;

namespace FutebolTabajaras.Repositories.DAL
{
    public class ClubContext : DbContext
    {
        public ClubContext() : base("ClubContext3")
        {

        }

        public DbSet<Accomplishment> Accomplishments { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }
    }
}
