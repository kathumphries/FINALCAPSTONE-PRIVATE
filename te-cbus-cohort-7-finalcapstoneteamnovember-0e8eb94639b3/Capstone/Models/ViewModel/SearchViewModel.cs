using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models.ViewModel
{
    public class SearchViewModel
    {
        [Display(Name = "Time of Day")]
        public string TimeOfDayString {get; set;}

        public Event Event { get; set; }
        public List<Event> EventList { get; set; }
        public List<Event> ArchivedEventList { get; set; }
        public List<Event> EventListDay0 { get; set; }
        public List<Event> EventListDay1 { get; set; }
        public List<Event> EventListDay2 { get; set; }
        public List<Event> EventListDay3 { get; set; }
        public List<Event> EventListDay4 { get; set; }

        [Display(Name = "Genre List")]
        public List<SelectListItem> GenreList { get; set; }

        [Display(Name = "Time of Day")]
        public List<SelectListItem> TimeOfDayList { get; set; }

        [Display(Name = "Venue")]
        public List<SelectListItem> VenueList { get; set; }

        [Display(Name = "Ticket Type")]
        public List<SelectListItem> TicketList { get; set; }

        [Display(Name = "Podcast")]
        public List<SelectListItem> PodcastList { get; set; }

        
    }
}
