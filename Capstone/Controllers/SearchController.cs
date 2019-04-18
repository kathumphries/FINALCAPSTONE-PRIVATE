using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.DAL.Interfaces;
using Capstone.Models;
using Capstone.Models.ViewModel;
using Capstone.Providers.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Web;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Capstone.Controllers
{
    public class SearchController : Controller
    {
        const string SessionName = "User_Auth";

        private readonly IAuthProvider authProvider;
        private readonly IPodcastSqlDal podcastSqlDal;
        private readonly IEventSqlDal eventSqlDal;
        private readonly IGenreSqlDal genreSqlDal;
        private readonly IVenueSqlDal venueSqlDal;
        private readonly ITicketSqlDal ticketSqlDal;

        public SearchController(IPodcastSqlDal podcastSqlDal, IEventSqlDal eventSqlDal, IGenreSqlDal genreSqlDal, IVenueSqlDal venueSqlDal, ITicketSqlDal ticketSqlDal, IAuthProvider authProvider)
        {
            this.podcastSqlDal = podcastSqlDal;
            this.eventSqlDal = eventSqlDal;
            this.genreSqlDal = genreSqlDal;
            this.venueSqlDal = venueSqlDal;
            this.ticketSqlDal = ticketSqlDal;
            this.authProvider = authProvider;
        }

        public IActionResult Copy()
        {
            return View();
        }

        // GET: /<controller>/
        [HttpGet]
        public IActionResult Index()
        {
            User user = new User();
            user = authProvider.GetCurrentUser();

            Event eventItem = new Event();
            eventItem.Podcast = new Podcast();
            eventItem.Podcast.Genre = new Genre();


            SearchViewModel model = new SearchViewModel
            {
                Event = eventItem,
                VenueList = GetVenueList(),
                GenreList = GetGenreList(),
                TicketList = GetTicketList(),
                PodcastList = GetPodcastList(),
                TimeOfDayList = GetTimeOfDay(),
                User = user,
                UserFav = new Dictionary<int, bool>()
            };          

            model.EventList = eventSqlDal.GetFutureEvents(eventItem, user);
            

            foreach (Event item in model.EventList)
            {  
                item.Venue = venueSqlDal.GetVenue(item.VenueID);
                item.Podcast = podcastSqlDal.GetPodcast(item.PodcastID);
                item.Podcast.Genre = genreSqlDal.GetGenreEventID(item.EventID);
                item.Ticket = ticketSqlDal.GetTicket(item.TicketLevel);

            }

            model.ArchivedEventList = eventSqlDal.GetPastEvents(eventItem, user);
            foreach (Event item in model.ArchivedEventList)
            {         
                item.Venue = venueSqlDal.GetVenue(item.VenueID);
                item.Podcast = podcastSqlDal.GetPodcast(item.PodcastID);
                item.Podcast.Genre = genreSqlDal.GetGenreEventID(item.EventID);
                item.Ticket = ticketSqlDal.GetTicket(item.TicketLevel);

            }

            model.EventListByDay = model.EventList
            .GroupBy(p => p.Beginning.Date)
            .Select(g => g.ToList())
            .ToList();

            List<Event> userEvents = eventSqlDal.GetUserEvents(user);

            foreach (Event item in userEvents)
            {
                model.UserFav.Add(item.EventID, true);
            }

            return View(model);
        }               

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SearchEvents(SearchViewModel model)
        {
            User user = authProvider.GetCurrentUser();
            model.User = user;
            model.EventList = eventSqlDal.Search(model.Event, user);

            foreach (Event item in model.EventList)
            {
                item.Venue = venueSqlDal.GetVenue(item.VenueID);               
                item.Podcast = podcastSqlDal.GetPodcast(item.PodcastID);
                item.Podcast.Genre = genreSqlDal.GetGenre(item.Podcast.GenreID);
                item.Ticket = ticketSqlDal.GetTicket(item.TicketLevel);

            }

            return View(model);
        }

        [HttpGet]
        public IActionResult AddUserEvent(int id)
        {
            User user = authProvider.GetCurrentUser();

            try
            {
                if (user.Role == 1 || user.Role == 2)
                {
                    eventSqlDal.AddUserEvent(user, id);
                }
               
            }
            catch (Exception)
            {

                
            }          

            return RedirectToAction("Index", "Search");
        }

        [HttpGet]
        public IActionResult RemoveUserEvent(int id)
        {
            User user = authProvider.GetCurrentUser();

            try
            {
                if (user.Role == 1 || user.Role == 2)
                {
                    eventSqlDal.RemoveUserEvent(user, id);
                }
            }
            catch (Exception)
            {


            }

            return RedirectToAction("Index", "Search");
        }

        [HttpGet]
        public IActionResult AddUserEventMyCal(int id)
        {
            User user = authProvider.GetCurrentUser();

            try
            {
                if (user.Role == 1 || user.Role == 2)
                {
                    eventSqlDal.AddUserEvent(user, id);
                }

            }
            catch (Exception)
            {


            }

            return RedirectToAction("UserCalendar", "Search");
        }

        [HttpGet]
        public IActionResult RemoveUserEventMyCal(int id)
        {
            User user = authProvider.GetCurrentUser();

            try
            {
                if (user.Role == 1 || user.Role == 2)
                {
                    eventSqlDal.RemoveUserEvent(user, id);
                }
            }
            catch (Exception)
            {


            }

            return RedirectToAction("UserCalendar", "Search");
        }

        public IActionResult UserCalendar()
        {
            User user = authProvider.GetCurrentUser();

            Event eventItem = new Event();
            eventItem.Podcast = new Podcast();
            eventItem.Podcast.Genre = new Genre();


            SearchViewModel model = new SearchViewModel
            {
                Event = eventItem,
                User = user,
                UserFav = new Dictionary<int, bool>()
            };

            model.EventList = eventSqlDal.GetUserEvents(user);

            foreach (Event item in model.EventList)
            {
                model.UserFav.Add(item.EventID, true);
            }

            foreach (Event item in model.EventList)
            {
                item.Venue = venueSqlDal.GetVenue(item.VenueID);
                item.Podcast = podcastSqlDal.GetPodcast(item.PodcastID);
                item.Podcast.Genre = genreSqlDal.GetGenreEventID(item.EventID);
                item.Ticket = ticketSqlDal.GetTicket(item.TicketLevel);

            } 

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
            List<Podcast> podcastList = podcastSqlDal.GetAllPodcasts();

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
