using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models.ViewModel
{
    public class EventViewModel
    {
        public Event EventItem { get; set; }
        public List<SelectListItem> GenreList { get; set; }

        [Display(Name = "Venue")]
        public List<SelectListItem> VenueList { get; set; }
        [Display(Name = "Podcast")]
        public List<SelectListItem> PodcastList { get; set; }
        [Display(Name = "TicketLevel")]
        public List<SelectListItem> TicketLevelList { get; set; }


    }
}
