using FutebolTabajaras.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FutebolTabajaras.Web.ViewModels
{
    public class TeamViewModel
    {
        public int ID { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Name { get; set; }

        public List<Player> Players { get; set; }

        public TeamViewModel()
        {

        }

        public TeamViewModel(int id, DateTime createdDate, string name)
        {
            ID = id;
            CreatedDate = createdDate;
            Name = name;
            Players = new List<Player>();
        }

        public TeamViewModel(int id, DateTime createdDate, string name, List<Player> players) : this(id, createdDate, name)
        {
            Players = players;
        }
    }
}