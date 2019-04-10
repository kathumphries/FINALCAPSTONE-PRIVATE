using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;



namespace Capstone.Models
{

    public class EventViewModel
    {
        public Event EventItem { get; set; }
        public Podcast PodcastID { get; set; }
        public List<Venue> VenueList { get; set; }
        public List<Genre> GenreList { get; set; }
    }

}
