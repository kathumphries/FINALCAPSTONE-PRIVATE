using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Capstone.Models;
using Capstone.DAL.Interfaces;

namespace Capstone.Controllers
{
    public class EventController : Controller
    {
        private  IPodcastSqlDal podcastDal;
        private  IEventSqlDal eventSqlDal;
        private  IGenreSqlDal genreSqlDal;
        private IVenueSqlDal venueSqlDal;

        public EventController(IPodcastSqlDal podcastSqlDal, IEventSqlDal eventSqlDal, IGenreSqlDal genreSqlDal, IVenueSqlDal venueSqlDal)
        {
            this.podcastDal = podcastSqlDal;
            this.eventSqlDal = eventSqlDal;
            this.genreSqlDal = genreSqlDal;
            this.venueSqlDal = venueSqlDal;
        }

        [HttpGet]
        public IActionResult EventDetail()
        {
            EventViewModel model = new EventViewModel();

            List<Event> events = eventSqlDal.GetAllEvents();
            return View(events);
        }

        [HttpGet]
        public IActionResult EditEvent()
        {
            Event eventItem = new Event();
            EventViewModel model = new EventViewModel
            {
              EventItem = eventItem,           
              VenueList = GetVenueList(),
              GenreList = GetGenreList()
            };

            return View(model);
        }

        public List<Genre> GetGenreList()
        {
            List<Genre> genreList = genreSqlDal.GetAllGenres();
            return genreList;
        }

        public List<Venue> GetVenueList()
        {
            List<Venue> venueList = venueSqlDal.GetAllVenues();
            return venueList;
        }

        //[HttpPost]
        //public IActionResult EditEvent(EventViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }
        //    else
        //    {
        //        //bool result = eventSqlDal.AddEventDetail();
        //        //TODO : account for false
        //        // return RedirectToAction(nameof());
        //    }
        //}
    }
}