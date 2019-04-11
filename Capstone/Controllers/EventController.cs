using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Capstone.Models;
using Capstone.DAL.Interfaces;
using Capstone.Models.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Capstone.Controllers
{
    public class EventController : Controller
    {

        private readonly IPodcastSqlDal podcastDal;
        private readonly IEventSqlDal eventSqlDal;
        private readonly IGenreSqlDal genreSqlDal;
        private readonly IVenueSqlDal venueSqlDal;
        private readonly ITicketSqlDal ticketSqlDal;

        public EventController(IPodcastSqlDal podcastSqlDal, IEventSqlDal eventSqlDal, IGenreSqlDal genreSqlDal, IVenueSqlDal venueSqlDal, ITicketSqlDal ticketSqlDal)
        {
            this.podcastDal = podcastSqlDal;
            this.eventSqlDal = eventSqlDal;
            this.genreSqlDal = genreSqlDal;
            this.venueSqlDal = venueSqlDal;
            this.ticketSqlDal = ticketSqlDal;
        }

        [HttpGet]
        public IActionResult EventDetail(int id = 1)  //do not change variable name id due to routing
        {
            EventViewModel model = new EventViewModel
            {
                EventItem = eventSqlDal.GetEvent(id)

            };
            model.EventItem.Podcast = podcastDal.GetPodcast(model.EventItem.PodcastID);
            
            
            
            return View(model);
        }


        [HttpGet]
        public IActionResult EditEvent(int id)
        {
            EventViewModel model = new EventViewModel
            {
                EventItem = eventSqlDal.GetEvent(id)

            };
            model.EventItem.Podcast = podcastDal.GetPodcast(model.EventItem.PodcastID);



            return View(model);

        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditEvent(int id, EventViewModel model)
        {
            try
            {

                    model.EventItem = eventSqlDal.GetEvent(id);
                    eventSqlDal.UpdateEventDetails(model.EventItem);
                   
                    return RedirectToAction("EventDetail", new { id = model.EventItem.EventID });
            }
            catch
            {
                return View();
            }
        }


        [HttpGet]
        public IActionResult SaveEvent()
        {
            Event eventItem = new Event();
            EventViewModel model = new EventViewModel
            {

                EventItem = eventItem,
                VenueList = GetVenueList(),
                GenreList = GetGenreList(),
                TicketList = GetTicketList(),
                PodcastList = GetPodcastList() 

            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveEvent(EventViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
               bool result = eventSqlDal.SaveEvent(model.EventItem);
               
               
                return RedirectToAction("Index", "Home");
            }
        }

        
        public List<SelectListItem> GetGenreList()
        {
            List<Genre> genreList = genreSqlDal.GetAllGenres();

            List<SelectListItem> selectListGenre = new List<SelectListItem>();

            foreach (Genre item in genreList)
            {
                selectListGenre.Add(new SelectListItem(item.GenreName, item.GenreID.ToString()));
            }
            
            return selectListGenre;
        }

        public List<SelectListItem> GetVenueList()
        {
            List<Venue> venueList = venueSqlDal.GetAllVenues();

            List<SelectListItem> selectListVenue = new List<SelectListItem>();

            foreach (Venue item in venueList)
            {
                selectListVenue.Add(new SelectListItem(item.DisplayName, item.VenueID.ToString()));
            }

            return selectListVenue;
        }


        public List<SelectListItem> GetTicketList()
        {
            List<Ticket> ticketList = ticketSqlDal.GetAllTickets();

            List<SelectListItem> selectListTickets = new List<SelectListItem>();

            foreach (Ticket item in ticketList)
            {
                selectListTickets.Add(new SelectListItem(item.TicketType, item.TicketID.ToString()));
            }

            return selectListTickets;
        }

        public List<SelectListItem> GetPodcastList()
        {
            List<Podcast> podcastList = podcastDal.GetAllPodcasts();

            List<SelectListItem> selectListPodcast = new List<SelectListItem>();
            
            foreach (Podcast podcast in podcastList)
            {
                selectListPodcast.Add(new SelectListItem(podcast.Title, podcast.PodcastID.ToString()));
            }

            return selectListPodcast;
        }

        

    }
}