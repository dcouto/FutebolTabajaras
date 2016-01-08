using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FutebolTabajaras.Repositories.DAL;
using FutebolTabajaras.Repositories.Entities;
using FutebolTabajaras.Web.ViewModels;

namespace FutebolTabajaras.Web.Controllers
{
    public class TeamsController : Controller
    {
        private ClubContext db = new ClubContext();

        // GET: Teams
        public ActionResult Index()
        {
            var teamVMs = db.Teams.OrderByDescending(i => i.CreatedDate).ToList()
                .Select(i => 
                new TeamViewModel(
                    i.ID, 
                    i.CreatedDate, 
                    i.Name, 
                    i.Players.Select(p => new Models.Player(p.ID, p.FirstName, p.LastName)).ToList()));

            return View(teamVMs);
        }

        // GET: Teams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        // GET: Teams/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Teams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name")] Team team)
        {
            if (ModelState.IsValid)
            {
                team.CreatedDate = DateTime.Now;

                db.Teams.Add(team);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(team);
        }

        // GET: Teams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Team dalTeam = db.Teams.Find(id);

            if (dalTeam == null)
            {
                return HttpNotFound();
            }

            var teamVM = new TeamViewModel(dalTeam.ID, dalTeam.CreatedDate, dalTeam.Name);

            var allDalPlayers = db.Players.ToList();

            var allDalTeamPlayers = dalTeam.Players != null ? dalTeam.Players : Enumerable.Empty<Player>();
            var newDalTeamPlayers = new List<Models.Player>();

            foreach(var dalPlayer in allDalPlayers)
            {
                var webPlayer = new Models.Player(dalPlayer.ID, dalPlayer.FirstName, dalPlayer.LastName);

                if (allDalTeamPlayers.Any(i => i.ID == dalPlayer.ID))
                {
                    webPlayer.Selected = true;
                }

                newDalTeamPlayers.Add(webPlayer);
            }

            teamVM.Players = newDalTeamPlayers;

            return View(teamVM);
        }

        // POST: Teams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Players")] TeamViewModel teamVM)
        {
            if (ModelState.IsValid)
            {
                var dalTeam = db.Teams.FirstOrDefault(i => i.ID == teamVM.ID);

                if(dalTeam == null)
                {
                    return View(teamVM);
                }

                dalTeam.Name = teamVM.Name;

                var allDalPlayers = db.Players.ToList();

                // with MVC binding, if a checkbox is checked, that object will get bound but if it's not checked
                // it won't get bound as "not selected", it simply won't get bound at all
                // so we need to build a dictionary of all possible players and whether or not they are selected
                // we'll default the bool to "false" meaning, no players were selected
                var allDalPlayersDictionary = allDalPlayers.ToDictionary(i => i, i => false);

                var currentWebTeamPlayers = teamVM.Players != null ? teamVM.Players.ToList() : new List<Models.Player>();

                // loop over whatever players were checked off on the front-end and set the players in the dictionary to true
                foreach (var webPlayer in currentWebTeamPlayers)
                {
                    var matchingDalPlayer = allDalPlayersDictionary.FirstOrDefault(i => i.Key.ID == webPlayer.ID);

                    if(!matchingDalPlayer.Equals(default(KeyValuePair<Models.Player, bool>)) && matchingDalPlayer.Key != null)
                    {
                        allDalPlayersDictionary[matchingDalPlayer.Key] = webPlayer.Selected;
                    }
                }

                // loop over the updated dictionary and update the dalTeam entity
                foreach(var dalPlayer in allDalPlayersDictionary)
                {
                    // the team currently contains this player
                    if(dalTeam.Players.Find(i => i.ID == dalPlayer.Key.ID) != null)
                    {
                        // but the player was unchecked on the front-end
                        if (!dalPlayer.Value)
                        {
                            dalTeam.Players.Remove(dalPlayer.Key);
                        }
                    }
                    // the team current DOES NOT contain this player
                    else
                    {
                        // but the player was checked on the front-end
                        if (dalPlayer.Value)
                        {
                            dalTeam.Players.Add(dalPlayer.Key);
                        }
                    }
                }

                db.Entry(dalTeam).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(teamVM);
        }

        // GET: Teams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Team team = db.Teams.Find(id);
            db.Teams.Remove(team);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
