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
using Capstone.Providers.Auth;
using System.Text;

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

        [AuthorizationFilter("1")]
        public IActionResult Index()
        {
            List<Event> eventList = eventSqlDal.GetAllEvents();

            eventList.ForEach(eventItem => PopulateEventDetails(eventItem.EventID));            

            return View(eventList);
        }


        public IActionResult EventCalender()
        {
            EventViewModel eventViewModel = GetEventCalenderDay();

            return View(eventViewModel);
        }

        [HttpGet]
        public IActionResult EventDetail(int id = 1)  //do not change variable name id due to routing
        {
            EventViewModel model = new EventViewModel
            {
                EventItem = PopulateEventDetails(id),

            };                   

            return View(model);
        }



        //CRUD

        [HttpGet]
        [AuthorizationFilter("1")]
        public IActionResult EditEvent(int id)
        {

            EventViewModel model = new EventViewModel
            {
                EventItem = PopulateEventDetails(id),
            };

            model.GenreList = GetGenreList();
            model.VenueList = GetVenueList();
            model.TicketList = GetTicketList();
            model.PodcastList = GetPodcastList();

            return View(model);

        }

        [AuthorizationFilter("1")]  //admin only
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
                model.EventItem = PopulateEventDetails(id);
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
        [AuthorizationFilter("1")]  //admin only
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
        [AuthorizationFilter("1")]  //admin only
        public IActionResult SaveEvent(EventViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                bool result = eventSqlDal.SaveEvent(model.EventItem);


                return RedirectToAction("Index");

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

        private EventViewModel GetEventCalenderDay()
        {
            EventViewModel eventViewModel = new EventViewModel();
            List<Event> eventList = new List<Event>();
            List<Event> comingEvents = new List<Event>();
            List<Event> pastEvents = new List<Event>();
            Event eventItem = new Event();

            eventList = eventSqlDal.GetAllEvents();

            foreach (Event item in eventList)
            {
                if (item.Beginning.Date >= DateTime.Today.Date)
                {
                    comingEvents.Add(item);
                }
                else if (item.Beginning.Date < DateTime.Today.Date)
                {
                    pastEvents.Add(item);
                }
            }

            eventViewModel.ComingEvents.AddRange(comingEvents);
            eventViewModel.PastEvents.AddRange(pastEvents);

            return eventViewModel;
        }




        public ActionResult EncodeICal(int eventID)
        {
            Event eventItem = eventSqlDal.GetEvent(eventID);
            eventItem.Venue = venueSqlDal.GetVenue(eventItem.VenueID);

            string downloadFileName = "PodfestMidWestEvent.ics";
            ICalendar ical = new ICalendar();
            var icalStringbuilder = new StringBuilder();

            icalStringbuilder.AppendLine("BEGIN:VCALENDAR");
            icalStringbuilder.AppendLine("PRODID:-//PodfestMidwest//EN");
            icalStringbuilder.AppendLine("VERSION:2.0");
            icalStringbuilder.AppendLine("BEGIN:VEVENT");
            icalStringbuilder.AppendLine("SUMMARY;LANGUAGE=en-us:" + eventItem.EventName);
            icalStringbuilder.AppendLine("CLASS:PUBLIC");
            icalStringbuilder.AppendLine(string.Format("CREATED:{0:yyyyMMddTHHmmssZ}", DateTime.UtcNow));
            icalStringbuilder.AppendLine("DESCRIPTION:" + eventItem.DescriptionCopy);
            icalStringbuilder.AppendLine(string.Format("DTSTART:{0:yyyyMMddTHHmmssZ}", eventItem.Beginning));
            icalStringbuilder.AppendLine(string.Format("DTEND:{0:yyyyMMddTHHmmssZ}", eventItem.Ending));
            icalStringbuilder.AppendLine("SEQUENCE:0");
            icalStringbuilder.AppendLine("UID:" + Guid.NewGuid());
            icalStringbuilder.AppendLine(
                string.Format(
                    "LOCATION:{0}\\, {1}\\, {2}\\, {3} {4}",
                    eventItem.Venue.DisplayName,
                    eventItem.Venue.Address1,
                    eventItem.Venue.City,
                    eventItem.Venue.State,
                    eventItem.Venue.ZipCode).Trim());
            icalStringbuilder.AppendLine("END:VEVENT");
            icalStringbuilder.AppendLine("END:VCALENDAR");

            var bytes = Encoding.UTF8.GetBytes(icalStringbuilder.ToString());

            return this.File(bytes, "text/calendar", downloadFileName);
        }


    //    public string GenerateGoogleCal(int eventID)
    //    {
    //        Event eventItem = eventSqlDal.GetEvent(eventID);
    //        eventItem.Podcast = podcastDal.GetPodcast(eventItem.PodcastID);
    //        eventItem.Venue = venueSqlDal.GetVenue(eventItem.VenueID);
    //        eventItem.Podcast.Genre = genreSqlDal.GetGenre(eventItem.Podcast.GenreID);
    //        eventItem.Ticket = ticketSqlDal.GetTicket(eventItem.TicketLevel);
    //        eventItem.GoogleURL = GenerateGoogleCal(eventItem.EventID);

    //        string googleURLstart = "https://calendar.google.com/calendar/render?action=TEMPLATE&text=";
    //        string eventName = eventItem.EventName;
    //        string googledates = @"&dates=";
    //        string beginning = eventItem.Beginning.ToString("yyyyMMddTHHmmssZ");
    //        string googelSlash = @"/";
    //        string ending = eventItem.Ending.ToString("yyyyMMddTHHmmssZ");
    //        string googleDetails = @"&details = For + details,+link + here:+";
    //        string url = (eventItem.Podcast.URL == null) ? "" : eventItem.Podcast.URL;
    //        string googleLocation = "&location = ";
    //        string venue = System.Web.HttpUtility.UrlEncode(eventItem.Venue.DisplayName + "," +
    //                eventItem.Venue.Address1 + "," +
    //                eventItem.Venue.City + "," +
    //                eventItem.Venue.State + "," +
    //                eventItem.Venue.ZipCode);
    //        string googleOut = @"& sf = true & output = xml";
    //        string googleURL = googleURLstart + eventName + googledates + beginning + googelSlash + ending + googleDetails + url + googleLocation + venue + googleOut;
    //        return Uri.EscapeUriString(googleURL);

    //    }

    //}

    //https://calendar.google.com/calendar/render?action=TEMPLATE&text=Birthday&dates=20180201T090000/20180201T180000&sprop=&sprop=name:
    //    https://www.google.com/calendar   +301+Park+Ave+,+New+York,+NY+10022&sf=true&output=xml


        private Event PopulateEventDetails(int id)
        {
            Event eventItem = eventSqlDal.GetEvent(id);

           

            if (eventItem.PodcastID != null)
            {
            eventItem.Podcast = podcastDal.GetPodcast(eventItem.PodcastID);
            eventItem.Podcast.Genre = genreSqlDal.GetGenre(eventItem.Podcast.GenreID);
            }

            if (eventItem.VenueID != null)
            {
                eventItem.Venue = venueSqlDal.GetVenue(eventItem.VenueID);
            }

            if (eventItem.TicketLevel != null)
            {
                eventItem.Ticket = ticketSqlDal.GetTicket(eventItem.TicketLevel);
            }
            
            return eventItem;
        }



}

}