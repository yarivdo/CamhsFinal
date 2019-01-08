using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CamhsFinal.DAL;
using CamhsFinal.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CamhsFinal.Controllers
{
    
    
    //TODO: make sure hat when Deferred or Closed is set , the isOpen is set to false
    //TODO: make a tab for MDT where all the episodes hich are not closed are shown, but only for the MDT. The rest (More info and Wait list) should be on other tabs

    public class EpisodesController : Controller
    {
        private readonly CamhsContext _context;

        public EpisodesController(CamhsContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> FindClient()
        {
            return View();
        }

        [HttpPost, ActionName("FindClient")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FindClient(string NHI)
        {
            Debug.WriteLine(NHI);
           
            try
            {
                var episodes = _context.Episodes.Include(c => c.Referral);

                //Find the client by NHI
                var query = (from a in _context.Clients
                    where a.NHI.Contains(NHI)
                    select a).First();

                //Now let's trace the related episodes by the client's ID
                var episodesQuery = (from a in episodes where a.ClientID.Equals(query.ClientID) select a)
                    .ToList();


                string toPass = query.NHI + " " + query.First + " " + query.Last;
                ViewBag.Name = toPass;
                ViewBag.ClientID = query.ClientID;

                return View("ClientResults", episodesQuery);

            }
            catch
            {
                return View("ClientNotFound");

            }
        }

        public async Task<IActionResult> MDT(int id) // The returned ID is the referral's
        {
            // based on the ReferralId I can populate the Episode object
            var episode = (from o in _context.Episodes
                          where o.ReferralID == id
                          select o).Single();
            episode.Referral = await _context.Referrals.FindAsync(id); 
            episode.Client = _context.Clients.Find(episode.ClientID);

            string nameToPass = episode.Client.NHI + " " + episode.Client.First + " " + episode.Client.Last;
            string detailsToPass = episode.Client.Age + " year old, based in " + episode.Client.Location;

            ViewBag.Name = nameToPass;
            ViewBag.detailsToPass = detailsToPass;

            return View(episode);
        }

        [HttpPost, ActionName("MDT")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MDT(Episode episode)
        {
            if (episode.Outcome == "3" || episode.Outcome == "4")  // Close the episode if outcome is Deferred or Closed
            {
                episode.isOpen = false;
            }
            try
            {
                episode.Referral = _context.Referrals.Find(episode.ReferralID);
                _context.Update(episode);
                await _context.SaveChangesAsync();
                Debug.WriteLine("ClientID after= {0}", episode.ClientID);
                return RedirectToAction("MdtList");
            }
            catch
            {
                return Content("Something went wrong");
            }
        }

        public async Task<IActionResult> newReferral(int id)
        {
            // We need to (a) create a new episode, then atach to it a client and a referral entity and then send it to the view
            Referral referral = new Referral(); // no ID yet
            Client client = _context.Clients.Find(id);
            Episode episode = new Episode {Client = client, Referral = referral};
            episode.isOpen = true;

            string nameToPass = episode.Client.NHI + " " + episode.Client.First + " " + episode.Client.Last;
            ViewBag.Name = nameToPass;

            _context.Add(episode);
            await _context.SaveChangesAsync(); //will add all of the necessary id

            return View(episode);
        }


        [HttpPost]
        public async Task<IActionResult> newReferral(Episode episode)
        {
            Client client = _context.Clients.Find(episode.ClientID);
            episode.Client = client;

            episode.Outcome = "0"; // Makes sure it will go to the MDT list

            if (ModelState.IsValid)
            {
                _context.Update(episode); // This is an update on a previously created episode
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Clients");
            }
            else
            {
                return Content("something went wrong");
            }

        }

        public IActionResult MdtList()
        {
            var episodes = _context.Episodes.Include(c => c.Client);
            var passList = (from m in episodes where m.Outcome == "0" select m).ToList();

            //var passList = (from m in _context.Episodes where m.Outcome == "0" select m).ToList();
            //foreach (var person in passList)
            //{
            //    person.Client = _context.Clients.Find(person.ClientID);
            //}
            return View(passList);
        }

        public IActionResult WaitList()
        {
            var episodes = _context.Episodes.Include(c => c.Client).Include(d => d.Referral);
            var passList = (from m in episodes where m.Outcome == "1" select m).ToList();

            return View(passList);
        }

        public IActionResult MoreInfoList()
        {
            var episodes = _context.Episodes.Include(c => c.Client).Include(d => d.Referral);
            var passList = (from m in episodes where m.Outcome == "2" select m).ToList();

            return View(passList);

        }
    }
}