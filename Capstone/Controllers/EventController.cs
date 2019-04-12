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
using System.Net;

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


        public IActionResult Index()
        {
            List<Event> eventList = eventSqlDal.GetAllEvents();


            return View(eventList);
        }


        [HttpGet]
        public IActionResult EventDetail(int id = 1)  //do not change variable name id due to routing
        {
            EventViewModel model = new EventViewModel
            {
                EventItem = eventSqlDal.GetEvent(id)

            };
            model.EventItem.Podcast = podcastDal.GetPodcast(model.EventItem.PodcastID);
            model.EventItem.GenreDescriptionBasedOnPodcast = genreSqlDal.GetGenreDescription(model.EventItem.Podcast.GenreID);
            return View(model);
        }


        [HttpGet]
        public IActionResult EditEvent(int id )
        {

            
                EventViewModel model = new EventViewModel
                {
                    EventItem = eventSqlDal.GetEvent(id),
                };

                if (model.EventItem.PodcastID != null)
                {
                    model.EventItem.Podcast = podcastDal.GetPodcast(model.EventItem.PodcastID);
                    model.EventItem.GenreDescriptionBasedOnPodcast = genreSqlDal.GetGenreDescription(model.EventItem.Podcast.GenreID);
            }

                model.GenreList = GetGenreList();
                model.VenueList = GetVenueList();
                model.TicketList = GetTicketList();
                model.PodcastList = GetPodcastList();



            return View(model);
      

        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditEvent(int id, EventViewModel model)
        {


            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {

                model.EventItem.GenreDescriptionBasedOnPodcast = genreSqlDal.GetGenreDescription(model.EventItem.Podcast.GenreID);
                model.PodcastList = GetPodcastList();
                model.GenreList = GetGenreList();
                model.VenueList = GetVenueList();
                model.TicketList = GetTicketList();
                model.PodcastList = GetPodcastList();

                bool result = eventSqlDal.UpdateEventDetails(model.EventItem);
               
                return RedirectToAction("EventDetail", new { id = model.EventItem.EventID });


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


              return RedirectToAction("EventDetail", new { id = model.EventItem.EventID });

            }
        }

        [HttpGet]
        public IActionResult DeleteEvent(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
            Event eventItem = eventSqlDal.GetEvent((int)id);
            eventItem.GenreDescriptionBasedOnPodcast = genreSqlDal.GetGenreDescription(eventItem.Podcast.GenreID);
            return View(eventItem);

            }
        }

         [HttpPost]
         [ValidateAntiForgeryToken]
         public IActionResult DeleteEvent(int id)
        {
           Event eventItem = eventSqlDal.GetEvent(id);
           eventItem.GenreDescriptionBasedOnPodcast = genreSqlDal.GetGenreDescription(eventItem.Podcast.GenreID);
           eventSqlDal.RemoveEvent(eventItem.EventID);
              
            return RedirectToAction("Index");
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

            List<SelectListItem> selectListVenues = new List<SelectListItem>();

            foreach (Venue venue in venueList)
            {
                selectListVenues.Add(new SelectListItem(venue.DisplayName, venue.VenueID.ToString()));
            }

            return selectListVenues;
        }

        public List<SelectListItem> GetTicketList()
        {
            List<Ticket> ticketList = ticketSqlDal.GetAllTickets();

            List<SelectListItem> selectListTickets = new List<SelectListItem>();

            foreach (Ticket level in ticketList)
            {
                selectListTickets.Add(new SelectListItem(level.TicketType, level.TicketID.ToString()));
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