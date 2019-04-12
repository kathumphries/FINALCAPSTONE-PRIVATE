﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models.ViewModel
{
    public class SearchViewModel
    {
        public Event Event { get; set; }
        public List<Event> EventList { get; set; }
        public List<SelectListItem> GenreList { get; set; }

        [Display(Name = "Venue")]
        public List<SelectListItem> VenueList { get; set; }

        public List<SelectListItem> TicketList { get; set; }

        [Display(Name = "Podcast")]
        public List<SelectListItem> PodcastList { get; set; }
    }
}
