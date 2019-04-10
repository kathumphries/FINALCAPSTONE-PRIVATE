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

            //List<Event> events = eventSqlDal.GetEvent();
            return View();
        }

        [HttpGet]
        public IActionResult SaveEvent()
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
                selectListVenue.Add(new SelectListItem(item.DisplayName, item.VenueId.ToString()));
            }

            return selectListVenue;
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
               
               
                return RedirectToAction("Home", "Index");
            }
        }
    }
}