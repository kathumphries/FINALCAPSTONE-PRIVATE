﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.DAL.Interfaces;
using Capstone.Models;
using Capstone.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Capstone.Controllers
{
    public class SearchController : Controller
    {

        private readonly IPodcastSqlDal podcastDal;
        private readonly IEventSqlDal eventSqlDal;
        private readonly IGenreSqlDal genreSqlDal;
        private readonly IVenueSqlDal venueSqlDal;
        private readonly ITicketSqlDal ticketSqlDal;

        public SearchController(IPodcastSqlDal podcastSqlDal, IEventSqlDal eventSqlDal, IGenreSqlDal genreSqlDal, IVenueSqlDal venueSqlDal, ITicketSqlDal ticketSqlDal)
        {
            this.podcastDal = podcastSqlDal;
            this.eventSqlDal = eventSqlDal;
            this.genreSqlDal = genreSqlDal;
            this.venueSqlDal = venueSqlDal;
            this.ticketSqlDal = ticketSqlDal;
        }

        // GET: /<controller>/
        [HttpGet]
        public IActionResult Index()
        {


            Event eventItem = new Event();
            eventItem.Podcast = new Podcast();
            SearchViewModel model = new SearchViewModel
            {
                Event = eventItem,
                VenueList = GetVenueList(),
                GenreList = GetGenreList(),
                TicketList = GetTicketList(),
                PodcastList = GetPodcastList(),
                TimeOfDayList = GetTimeOfDay()
            };

            model.EventList = eventSqlDal.GetAllEvents();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SearchEvents(SearchViewModel model)
        {
            model.EventList = eventSqlDal.GetEventsByGenre(model.Event);
            model.EventList.AddRange(eventSqlDal.GetEventsByLocation(model.Event));
            model.EventList.AddRange(eventSqlDal.GetEventsByTicket(model.Event));
            model.EventList.AddRange(eventSqlDal.GetEventsByTimeOfDay(model.TimeOfDayString));

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

        public List<SelectListItem> GetTimeOfDay()
        {
            string morning = "Morning";
            string afternoon = "Afternoon";
            string evening = "Evening";

            List<SelectListItem> selectListTime = new List<SelectListItem>
            {
                 new SelectListItem((morning), ("1")),
                 new SelectListItem((afternoon), ("2")),
                 new SelectListItem((evening), ("3"))
            };

            return selectListTime;
        }
    }
}
