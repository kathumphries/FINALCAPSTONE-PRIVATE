using Capstone.DAL.Interfaces;
using Capstone.Models;
using Capstone.Models.Account;
using Capstone.Providers.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using Capstone.Providers.Auth;
using System.Text;
using System;
using Capstone.Models.ViewModel;

namespace Capstone.Controllers
{
    public class AccountController : Controller

    {
        const string SessionName = "User_Auth";
        
        private readonly IAuthProvider authProvider;
        private readonly IUserSqlDal userSqlDal;
        private readonly IPodcastSqlDal podcastSqlDal;
        private readonly IEventSqlDal eventSqlDal;
        private readonly IGenreSqlDal genreSqlDal;
        private readonly IVenueSqlDal venueSqlDal;
        private readonly ITicketSqlDal ticketSqlDal;



        public AccountController(IAuthProvider authProvider, IUserSqlDal userSqlDal, IEventSqlDal eventSqlDal, ITicketSqlDal ticketSqlDal) 
            //IPodcastSqlDal podcastSqlDal, IGenreSqlDal genreSqlDal, IVenueSqlDal venueSqlDal, ITicketSqlDal ticketSqlDal)
        {
            this.authProvider = authProvider;
            this.userSqlDal = userSqlDal;
            this.podcastSqlDal = podcastSqlDal;
            this.eventSqlDal = eventSqlDal;
            this.genreSqlDal = genreSqlDal;
            this.venueSqlDal = venueSqlDal;
            this.ticketSqlDal = ticketSqlDal;
        }

        //Roles in db: 
        //("1" - Admin, "2 - RegisterUser", "3 - Podcaster", "4 - Anonymous"

        //[AuthorizationFilter] // actions can be filtered to only those that are logged in
   
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            // Ensure the fields were filled out
            if (ModelState.IsValid)
            {
                // Check that they provided correct credentials
                bool validLogin = authProvider.SignIn(loginViewModel.Email, loginViewModel.Password);
                if (validLogin)
                {
                    // Redirect the user where you want them to go after successful login
                    return RedirectToAction("Index", "Home");
                }
            }

            return View(loginViewModel);
        }

        [HttpGet]
        [AuthorizationFilter("1","2","3")]
        public IActionResult Logout()
        {
            // Clear user from session
            authProvider.LogOff();

            // Redirect the user where you want them to go after logoff
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
       public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {

                // Register them as a new user (and set default role)
                // When a user registeres they need to be given a role. If you don't need anything special
                // just give them "User".
                if (authProvider.Register(registerViewModel.Email, registerViewModel.Password, 2) == false)
                {
                    return View(registerViewModel);
                }

                // Redirect the user where you want them to go after registering
                return RedirectToAction("Registered", "Account");
            }
            
            return View(registerViewModel);
        }


        [AuthorizationFilter]
        [HttpGet]
        public IActionResult Registered()
        {
            return View();
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

        [AuthorizationFilter("2")]
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
        [AuthorizationFilter("2")]
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

        [AuthorizationFilter("2")]  //user
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
        [AuthorizationFilter("2")]  //admin only
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
        [AuthorizationFilter("2")]  //user
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

        public List<SelectListItem> GetPodcastList()
        {
            List<Podcast> podcastList = podcastSqlDal.GetAllPodcasts();

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





        private Event PopulateEventDetails(int id)
        {
            Event eventItem = eventSqlDal.GetEvent(id);



            if (eventItem.PodcastID != null)
            {
                eventItem.Podcast = podcastSqlDal.GetPodcast(eventItem.PodcastID);
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


    }//class
}//namespace   
        





