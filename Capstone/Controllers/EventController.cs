
using Capstone.DAL.Interfaces;
using Capstone.Models;
using Capstone.Models.ViewModel;
using Capstone.Providers.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Capstone.Controllers
{
    public class EventController : Controller
    {
        private readonly SmtpClient smtpClient;
        private readonly IPodcastSqlDal podcastDal; 
        private readonly IEventSqlDal eventSqlDal;
        private readonly IGenreSqlDal genreSqlDal;
        private readonly IVenueSqlDal venueSqlDal;
        private readonly ITicketSqlDal ticketSqlDal;
        const string SessionName = "User_Auth";

        private readonly IAuthProvider authProvider;

        public EventController(IAuthProvider authProvider, SmtpClient smtpClient, IPodcastSqlDal podcastSqlDal, IEventSqlDal eventSqlDal, IGenreSqlDal genreSqlDal, IVenueSqlDal venueSqlDal, ITicketSqlDal ticketSqlDal)
        {
            this.smtpClient = smtpClient;
            this.podcastDal = podcastSqlDal;
            this.eventSqlDal = eventSqlDal;
            this.genreSqlDal = genreSqlDal;
            this.venueSqlDal = venueSqlDal;
            this.ticketSqlDal = ticketSqlDal;
            this.authProvider = authProvider;

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
        public async System.Threading.Tasks.Task<IActionResult> EditEvent(int id, EventViewModel model)
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

                //MailController mailController = new MailController(smtpClient);
                // await mailController.Post();


                PopulateEventDetails(id, "Edit");

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


                PopulateEventDetails(model.EventItem.EventID, "Created");
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
            iCalendar ical = new iCalendar();
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

        private void PopulateEventDetails(int id, string action)
        {

            User user = authProvider.GetCurrentUser();
            DateTime timeChanged = DateTime.Now;
            Event eventItem = eventSqlDal.GetEvent(id);

            string eventDetail = timeChanged +","+user.Email +"," + 
                                 user.Name + "," +
                                 action + 
                                 "Event ID: " + eventItem.EventID + "," +
                                 "VenueID: " + eventItem.VenueID + "," +
                                 "Beginning: " + eventItem.Beginning + "," +
                                 "Ending: " + eventItem.Ending.ToString() + "," +
                                 "DescriptionCopy: " + eventItem.DescriptionCopy + "," +
                                 "TicketLevel: " + eventItem.TicketLevel + "," +
                                 "UpsaleCopy: " + eventItem.UpsaleCopy + "," +
                                 "IsFinalized: " + eventItem.IsFinalized + "," +
                                 "EventName: " + eventItem.EventName + "," +
                                 "PodcastID: " + eventItem.PodcastID + "," +
                                 "CoverPhoto: " + eventItem.CoverPhoto;


           System.IO.File.AppendAllText(@"c:\pmlog\log.txt", (eventDetail + "\n"));

        }

         Event PopulateEventDetails(int id)
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

     
        //public void SendEmail()  //(string receiver)
        //{
        //    string receiver = "katcandcode@gmail.com"; //list of admin emails
        //    string subject = "Event Change Notice";
        //    string message= "Testing";

                    
        //            MailController mailController = new MailController();

        //            mailController.Post();
                  
                  
        //}
            
    }

}